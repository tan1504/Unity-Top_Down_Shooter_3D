using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoverPerk { Unavailable, CanTakeOver, CanCheckAndChangeCover }
public enum UnstoppablePerk { Unavailable, Unstoppable }
public enum GranadePerk { Unavailable, CanThrowGranade }

public class Enemy_Range : Enemy
{
	[Header("Enemy Perks")]
	public Enemy_RangeWeaponType weaponType;
	public CoverPerk coverPerk;
	public UnstoppablePerk unstoppablePerk;
	public GranadePerk granadePerk;

	[Header("Granade Perk")]
	public int grenadeDamage;
	public GameObject grenadePrefab;
	public float impactPower;
	public float explosionTimer = 0.75f;
	public float timeToTarget = 1.2f;
	public float granadeCooldown;
	private float lastTimeGrandeCooldown = -10;
	[SerializeField] private Transform grenadeStartPoint;

	[Header("Advance Perk")]
	public float advanceSpeed;
	public float advanceStoppingDistance;
	public float advanceDuration = 2.5f;

	[Header("Cover System")]
	public float minCoverTime;
	public float safeDistance;
	public CoverPoint currentCover {  get; private set; }
	public CoverPoint lastCover { get; private set; }

	[Header("Weapon Details")]
	public float attackDelay;
	public Enemy_RangeWeaponData_SO weaponData;

	[Space]
	public Transform gunPoint;
	public Transform weaponHolder;
	public GameObject bulletPrefab;

	[Header("Aim Details")]
	public float slowAim = 4;
	public float fastAim = 20;
	public Transform aim;
	public Transform playersBody;
	public LayerMask whatToIgnore;

	[SerializeField] List<Enemy_RangeWeaponData_SO> availableWeaponData;

	#region States

	public IdleState_Range idleState {  get; private set; }
	public MoveState_Range moveState { get; private set; }
	public BattleState_Range battleState { get; private set; }
	public RunToCoverState_Range runToCoverState { get; private set; }
	public AdvanceState_Range advanceState { get; private set; }
	public ThrowGranadeState_Range throwGranadeState { get; private set; }
	public DeadState_Range deadState { get; private set; }
	#endregion

	protected override void Awake()
	{
		base.Awake();

		idleState = new IdleState_Range(this, stateMachine, "Idle");
		moveState = new MoveState_Range(this, stateMachine, "Move");
		battleState = new BattleState_Range(this, stateMachine, "Battle");
		runToCoverState = new RunToCoverState_Range(this, stateMachine, "Run");
		advanceState = new AdvanceState_Range(this, stateMachine, "Advance");
		throwGranadeState = new ThrowGranadeState_Range(this, stateMachine, "ThrowGranade");
		deadState = new DeadState_Range(this, stateMachine, "Idle");		// idle is place holder, we using ragdoll
	}

	protected override void Start()
	{
		base.Start();

		playersBody = player.GetComponent<Player>().playerBody;
		aim.parent = null;

		InitializePerk();  

		stateMachine.Initialize(idleState);
		visuals.SetupLook();
		SetupWeapon();
	}

	protected override void Update()
	{
		base.Update();

		stateMachine.currentState.Update();
	}

	public override void Die()
	{
		base.Die();

		if (stateMachine.currentState != deadState)
			stateMachine.ChangeState(deadState);
	}

	public bool CanThrowGranade()
	{
		if (granadePerk == GranadePerk.Unavailable)
			return false;

		if (Vector3.Distance(player.position, transform.position) < safeDistance)
			return false;

		if (Time.time > granadeCooldown + lastTimeGrandeCooldown)
			return true;

		return false;
	}

	public void ThrowGranade()
	{
		lastTimeGrandeCooldown = Time.time;
		visuals.EnableGrenadeModel(false); 

		GameObject newGranade = ObjectPool.instance.GetObject(grenadePrefab, grenadeStartPoint);

		Enemy_Granade newGrenadeScript = newGranade.GetComponent<Enemy_Granade>();

        if (stateMachine.currentState == deadState)
        {
			newGrenadeScript.SetupGranade(whatIsAlly, transform.position, 1, explosionTimer, impactPower, grenadeDamage);
			return;
		}

        newGrenadeScript.SetupGranade(whatIsAlly, player.position, timeToTarget, explosionTimer, impactPower, grenadeDamage);
	}

	protected override void InitializePerk()
	{
		if (weaponType == Enemy_RangeWeaponType.Random)
		{
			ChooseRandomWeaponType();
		}

		if (IsUnstoppable())
        {
			advanceSpeed = 1;
			anim.SetFloat("AdvanceAnimIndex", 1);		// Slow walk animation
        }
    }

	private void ChooseRandomWeaponType()
	{
		List<Enemy_RangeWeaponType> validTypes = new List<Enemy_RangeWeaponType>();

		foreach (Enemy_RangeWeaponType value in Enum.GetValues(typeof(Enemy_RangeWeaponType)))
		{
			if (value != Enemy_RangeWeaponType.Random && value != Enemy_RangeWeaponType.Sniper)
				validTypes.Add(value);
		}

		int randomIndex = UnityEngine.Random.Range(0, validTypes.Count);
		weaponType = validTypes[randomIndex];
	}

	public override void EnterBattleMode()
	{
		if (inBattleMode)
			return;

		base.EnterBattleMode();

		if (CanGetCover())
			stateMachine.ChangeState(runToCoverState);
		else
			stateMachine.ChangeState(battleState);
	}


	#region Cover System

	public bool CanGetCover()
	{
		if (coverPerk == CoverPerk.Unavailable)
			return false;

		currentCover = AttempToFindCover()?.GetComponent<CoverPoint>();

		if (lastCover != currentCover && currentCover != null)
			return true;

		Debug.Log("No cover found");
		return false;
	}

	private Transform AttempToFindCover()
	{
		List<CoverPoint> collectedCoverPoints = new List<CoverPoint>();

		foreach (Cover cover in CollectNearByCovers())
		{
			collectedCoverPoints.AddRange(cover.GetValidCoverPoints(transform));
		} 

		CoverPoint closestCoverPoint = null;
		float shortestDistance = float.MaxValue;

		foreach (CoverPoint coverPoint in collectedCoverPoints)
		{
			float currentDistance = Vector3.Distance(transform.position, coverPoint.transform.position);

			if (currentDistance < shortestDistance)
			{
				closestCoverPoint = coverPoint;
				shortestDistance = currentDistance;
			}
		}

		if (closestCoverPoint != null)
		{
			lastCover?.SetOccupied(false);
			lastCover = currentCover;

			currentCover = closestCoverPoint;
			currentCover.SetOccupied(true);

			return currentCover.transform;
		}

		return null;	
	}

	private List<Cover> CollectNearByCovers()
	{
		float coverRadiusCheck = 30;
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, coverRadiusCheck);
		List<Cover> collectedCovers = new List<Cover>();

		foreach (Collider collider in hitColliders)
		{
			Cover cover = collider.GetComponent<Cover>();

			if (cover != null && collectedCovers.Contains(cover) == false) 
				collectedCovers.Add(cover);
		}

		return collectedCovers;
	}

	#endregion

	public void FireSingleBullet()
	{
		anim.SetTrigger("Shoot");

		Vector3 bulletDirection = (aim.position - gunPoint.position).normalized;

		GameObject newBullet = ObjectPool.instance.GetObject(bulletPrefab, gunPoint);
		newBullet.transform.rotation = Quaternion.LookRotation(gunPoint.forward);

		newBullet.GetComponent<Bullet>().BulletSetup(whatIsAlly, weaponData.bulletDamage);

		Rigidbody rbNewBullet = newBullet.GetComponent<Rigidbody>();

		Vector3 bulletDirectionWithSpread = weaponData.ApplyWeaponSpread(bulletDirection);

		rbNewBullet.mass = 20 / weaponData.bulletSpeed;
		rbNewBullet.velocity = bulletDirectionWithSpread * weaponData.bulletSpeed;
	}

	private void SetupWeapon()
	{
		List<Enemy_RangeWeaponData_SO> filteredData = new List<Enemy_RangeWeaponData_SO>();

		foreach(var weaponData in availableWeaponData)
		{
			if(weaponData.weaponType == weaponType)
				filteredData.Add(weaponData);
		}

		if (filteredData.Count > 0)
		{
			int random = UnityEngine.Random.Range(0, filteredData.Count);
			weaponData = filteredData[random];
		}
		else
			Debug.LogWarning("No available weapon data was found");

		gunPoint = visuals.currentWeaponModel.GetComponent<Enemy_RangeWeaponModel>().gunPoint;
	}

	#region Enemy's Aim Region

	public void UpdateAimPosition()
	{
		float aimSpeed = IsAimOnPlayer() ? fastAim : slowAim;

		aim.position = Vector3.MoveTowards(aim.position, playersBody.position, aimSpeed * Time.deltaTime);

	}

	public bool IsAimOnPlayer()
	{
		float distanceAimToPlayer = Vector3.Distance(aim.position, player.position);

		return distanceAimToPlayer < 2;
	}

	public bool IsSeeingPlayer()
	{
		Vector3 myPosition = transform.position + Vector3.up;
		Vector3 directionToPlayer = playersBody.position - myPosition;

        if (Physics.Raycast(myPosition, directionToPlayer, out RaycastHit hit, Mathf.Infinity, ~whatToIgnore))
        {
            if (hit.transform.root == player.root)      // root: never returns null, if this Transform doesn't have a parent it returns itself.
			{
				
				UpdateAimPosition();
				return true;
            }
        }

		return false;
    }
	#endregion

	public bool IsUnstoppable() => unstoppablePerk == UnstoppablePerk.Unstoppable;

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, advanceStoppingDistance);
	}
}

using UnityEngine;

public class BattleState_Range : EnemyState
{
	private Enemy_Range enemy;

	private float lastTimeShot = -10;
	private int bulletsShot = 0;

	private int bulletsPerAttack;
	private float weaponCooldown;

	private float coverCheckTimer;
	private bool fistTimeAttack = true;

	public BattleState_Range(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
	{
		enemy = enemyBase as Enemy_Range;
	}

	public override void Enter()
	{
		base.Enter();

		SetupValuesForFirstAttack();

		enemy.agent.isStopped = true;
		enemy.agent.velocity = Vector3.zero;

		enemy.visuals.EnableIK(true, true);

		stateTimer = enemy.attackDelay;

	}

	public override void Update()
	{
		base.Update();

		if (enemy.IsSeeingPlayer())
			enemy.FaceTarget(enemy.aim.position);

		if (enemy.CanThrowGranade())
			stateMachine.ChangeState(enemy.throwGranadeState);

		if (MustAdvancePlayer())
			stateMachine.ChangeState(enemy.advanceState);	

		CheckCoverIfShould();

		if (stateTimer > 0)
			return;

		if (WeaponOutOfBullets())
		{
			if (enemy.IsUnstoppable() && UnstoppableWalkReady())
			{
				enemy.advanceDuration = weaponCooldown;
				stateMachine.ChangeState(enemy.advanceState);
			}

			if (WeaponOnCooldown())
				AttempToResetWeapon();

			return;
		}

		if (CanShoot() && enemy.IsAimOnPlayer())
		{
			Shoot();
		}
	}

	private bool MustAdvancePlayer()
	{
		if (enemy.IsUnstoppable())
			return false;

		return enemy.IsPlayerInAggressionRange() == false && ReadyToLeaveCover();
	}

	private bool UnstoppableWalkReady()
	{
		float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
		bool outOfStopppingDistance = distanceToPlayer > enemy.advanceStoppingDistance;
		bool unstoppableWalkCooldown =
			Time.time < enemy.weaponData.minWeaponCooldown + enemy.advanceState.lastTimeAdvanced;

		return outOfStopppingDistance && unstoppableWalkCooldown == false;
	}

	#region Cover System Region
	 
	private bool ReadyToLeaveCover()
	{
		return Time.time > enemy.minCoverTime + enemy.runToCoverState.lastTimeTookCover;
	}

	private void CheckCoverIfShould()
	{
		if (enemy.coverPerk != CoverPerk.CanCheckAndChangeCover)
			return;

		coverCheckTimer -= Time.deltaTime;

		if (coverCheckTimer < 0)
		{
			coverCheckTimer = 0.5f;			// We do cover check each 0.5s;

			if (ReadyToChangeCover() && ReadyToLeaveCover())
			{
				if (enemy.CanGetCover())
					stateMachine.ChangeState(enemy.runToCoverState);
			}
		}

	}

	private bool ReadyToChangeCover()
	{
		bool inDanger = IsPlayerInClearSight() || IsPlayerClose();
		bool advanceTimeIsOver = Time.time > enemy.advanceState.lastTimeAdvanced + enemy.advanceDuration;

		return inDanger && advanceTimeIsOver;
	}

	private bool IsPlayerClose()
	{
		return Vector3.Distance(enemy.transform.position, enemy.player.position) < enemy.safeDistance;
	}

	private bool IsPlayerInClearSight()
	{
		Vector3 directionToPlayer = (enemy.player.position + Vector3.up) - enemy.transform.position;        // Vector3.up makes sure ray touch on player

		if (Physics.Raycast(enemy.transform.position, directionToPlayer, out RaycastHit hit))
		{
			if (hit.transform.root == enemy.player.root)
				return true;
		}

		return false;
	}

	#endregion

	#region Weapon Region

	private void AttempToResetWeapon()
	{
		bulletsShot = 0;
		bulletsPerAttack = enemy.weaponData.GetBulletsPerAttack();
		weaponCooldown = enemy.weaponData.GetWeaponCooldown();
	}

	private bool WeaponOnCooldown() => Time.time > lastTimeShot + weaponCooldown;

	private bool WeaponOutOfBullets() => bulletsShot >= bulletsPerAttack;

	private bool CanShoot() => Time.time > lastTimeShot + (1 / enemy.weaponData.fireRate);

	private void Shoot()
	{
		enemy.FireSingleBullet();
		lastTimeShot = Time.time;
		bulletsShot++;
	}

	private void SetupValuesForFirstAttack()
	{
		if (fistTimeAttack)
		{
			// Advance stop distance should be slitly smaller than aggresstion range in order
			// in order for enemy to advance on the time.

			enemy.aggressionRange = enemy.advanceStoppingDistance + 2;

			fistTimeAttack = false;
			bulletsPerAttack = enemy.weaponData.GetBulletsPerAttack();
			weaponCooldown = enemy.weaponData.GetWeaponCooldown();
		}
	}
	#endregion
}

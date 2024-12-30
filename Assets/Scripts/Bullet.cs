using UnityEngine;

public class Bullet : MonoBehaviour
{
	private int bulletDamage;
	private float impactForce;

	private Rigidbody rb;
	private BoxCollider cd;
	private MeshRenderer meshRenderer;
	private TrailRenderer trailRenderer;

	[SerializeField] private GameObject bulletImpactFX;

	private Vector3 startPosition;
	private float flyDistance;
	private bool bulletDisabled;

	private LayerMask allyLayerMask;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody>();
		cd = GetComponent<BoxCollider>();
		meshRenderer = GetComponent<MeshRenderer>();
		trailRenderer = GetComponent<TrailRenderer>();
	}

	protected virtual void Update()
	{
		FadeTrailIfNeeded();
		DisabledBulletIfNeeded();
		ReturnToPoolIfNeeded();
	}

	protected void ReturnToPoolIfNeeded()
	{
		if (trailRenderer.time <= 0)
			ReturnBulletToPool();
	}

	protected void DisabledBulletIfNeeded()
	{
		if (Vector3.Distance(startPosition, transform.position) > flyDistance && !bulletDisabled)
		{
			cd.enabled = false;
			meshRenderer.enabled = false;
			bulletDisabled = true;
		}
	}

	protected void FadeTrailIfNeeded()
	{
		if (Vector3.Distance(startPosition, transform.position) > flyDistance - 1.5f)
			trailRenderer.time -= 2f * Time.deltaTime; // number 2 is chosen through testing	 
	}

	public void BulletSetup(LayerMask allyLayerMask, int bulletDamage, float flyDistance = 100, float impactForce = 100)
	{
		this.allyLayerMask = allyLayerMask;
		this.impactForce = impactForce;
		this.bulletDamage = bulletDamage;

		bulletDisabled = false;
		cd.enabled = true;
		meshRenderer.enabled = true;

		trailRenderer.Clear();
		trailRenderer.time = 0.25f;
		startPosition = transform.position;
		this.flyDistance = flyDistance + 0.5f;  // 0.5f is the length of tip of the laser  (Check method UpdateAimVisuals on  PlayerAim script)
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (FriendlyFire() == false)
		{
			// Use a bitwise AND to check if the collision layer is in  the allyLayerMask
			if ((allyLayerMask.value & (1 << collision.gameObject.layer)) > 0)
			{
				ReturnBulletToPool(10);
				return;
			}
		}

		CreateImpactFX();
		ReturnBulletToPool();

		IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
		damageable?.TakeDamge(bulletDamage);

		ApplyBulletImpactToEnemy(collision);
	}

	private void ApplyBulletImpactToEnemy(Collision collision)
	{
		Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
		if (enemy != null)
		{
			Vector3 force = rb.velocity.normalized * impactForce;
			Rigidbody hitRigidbody = collision.collider.attachedRigidbody;

			enemy.BulletImpact(force, collision.contacts[0].point, hitRigidbody);
		}
	}

	protected void ReturnBulletToPool(float delay = 0) => ObjectPool.instance.ReturnObject(gameObject, delay);

	protected void CreateImpactFX()
	{
		GameObject newFX = Instantiate(bulletImpactFX);
		newFX.transform.position = transform.position;

		Destroy(newFX, 1);

		//GameObject newImpactFX = ObjectPool.instance.GetObject(bulletImpactFX, transform);
		//ObjectPool.instance.ReturnObject(newImpactFX, 1);
	}

	private bool FriendlyFire() => GameManager.instance.friendlyFire;
}

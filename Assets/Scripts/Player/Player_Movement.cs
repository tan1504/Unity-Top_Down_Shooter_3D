using UnityEngine;

public class Player_Movement : MonoBehaviour
{
	private Player player;
	private PlayerControls controls;
	private CharacterController characterController;
	private Animator animator;

	[Header("Movement Infor")]
	[SerializeField] private float walkSpeed;
	[SerializeField] private float turnSpeed;
	[SerializeField] private float runSpeed;
	private float speedMovement;
	private Vector3 movementDirection;
	private float verticalVelocity;
	private bool isRunning;

	public Vector2 moveInput { get; private set; }
	private Vector3 lookingDirection;

	private AudioSource walkSFX;
	private AudioSource runSFX;
	private bool canPlayFootsteps;

	private void Start()
	{
		player = GetComponent<Player>();
		
		Invoke(nameof(AllowFootstepSFX), 1);
		walkSFX = player.sound.walkSFX;
		runSFX = player.sound.runSFX;

		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();

		speedMovement = walkSpeed;
		animator.speed = walkSpeed;

		AssignInputEvents();
	}

	private void Update()
	{
		if(player.health.isDead) 
			return;

		ApplyMovement();
		ApplyRotation();
		AnimatorControllers();
	}

	private void AnimatorControllers()
	{
		float xVelovity = Vector3.Dot(movementDirection.normalized, transform.right);
		float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

		animator.SetFloat("xVelocity", xVelovity, 0.1f, Time.deltaTime);
		animator.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);

		bool playerRun = isRunning && moveInput.magnitude > 0;
		animator.SetBool("isRunning", playerRun);
	}

	private void ApplyRotation()
	{
		lookingDirection = player.aim.GetMouseHitInfor().point - transform.position;
		lookingDirection.y = 0f;
		lookingDirection.Normalize();

		Quaternion desiredRotation = Quaternion.LookRotation(lookingDirection);
		transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
	}

	private void ApplyMovement()
	{
		movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

		Debug.Log(movementDirection.magnitude);

		ApplyGravity();

		if (movementDirection.magnitude > 0)
		{
			PlayFootStepsSFX(isRunning);

			characterController.Move(movementDirection * speedMovement * Time.deltaTime);
		}
		//else
		//	StopFootStepsSFX();
	}

	private void PlayFootStepsSFX(bool isRunning)
	{
		if (canPlayFootsteps == false)
			return;

		if (isRunning)
		{
			if (runSFX.isPlaying == false)
				runSFX.Play();
		}
		else
		{
			if (walkSFX.isPlaying == false)
			{
				walkSFX.Play();
			}
		}
	}

	//private void StopFootStepsSFX()
	//{
	//	walkSFX.Stop();
	//	runSFX.Stop();
	//}

	private void AllowFootstepSFX() => canPlayFootsteps = true;

	private void ApplyGravity()
	{
		if (characterController.isGrounded == false)
		{
			verticalVelocity = verticalVelocity - 9.81f * Time.deltaTime;
			movementDirection.y = verticalVelocity;
		}
		else
		{
			movementDirection.y = -0.5f;
		}
	}

	private void AssignInputEvents()
	{
		controls = player.controls;

		controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
		controls.Character.Movement.canceled += context =>
		{
			//StopFootStepsSFX();	
			moveInput = Vector2.zero;
		};

		controls.Character.Run.performed += context =>
		{
			isRunning = true;
			speedMovement = runSpeed;
			animator.speed = 1f;
		};
		controls.Character.Run.canceled += context =>
		{
			isRunning = false;
			speedMovement = walkSpeed;
			animator.speed = walkSpeed;
		};
	}
}

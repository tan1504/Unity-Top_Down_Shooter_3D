using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Boss : EnemyState
{
	private Enemy_Boss enemy;
	private bool interactionDisabled;

	public DeadState_Boss(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
	{
		enemy = enemyBase as Enemy_Boss;
	}

	public override void Enter()
	{
		base.Enter();

		enemy.abilityState.DisableFlamethrower();

		interactionDisabled = false;

		stateTimer = 1.5f;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		base.Update();

		//	uncomment if we want to interact with enemy's body
		//DisableInteractionIfShould();
	}

	private void DisableInteractionIfShould()
	{
		if (stateTimer < 0 && interactionDisabled == false)
		{
			interactionDisabled = true;
			enemy.ragdoll.CollidersActive(false);
			enemy.ragdoll.RagdollActive(false);
		}
	}
}

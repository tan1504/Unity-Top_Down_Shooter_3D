using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Range : EnemyState
{
	private Enemy_Range enemy;

	private bool interactionDisabled;

	public DeadState_Range(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
	{
		enemy = enemyBase as Enemy_Range;
	}

	public override void Enter()
	{
		base.Enter();

		if (enemy.throwGranadeState.finishedThrowingGrenade == false)
			enemy.ThrowGranade();

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

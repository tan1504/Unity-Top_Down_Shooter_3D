using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Melee : EnemyState
{
	private Enemy_Melee enemy;

	private bool interactionDisabled;

	public DeadState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
	{
		enemy = enemyBase as Enemy_Melee;
	}

	public override void Enter()
	{
		base.Enter();

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

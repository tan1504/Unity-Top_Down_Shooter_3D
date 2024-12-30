using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState_Melee : EnemyState
{
	private Enemy_Melee enemy;
	private float lastTimeUpdateDestination;
	public ChaseState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
	{
		enemy = enemyBase as Enemy_Melee;
	}

	public override void Enter()
	{
		base.Enter();

		enemy.agent.speed = enemy.runSpeed;
		enemy.agent.isStopped = false;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		base.Update();

		if (enemy.PlayerInAttackRange())
			stateMachine.ChangeState(enemy.attackState);

		enemy.FaceTarget(GetNextPathPoint());

		if (CanUpdateDestination())
			enemy.agent.destination = enemy.player.position;
	}

	private bool CanUpdateDestination()
	{
		if (Time.time > lastTimeUpdateDestination + 0.25f)
		{
			lastTimeUpdateDestination = Time.time;
			return true;
		}

		return false;
	}
}

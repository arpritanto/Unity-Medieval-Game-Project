using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private int score;

    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);

        score = ScoringSystem.numberOfScore;
        score = score + 15;

        PlayerPrefs.SetInt("NumberOfScore", score);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
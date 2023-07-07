using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    // public GameObject GameOver;

    public override void Enter()
    {
        // GameOver = GameObject.Find("GameOverScene");
        // GameOver.SetActive(true);
        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = true;

        stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        
    }
}
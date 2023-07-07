using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private bool shouldFade;

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    private float speed;

    public const float maxEnergy = 100f;
    public float energy;

    public GameObject hud;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine) 
    {
        this.shouldFade = shouldFade;
    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f);

        if (shouldFade)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.Play(FreeLookBlendTreeHash);
        }

        hud = GameObject.Find("HUDManager");

        hud.GetComponent<HUDManager>().SetMaxEnergy(maxEnergy);

        energy = maxEnergy;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * speed, deltaTime);

        speed = (stateMachine.FreeLookMovementSpeed) / 2;

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            energy += 10 * Time.deltaTime;

            hud.GetComponent<HUDManager>().SetEnergy(energy);

            return;
        }

        if (energy <= 100)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.6f, AnimatorDampTime, deltaTime);
            energy += 5 * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (energy > 0)
            {
                stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1.5f, AnimatorDampTime, deltaTime);
                speed = 6;

                energy -= 10 * Time.deltaTime;

                if (energy <= 0)
                {
                    energy = 0;
                    stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.4f, AnimatorDampTime, deltaTime);
                    speed = 3;
                }
            }  
        }

        if (energy > 100)
        {
            energy = maxEnergy;
        }

        hud.GetComponent<HUDManager>().SetEnergy(energy);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;

        hud.GetComponent<HUDManager>().SetEnergy(energy);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }
}

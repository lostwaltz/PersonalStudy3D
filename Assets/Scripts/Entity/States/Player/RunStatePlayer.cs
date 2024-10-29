using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStatePlayer : State
{
    public RunStatePlayer()
    {
        tag = "Run";
    }
    public override void Enter()
    {
        animator.SetBool("Run", true);
    }

    public override void Execute()
    {
        movement.ApplyLook();
        movement.ApplyMovement();

        //TODO: need retouch magic number
        vitalController.ChangeValue(VitalType.STAMINA, -10f * Time.deltaTime);

        if (rayCheck.isLadderOnFront())
        {
            machine.TransitionTo(machine.states["Climb"]);
            return;
        }

        if (false == movement.isRun)
            machine.TransitionTo(machine.states["Walk"]);

        if (true == movement.isJumpTrigered)
            machine.TransitionTo(machine.states["Jump"]);
    }

    public override void Exit()
    {
        animator.SetBool("Run", false);
    }
}

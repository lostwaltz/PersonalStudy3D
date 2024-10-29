using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStatePlayer : State
{
    public WalkStatePlayer()
    {
        tag = "Walk";
    }

    public override void Enter()
    {
        animator.SetBool("Walk", true);
    }

    public override void Execute()
    {
        movement.ApplyLook();
        movement.ApplyMovement();

        if (rayCheck.isLadderOnFront())
        {
            machine.TransitionTo(machine.states["Climb"]);
            return;
        }

        if (true == movement.isRun)
            machine.TransitionTo(machine.states["Run"]);

        if (false == movement.isWalk)
            machine.TransitionTo(machine.states["Idle"]);

        if (true == movement.isJumpTrigered)
            machine.TransitionTo(machine.states["Jump"]);
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
    }
}

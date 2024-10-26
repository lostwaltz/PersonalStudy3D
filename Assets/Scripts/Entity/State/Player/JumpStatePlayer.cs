using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStatePlayer : State
{
    public JumpStatePlayer()
    {
        tag = "Jump";
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        movement.ApplyLook();
        movement.ApplyMovement();
        movement.ApplyJump();

        if (false == movement.isWalk)
            machine.TransitionTo(machine.states["Idle"]);
        if (false == movement.isRun)
            machine.TransitionTo(machine.states["Walk"]);
        if (true == movement.isRun)
            machine.TransitionTo(machine.states["Run"]);
    }

    public override void Exit()
    {
    }
}

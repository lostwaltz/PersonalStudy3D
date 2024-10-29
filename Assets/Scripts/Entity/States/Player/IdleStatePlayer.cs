using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class IdleStatePlayer : State
{
    public IdleStatePlayer()
    {
        tag = "Idle";
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        movement.ApplyLook();
        movement.ApplyMovement();

        if (true == movement.isWalk)
            machine.TransitionTo(machine.states["Walk"]);
        if(true == movement.isJumpTrigered)
            machine.TransitionTo(machine.states["Jump"]);
    }

    public override void Exit()
    {
    }

}

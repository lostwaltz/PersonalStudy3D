using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionStatePlayer : State
{
    public InteractionStatePlayer()
    {
        tag = "Interactive";
    }
    public override void Enter()
    {
    }

    public override void Execute()
    {
        movement.ApplyLook();
        

        if (true == movement.isWalk)
            machine.TransitionTo(machine.states["Walk"]);
        if (true == movement.isJumpTrigered)
            machine.TransitionTo(machine.states["Jump"]);
    }

    public override void Exit()
    {
    }
}

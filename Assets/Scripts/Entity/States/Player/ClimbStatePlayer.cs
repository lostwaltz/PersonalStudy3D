using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClimbStatePlayer : State
{
    Rigidbody rigidbody;

    public ClimbStatePlayer(Rigidbody _rigidbody)
    {
        rigidbody = _rigidbody;
        tag = "Climb";
    }

    public override void Enter()
    {
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
    }

    public override void Execute()
    {
        movement.ApplyLook();
        movement.ApplyCliming();

        //rigidbody.AddForce((Vector3.up * 10f) * Time.deltaTime, ForceMode.Acceleration);

        if (true == movement.isJumpTrigered)
            machine.TransitionTo(machine.states["Jump"]);

        if (false == rayCheck.isLadderOnFront())
        {
            if (false == movement.isWalk)
                machine.TransitionTo(machine.states["Idle"]);
            if (false == movement.isRun)
                machine.TransitionTo(machine.states["Walk"]);
            if (true == movement.isRun)
                machine.TransitionTo(machine.states["Run"]);
        }
    }

    public override void Exit()
    {
        rigidbody.useGravity = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCheck : MonoBehaviour
{

    private LayerMask groundLayerMask;

    private void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    public bool isGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }

        }
        return false;
    }

    public bool isLadderOnFront()
    {
        Ray ray = new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), transform.forward);

        if (Physics.Raycast(ray, 0.2f, 1 << LayerMask.GetMask("Ladder")))
        {
            return true;
        }

        return false;
    }
}

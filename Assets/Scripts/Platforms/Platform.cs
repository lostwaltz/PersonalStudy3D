using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 targetPostion;
    public LayerMask layerMask;

    private Vector3 startPostion;


    private void Awake()
    {
        startPostion = transform.position;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * 0.3f, 1f);
        transform.position = Vector3.Lerp(startPostion, targetPostion, t);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

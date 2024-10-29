using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    Coroutine coroutine = null;
    public LayerMask layerMask;

    private void Awake()
    {
        GetComponentInChildren<TMP_Text>().enabled = false;
    }

    private void Update()
    {


        Ray ray = new Ray(transform.position, transform.up);
        if (Physics.Raycast(ray, 5f, layerMask))
        {
            StarHitLaser();
        }
    }

    public void StarHitLaser()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(OnHit());
    }

    IEnumerator OnHit()
    {
        GetComponentInChildren<TMP_Text>().enabled = true;
        yield return new WaitForSeconds(5f);
        GetComponentInChildren<TMP_Text>().enabled = false;

    }
}

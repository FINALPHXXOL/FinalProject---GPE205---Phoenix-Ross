using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchOnHit : MonoBehaviour
{
    public Pawn owner;
    public AIController controller;
    public void Start()
    {
        controller = (AIController)owner.controller;
    }

    public void OnTriggerEnter(Collider other)
    {

        controller = (AIController)owner.controller;

        if (controller != null)
        {
            if (controller.isChasing == true)
            {
                CaughtStatus otherStatus = other.gameObject.GetComponent<CaughtStatus>();
                if (otherStatus != null)
                {
                    if (otherStatus != null)
                    {
                        //Debug.Log(otherStatus.name);
                        //AudioSource.PlayClipAtPoint(hitExplode, otherHealth.transform.position, 0f);
                        otherStatus.Caught(owner);
                    }
                }
            }
        }

    }
}

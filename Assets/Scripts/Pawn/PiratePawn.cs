using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PiratePawn : Pawn
{
    public int rotateNoise;
    public int moveNoise;
    public bool isSneaking;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveForward()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            if (mover != null)
            {
                mover.Move(transform.forward, moveSpeed);
            }
            if (noise != null)
            {
                if (isSneaking != true)
                {
                    noise.MakeNoise(10);
                } else if (isSneaking == true)
                {
                    noise.StopNoise();
                }
            }
        }
    }

    public override void MoveBackward()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            if (mover != null)
            {
                
                mover.Move(transform.forward, -moveSpeed);
            }
            if (noise != null)
            {
                if (isSneaking != true)
                {
                    noise.MakeNoise(10);
                } else if (isSneaking == true)
                {
                    noise.StopNoise();
                }
            }
        }
    }

    public override void RotateClockwise()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            if (mover != null)
            {
                mover.Rotate(turnSpeed);
            }
            if (noise != null)
            {
                if (isSneaking != true)
                {
                    noise.MakeNoise(5);
                } else if (isSneaking == true)
                {
                    noise.StopNoise();
                }
            }
        }
    }

    public override void RotateCounterClockwise()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = GetComponent<NoiseMaker>();
            if (mover != null)
            {
                mover.Rotate(-turnSpeed);
            }
            if (noise != null)
            {
                if (isSneaking != true)
                {
                    noise.MakeNoise(5);
                } else if (isSneaking == true)
                {
                    noise.StopNoise();
                }
            }
        }
    }

    public override void Sneak()
    {
        isSneaking = true;
        moveSpeed = sneakSpeed;
        Debug.Log("sneaking");
    }

    public override void StopSneak()
    {
        isSneaking = false;
        moveSpeed = defaultmoveSpeed;
        Debug.Log("stop sneaking");
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Debug.Log("Rotation check 1");
        Vector3 vectorToTarget = targetPosition - transform.position;
        Debug.Log("Rotation check 2");
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        Debug.Log("Rotation check 3");
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}

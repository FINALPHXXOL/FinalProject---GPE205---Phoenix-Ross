using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public abstract class Pawn : MonoBehaviour
{
    public float defaultmoveSpeed;
    public float moveSpeed;
    public float sneakSpeed;
    public float turnSpeed;
    
    public bool isPlaying = false;
    public ParticleSystem smokeScreen;
    
    public Mover mover;
    public Controller controller;
    public Image hearingCircle;

    // Start is called before the first frame update
    public virtual void Start()
    {
        moveSpeed = defaultmoveSpeed;
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void Sneak();
    public abstract void StopSneak();
    public abstract IEnumerator PlayParticleSystem();
    
}

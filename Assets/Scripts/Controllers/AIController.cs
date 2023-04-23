using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Chase, Patrol, ChooseTarget };
    public AIState currentState;
    private float lastStateChangeTime;
    public GameObject target;
    public float fleeDistance;
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;
    public float hearingDistance;
    public float viewDistance;
    public float fieldOfView;
    public bool isPatrolLoop;
    public bool isChasing;
    public Color color = Color.red;


    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null)
        {
            //if (GameManager.instance.enemyAIsSpawnTranform != null)
            // {
            GameManager.instance.enemyAIs.Add(this);
            // }
        }
        base.Start();
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            // if (GameManager.instance.enemyAIsSpawnTranform != null)
            // {
            GameManager.instance.enemyAIs.Remove(this);

            // }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeDecisions();
        base.Update();
    }

    public virtual void MakeDecisions()
    {

        switch (currentState)
        {
            case AIState.Chase:
                // Do work
                TargetNearestPirate();
                DoChaseState();
                // Check for transitions
                if (target == null || (!IsDistanceLessThan(target, (viewDistance * 1.25f)) || (!CanSee(target))) && !CanHear(target))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Patrol:
                TargetNearestPirate();
                Patrol();
                if (target != null)
                {
                    if ((IsDistanceLessThan(target, viewDistance) && (CanSee(target))) || CanHear(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                break;
            case AIState.ChooseTarget:
                TargetNearestPirate();
                if (target != null)
                {
                    if ((IsDistanceLessThan(target, viewDistance) && (CanSee(target))) || CanHear(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                if (target == null || (!CanSee(target) && !CanHear(target)))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
        }
    }

    public override void RespawnPlayer()
    {
        GameManager.instance.playerSpawnTransform = GameManager.instance.FindRandomSpawn<PlayerSpawnPoint>(GameManager.instance.playerSpawns);

        GameObject newPawnObj = Instantiate(GameManager.instance.piratePawnPrefab, GameManager.instance.playerSpawnTransform.position, GameManager.instance.playerSpawnTransform.rotation);

        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        this.pawn = newPawn;
        newPawn.controller = this;

        Debug.Log("Pirate respawned.");

    }

    public override void AddToScore(float amount)
    {

    }

    public override void RemoveScore(float amount)
    {

    }

    public override void AddLives(float amount)
    {

    }

    public override void RemoveLives(float amount)
    {

    }

    public virtual void ChangeState(AIState newState)
    {
        //Change current state
        currentState = newState;

        //Save the time when we changed states
        lastStateChangeTime = Time.time;
    }

    #region States
    public void DoSeekState()
    {
        isChasing = true;
        if (target != null)
        {


            if (target.transform.position != null)
            {
                Seek(target.transform.position);
            }
        }
    }
    protected virtual void DoChaseState()
    {
        isChasing = true;
        if (target != null)
        {


            if (target.transform.position != null)
            {
                Seek(target.transform.position);
            }

        }
    }
    protected virtual void DoIdleState()
    {
        isChasing = false;
        // Do Nothing
    }
    protected virtual void DoAttackState()
    {
        isChasing = true;
        // Chase
        if (target != null)
        {
            if (target.transform.position != null)
            {
                Seek(target.transform.position);
            }
        }

    }
    protected void Flee()
    {
        isChasing = false;
        if (pawn != null)
        {
            // Find the Vector to our target
            Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
            // Find the Vector away from our target by multiplying by -1
            Vector3 vectorAwayFromTarget = -vectorToTarget;
            // Find the vector we would travel down in order to flee
            Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

            float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
            float percentOfFleeDistance = targetDistance / fleeDistance;
            percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
            float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

            // Seek the point that is "fleeVector" away from our current position
            Seek(pawn.transform.position + fleeVector * flippedPercentOfFleeDistance);
        }

    }
    protected void Patrol()
    {
        isChasing = false;
        // If we have a enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            // If we are close enough, then increment to next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) <= waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else if (isPatrolLoop == true)
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        isChasing = false;
        // Set the index to 0
        currentWaypoint = 0;
    }
    #endregion States

    #region Behaviors
    public void Seek(Vector3 targetPosition)
    {
        isChasing = true;
        if (targetPosition != null)
        {
            pawn.RotateTowards(targetPosition);

            if (target != null)
            {
                pawn.MoveForward();
            }

        }
    }

    public void Seek(Transform targetTransform)
    {
        isChasing = true;
        if (targetTransform != null)
        {
            // Seek the position of our target Transform
            Seek(targetTransform.position);
        }
    }

    public void Seek(Pawn targetPawn)
    {
        if (targetPawn != null)
        {
            // Seek the pawn's transform!
            Seek(targetPawn.transform);
        }
    }

    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }
    protected void TargetNearestPirate()
    {
        // Get a list of all the pirates (pawns)

        if (GameManager.instance.players[0].pawn != null)
        {
            // Assume that the first pirate is closest
            Pawn closestPirate = GameManager.instance.players[0].pawn;
            if (pawn != null)
            {
                float closestPirateDistance = Vector3.Distance(pawn.transform.position, closestPirate.transform.position);

                // Iterate through them one at a time
                foreach (PlayerController player in GameManager.instance.players)
                {
                    if (player != null && player.pawn != null)
                    {
                        // If this one is closer than the closest
                        if (Vector3.Distance(pawn.transform.position, player.pawn.transform.position) <= closestPirateDistance)
                        {
                            // It is the closest
                            closestPirate = player.pawn;
                            closestPirateDistance = Vector3.Distance(pawn.transform.position, closestPirate.transform.position);
                        }
                    }
                }

                // Target the closest pirate
                target = closestPirate.gameObject;

            }
        }
    }
    #endregion Behaviors

    #region Transitions
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (IsHasTarget())
        {
            if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Debug.Log("State changed to find target");
            // ChangeState(AIState.ChooseTarget);
            return false;
        }
    }
    protected bool IsHasTarget()
    {
        // return true if we have a target, false if we don't
        return (target != null);
    }
    public bool CanHear(GameObject target)
    {
        if (pawn != null)
        {
            if (pawn.hearingCircle != null)
            {
                RectTransform rectTransform = pawn.hearingCircle.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(hearingDistance, hearingDistance);
            }
            if (IsHasTarget())
            {
                // Get the target's NoiseMaker
                NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
                // If they don't have one, they can't make noise, so return false
                if (noiseMaker == null)
                {
                    return false;
                }
                // If they are making 0 noise, they also can't be heard
                if (noiseMaker.volumeDistance <= 0)
                {
                    return false;
                }
                // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
                float totalDistance = noiseMaker.volumeDistance + hearingDistance;
                // If the distance between our pawn and target is closer than this...
                if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
                {
                    // ... then we can hear the target
                    return true;
                }
                else
                {
                    // Otherwise, we are too far away to hear them
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Debug.Log("State changed to find target");
            // ChangeState(AIState.ChooseTarget);
            return false;
        }
    }
    public bool CanSee(GameObject target)
    {
        Debug.Log("Ray1");
        if (pawn != null)
        {
            Debug.Log("Ray2");
            // We use the location of our target in a number of calculations - store it in a variable for easy access.
            Vector3 targetPosition = target.transform.position;

            

            //test
            Vector3 targetCenter = target.transform.position;

            targetCenter.y += target.transform.localScale.y / 2;

            Vector3 transformCenter = pawn.transform.position;

            transformCenter.y += pawn.transform.localScale.y / 2;

            // Find the vector from the agent to the target
            // We do this by subtracting "destination minus origin", so that "origin plus vector equals destination."
            Vector3 agentToTargetVector = targetCenter - transformCenter;

            // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
            float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

            // if that angle is less than our field of view
            if (angleToTarget < fieldOfView)
            {
                Debug.Log("Ray3");
                // Create a variable to hold a ray from our position to the target
                Ray rayToTarget = new Ray();

                // Set the origin of the ray to our position, and the direction to the vector to the target
                rayToTarget.origin = transformCenter; //transform.position
                rayToTarget.direction = agentToTargetVector;

                // Create a variable to hold information about anything the ray collides with
                RaycastHit hitInfo;

                // Cast our ray for infinity in the direciton of our ray.
                //    -- If we hit something...
                Debug.DrawRay(rayToTarget.origin, rayToTarget.direction, color, 2f, false);
                if (Physics.Raycast(rayToTarget, out hitInfo, Mathf.Infinity)) //Mathf.Infinity))
                {
                    Debug.Log("Ray4");
                    Physics.IgnoreCollision(pawn.GetComponent<Collider>(), hitInfo.collider);
                    
                    Debug.DrawLine(rayToTarget.origin, rayToTarget.direction, color, 2f, false);
                    // ... and that something is our target 
                    if (hitInfo.collider.gameObject == target)
                    {
                        Debug.Log("Ray5");
                        // return true 
                        //    -- note that this will exit out of the function, so anything after this functions like an else
                        return true;
                    }
                }
            }
        }
        Debug.Log("RayFalse");
        // return false
        //   -- note that because we returned true when we determined we could see the target, 
        //      this will only run if we hit nothing or if we hit something that is not our target.
        // Debug.Log("State changed to find target");
        //  ChangeState(AIState.ChooseTarget);
        return false;

    }
    #endregion Transitions

}
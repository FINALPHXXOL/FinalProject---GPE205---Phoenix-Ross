using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICaptain : AIController
{
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

    public override void MakeDecisions()
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
                        if (AudioManager.instance != null)
                        {
                            AudioManager.instance.PlaySpottedSound();
                        }
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
                        if (AudioManager.instance != null)
                        {
                            AudioManager.instance.PlaySpottedSound();
                        }
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
}

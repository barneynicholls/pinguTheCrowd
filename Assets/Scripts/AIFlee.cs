using System;
using UnityEngine;
using UnityEngine.AI;

public class AIFlee : MonoBehaviour
{
    [SerializeField]
    private Transform goal;
    [SerializeField]
    private float detectionRadius = 5f;
    [SerializeField]
    private float fleeRadius = 20f;

    [SerializeField]
    private float fleeSpeed = 15f;
    [SerializeField]
    private float fleeAngularSpeed = 300f;
    [SerializeField]
    private float agentSpeed = 3f;
    [SerializeField]
    private float angularSpeed = 120f;

    [SerializeField]
    [Range(1f,5f)]
    private float speedRangeMultiplier;

    private NavMeshAgent agent;
    private DateTime fleeTime; 
    private bool isFleeing;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = agentSpeed * UnityEngine.Random.Range(1f, speedRangeMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFleeing)
        {
            if (agent.remainingDistance < 1 || (DateTime.Now - fleeTime) > TimeSpan.FromSeconds(5))
            {
                Debug.Log("reached flee point");

                isFleeing = false;
                ResetToGoal();
            }
        }
        else
        {
            agent.SetDestination(goal.position);
        }
    }

    void ResetToGoal()
    {
        agent.ResetPath();
        agent.speed = agentSpeed * UnityEngine.Random.Range(1f, speedRangeMultiplier);
        agent.angularSpeed = angularSpeed;
        agent.SetDestination(goal.position);
    }

    internal void flee(Vector3 point)
    {
        if(isFleeing)
        {
            Debug.Log("already fleeing");
            return;
        }

        Debug.Log("flee");
        var distance = Vector3.Distance(point, transform.position);
        if (distance > detectionRadius)
        {
            Debug.Log("too far away");
            return;
        }

        var fleeDirection = (transform.position - point).normalized;
        var newGoal = transform.position + fleeDirection * fleeRadius;

        var path = new NavMeshPath();
        agent.CalculatePath(newGoal, path);

        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("fleeing");
            isFleeing = true;
            fleeTime = DateTime.Now;
            //agent.ResetPath();
            agent.speed = fleeSpeed;
            agent.angularSpeed = fleeAngularSpeed;
            agent.SetDestination(path.corners[path.corners.Length-1]);
        } else
        {
            Debug.Log("invalid path");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isFleeing ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

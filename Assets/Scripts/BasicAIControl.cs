using UnityEngine;
using UnityEngine.AI;

public class BasicAIControl : MonoBehaviour
{
    public GameObject target;
    private Transform targetTransform;
    NavMeshAgent agent;
    Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (target != null)
            agent.SetDestination(targetTransform.position);
    }

    private void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
            rb.velocity = agent.desiredVelocity;
        else
            rb.velocity = Vector3.zero;
    }
}
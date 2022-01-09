using UnityEngine;
using UnityEngine.AI;

public class SetRandomNavmeshPath : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("SetNewPath", 1f, Random.Range(3f, 6f));
    }

    void SetNewPath()
    {
        var randomPointAround = Random.insideUnitCircle * 50f;
        var targetPosition = transform.position + new Vector3(randomPointAround.x, 0, randomPointAround.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 4f, NavMesh.AllAreas))
        {
            agent.destination = targetPosition;
        }
    }
}
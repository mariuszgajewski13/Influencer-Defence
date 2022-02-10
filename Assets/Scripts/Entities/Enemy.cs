using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : EntityBase
{

    public GameObject target;
    private Transform targetTransform;
    NavMeshAgent agent;
    Rigidbody rb;

    private Transform playerTransform;
    [SerializeField] GameObject player;
    [SerializeField] float detectionRange = 6f;

    Vector3 verticalOffset = new Vector3(0, 1f, 0);
    AICharacterControl ai;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();
        slider = GetComponentInChildren<Slider>();
        InvokeRepeating("SetNewPath", 1f, Random.Range(3f, 6f));

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        agent = GetComponent<NavMeshAgent>();

        ai = GetComponent<AICharacterControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        InvokeRepeating("DetectTick", 1f, 1f);
    }

    private void Update()
    {
        UpdateHealthBar();

        if (agent.remainingDistance > agent.stoppingDistance)
            rb.velocity = agent.desiredVelocity;
        else
            rb.velocity = Vector3.zero;
    }


    void UpdatePath()
    {
        if (target != null)
            agent.SetDestination(targetTransform.position);
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

    void DetectTick()
    {
        var offsetedPosition = transform.position + verticalOffset;
        var direction = ((playerTransform.position + verticalOffset) - offsetedPosition).normalized * detectionRange;

        Debug.DrawRay(offsetedPosition, direction, Color.red, 10f);

        if (Physics.Raycast(offsetedPosition, direction, LayerMask.NameToLayer("Player")))
        {
            Debug.Log(string.Format("<color=white><b>{0}</b></color>", "Hit!"));
            ai.target = playerTransform;
        }
    }
}

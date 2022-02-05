using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject player;
    [SerializeField] float detectionRange = 6f;

    Vector3 verticalOffset = new Vector3(0, 1f, 0);
    AICharacterControl ai;

    private void Start()
    {
        ai = GetComponent<AICharacterControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        InvokeRepeating("DetectTick", 1f, 1f);
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
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float detectionRange = 6f;

    Vector3 verticalOffset = new Vector3(0, 1f, 0);
    AICharacterControl ai;

    private void Start()
    {
        ai = GetComponent<AICharacterControl>();

        InvokeRepeating("DetectTick", 1f, 1f);
    }

    void DetectTick()
    {
        var offsetedPosition = transform.position + verticalOffset;
        var direction = ((player.position + verticalOffset) - offsetedPosition).normalized * detectionRange;

        Debug.DrawRay(offsetedPosition, direction, Color.red, 10f);

        if (Physics.Raycast(offsetedPosition, direction, LayerMask.NameToLayer("Player")))
        {
            Debug.Log(string.Format("<color=white><b>{0}</b></color>", "Hit!"));
            ai.target = player;
        }
    }
}
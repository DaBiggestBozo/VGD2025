using NUnit.Framework.Constraints;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject playerObject;

    public Vector3 closedPosition = new Vector3(0f, 0f, 0f);
    public Vector3 openPosition = new Vector3(0f, 5f, 0f);
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = closedPosition;
    }

    void Update()
    {
        door.transform.localPosition = Vector3.Lerp(targetPosition, door.transform.localPosition, 0.9f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject) 
        {
            targetPosition = openPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            targetPosition = closedPosition;
        }
    }
}

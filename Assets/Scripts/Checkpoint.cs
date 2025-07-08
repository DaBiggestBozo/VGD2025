using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject checkpointObject;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            print("checkpoint");
            checkpointObject.SetActive(false);
        }
    }
}

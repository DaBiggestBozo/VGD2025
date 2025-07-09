using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject checkpointObject;
    [SerializeField] private GameObject spawnpointObject;
    private PlayerHealth playerHealth;
    private bool checkpointLoaded = false;

    private void Awake()
    {
        checkpointLoaded = false;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        print(playerHealth.spawnLocation);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            PlayerPrefs.SetFloat("Checkpoint X", spawnpointObject.transform.position.x);
            PlayerPrefs.SetFloat("Checkpoint Y", spawnpointObject.transform.position.y);
            PlayerPrefs.SetFloat("Checkpoint Z", spawnpointObject.transform.position.z);
            PlayerPrefs.SetFloat("Checkpoint Rotation X", spawnpointObject.transform.rotation.x);
            PlayerPrefs.SetFloat("Checkpoint Rotation Y", spawnpointObject.transform.rotation.y);
            PlayerPrefs.SetFloat("Checkpoint Rotation Z", spawnpointObject.transform.rotation.z);
            PlayerPrefs.Save();
            playerHealth.currentHealth = playerHealth.maxHealth; //Heal player
            checkpointObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!checkpointLoaded)
        {
            player.transform.position = playerHealth.spawnLocation;
            //player.transform.rotation = Quaternion.LookRotation(playerHealth.spawnRotation); //idk low priority
            if (player.transform.position == playerHealth.spawnLocation) //doesnt check rotation but oh well
            {
                checkpointLoaded = true;
            }
        }
    }
}

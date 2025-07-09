using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private GameObject DeathScreen;
    public Vector3 spawnLocation; //Change this to change default spawn location
    public Vector3 spawnRotation; //Spawn angle
    public Vector3 defaultSpawnLocation; //set in inspector

    [SerializeField] private AudioSource deathSource;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        currentHealth = maxHealth;
        totalHealthBar.fillAmount = maxHealth;
        spawnLocation = defaultSpawnLocation;

        if(PlayerPrefs.GetString("Respawning") == "true")
        {
            spawnLocation = new Vector3(PlayerPrefs.GetFloat("Checkpoint X"), PlayerPrefs.GetFloat("Checkpoint Y"), PlayerPrefs.GetFloat("Checkpoint Z"));
            spawnRotation = new Vector3(PlayerPrefs.GetFloat("Checkpoint Rotation X"), PlayerPrefs.GetFloat("Checkpoint Rotation Y"), PlayerPrefs.GetFloat("Checkpoint Rotation Z"));
            PlayerPrefs.SetString("Respawning", "false");
        } else
        {
            spawnLocation = defaultSpawnLocation;
            PlayerPrefs.SetFloat("Checkpoint X", defaultSpawnLocation.x);
            PlayerPrefs.SetFloat("Checkpoint Y", defaultSpawnLocation.y);
            PlayerPrefs.SetFloat("Checkpoint Z", defaultSpawnLocation.z);
        }
    }
    private void Start()
    {
        transform.position = spawnLocation;
    }

    private void Update()
    {
        //currentHealthBar.fillAmount = currentHealth/maxHealth;
        currentHealthBar.fillAmount = Mathf.Lerp(currentHealthBar.fillAmount, currentHealth / maxHealth, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Bullet")
        {
            Destroy(other.gameObject);
            DamagePlayer(15);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Kill Player")
        {
            KillPlayer();
        }
    }

    public void DamagePlayer(int damage) //damage is amount of damage to deal
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            //Add damage flash?
        }
        else
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        DeathScreen.SetActive(true);
        deathSource.PlayOneShot(deathSound);
    }
}

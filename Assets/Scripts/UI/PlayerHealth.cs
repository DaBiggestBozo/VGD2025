using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private GameObject DeathScreen;

    private void Awake()
    {
        currentHealth = maxHealth;
        totalHealthBar.fillAmount = maxHealth;
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

    public void DamagePlayer(int damage) //damage is amount of damage to deal
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            print("Oof");
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
    }
}

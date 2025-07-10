using System.Runtime.CompilerServices;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private float PunchCooldown = 0.75f;
    private float PunchTimer = 0f;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private LayerMask hitable; //things that stop a punch (or get hit by it)
    [SerializeField] private GameObject player;
    private PlayerHealth playerHP;
    public int PunchDamage = 15;
    public int PunchHealing = 20;
    //[SerializeField] private BoxCollider damageBox;
    //[SerializeField] private Transform capsuleStart;
    //[SerializeField] private Transform capsuleEnd;
    //[SerializeField] private float punchWidth = 0.5f;
    //Cut ideas to save time, doing raycast

    private void Awake()
    {
        playerHP = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckPunch();
        }
        PunchTimer += Time.deltaTime;
    }

    private void StartPunch()
    {
        PunchTimer = 0f;
        anim.SetTrigger("Punch");
        Invoke("PunchHit", 0.05f); //delay for animation
    }

    private void PunchHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 3f, hitable)) //3f is punch distance
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyAI>().TakeDamage(PunchDamage);
                playerHP.DamagePlayer(-PunchHealing);
            }

            if (hit.collider.gameObject.CompareTag("Enemy Bullet")) //destroy bullets you punch, maybe add parrying later
            {
                Destroy(hit.collider.gameObject);
            }
        }
        //if (Physics.CapsuleCast(capsuleStart.position, capsuleEnd.position, punchWidth, playerCam.transform.forward, out hit, 3f, hitable))
        //{
        //    print(hit);
        //}
    }

    private void CheckPunch()
    {
        if (PunchTimer >= PunchCooldown)
        {
            StartPunch();
        }
    }
}

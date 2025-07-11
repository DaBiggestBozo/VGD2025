using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shooting : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public GameObject muzzleFlash, bulletHoleGraphic;
    public GameObject Gun;
    public GameObject Light;

    public Transform cameraPosition;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask whatIsEnemy;

    bool shooting, readyToShoot, Reloading;

    public CameraShake cs;
    public float camShakeMagnitude, camShakeDuration;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject deathScreen;

    [SerializeField] GameObject player;

    Animator anim;

    public AudioSource Gunshotsource;
    public AudioClip GunshotSound;

    private void Awake()
    {
        readyToShoot = true;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !pauseMenu.activeInHierarchy && !settingsMenu.activeInHierarchy)
        {
            BulletHoleImpact();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && shooting)
        {
            anim.SetBool("Fired", true);
        }

        else
        {
            anim.SetBool("Fired", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && shooting && !pauseMenu.activeInHierarchy && !settingsMenu.activeInHierarchy)
        {
            StartCoroutine(StartFlash());
        }

    }



    public IEnumerator StartFlash()
    {
        Light.SetActive(!Light.activeSelf);
        yield return new WaitForSeconds(0.2f);
        Light.SetActive(!Light.activeSelf);
    }

    private void MyInput()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (!pauseMenu.activeInHierarchy && !settingsMenu.activeInHierarchy && !deathScreen.activeInHierarchy)
        {
            if (allowButtonHold)
            {
                shooting = Input.GetKey(KeyCode.Mouse0);
            }
            else
            {
                shooting = Input.GetKeyDown(KeyCode.Mouse0);
            }
        }

        if (readyToShoot && shooting && !pauseMenu.activeInHierarchy && !settingsMenu.activeInHierarchy && !deathScreen.activeInHierarchy)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Gunshotsource.PlayOneShot(GunshotSound);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, whatIsEnemy))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
                if (Vector3.Distance(player.transform.position, hit.collider.transform.position) <= 4)
                {
                    player.GetComponent<PlayerHealth>().DamagePlayer(-11); //heal player
                }
            }
        }



        readyToShoot = false;

        Invoke("ResetShot", timeBetweenShooting);

        cs.Shake(camShakeDuration, camShakeMagnitude);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !pauseMenu.activeInHierarchy && !settingsMenu.activeInHierarchy)
        {
            StartCoroutine(cs.Shake(0.15f, 0.4f));
        }

        //GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        //Destroy(flash, 0.1f);

    }

    private void BulletHoleImpact()
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            GameObject impac = Instantiate(bulletHoleGraphic, hit.point, Quaternion.LookRotation(hit.normal));

            Vector3 forwardVecto = impac.transform.forward;

            impac.transform.Translate(forwardVecto * 0.1f, Space.World);

            Destroy(impac, 0.3f);
        } else
        {
            GameObject impact = Instantiate(bulletHoleGraphic, hit.point, Quaternion.LookRotation(hit.normal));

            Vector3 forwardVector = impact.transform.forward;

            impact.transform.Translate(forwardVector * 0.1f, Space.World);

            Destroy(impact, 15f);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public GameObject muzzleFlash, bulletHoleGraphic;
    public GameObject Gun;

    public Transform cameraPosition;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask whatIsEnemy;

    bool shooting, readyToShoot, Reloading;

    public CameraShake cs;
    public float camShakeMagnitude, camShakeDuration;

    [SerializeField] GameObject pauseMenu;

    Animator anim;

    private void Awake()
    {
        readyToShoot = true;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();

        if (Input.GetKey(KeyCode.Mouse0) && !hit.collider.CompareTag("Enemy"))
        {
            BulletHoleImpact();
        }

        if (Input.GetKey(KeyCode.Mouse0) && shooting)
        {
            anim.SetBool("Fired", true);
        }

        else
        {
            anim.SetBool("Fired", false);
        }
    }

    IEnumerator StartRecoil()
    {
        Gun.GetComponent<Animator>().Play("New State");
        yield return new WaitForSeconds(1f);
        Gun.GetComponent<Animator>().Play("Recoils");
    }

    private void MyInput()
    {
        if (!pauseMenu.activeInHierarchy)
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

        if (readyToShoot && shooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, whatIsEnemy))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {

                hit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
            }
        }

        readyToShoot = false;

        Invoke("ResetShot", timeBetweenShooting);

        cs.Shake(camShakeDuration, camShakeMagnitude);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(cs.Shake(0.15f, 0.4f));
        }

        //GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        //Destroy(flash, 0.1f);

    }

    private void BulletHoleImpact()
    {
        GameObject impact = Instantiate(bulletHoleGraphic, hit.point, Quaternion.LookRotation(hit.normal));

        Vector3 forwardVector = impact.transform.forward;

        impact.transform.Translate(forwardVector * 0.1f, Space.World);

        Destroy(impact, 20f);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
}

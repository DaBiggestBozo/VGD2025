using System.Collections;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Animator animator;
    public AnimationClip Recoils;
    public AnimationClip GunWalking;
    public GameObject Gun;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(StartRecoil());
        }

        if (Input.GetKey(KeyCode.W))
        {
            StartCoroutine(StartWalking());
        }
    }

    IEnumerator StartWalking()
    {
        Gun.GetComponent<Animator>().Play("Recoils");
        yield return new WaitForSeconds(0.70f);
        Gun.GetComponent<Animator>().Play("GunWalking");
    }

    IEnumerator StartRecoil()
    {
        Gun.GetComponent<Animator>().Play("Recoils");
        yield return new WaitForSeconds(0.20f);
        Gun.GetComponent<Animator>().Play("Idle");
    }
}

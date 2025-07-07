using System.Collections;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Animator animator;
    public AnimationClip Recoils;
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
    }

    IEnumerator StartRecoil()
    {
        Gun.GetComponent<Animator>().Play("Recoils");
        yield return new WaitForSeconds(0.20f);
        Gun.GetComponent<Animator>().Play("Idle");
    }
}

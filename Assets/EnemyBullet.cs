using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
private void OnTriggerEnter(Collider other)

    {

        if (other.tag != "Player")

        {

            Destroy(gameObject);

        }

    }
}   
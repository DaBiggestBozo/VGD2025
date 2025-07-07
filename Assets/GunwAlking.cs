using UnityEngine;

public class GunwAlking : StateMachineBehaviour
{
    public bool Fired;
    public bool Walking;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fired = true;
        }

        else
        {
            Fired = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Walking = true;
        }

        else
        {
            Walking = false;
        }
    }
}

using JetBrains.Annotations;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class recoill : StateMachineBehaviour
{
    public bool Fired;

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
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public WallRunning wallrun;

    public float sensX;
    public float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    private float tiltInterpolate;

    [SerializeField] private float tiltSpeed = 0.2f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Mouse inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        tiltInterpolate = Mathf.LerpAngle(tiltInterpolate, wallrun.tilt, tiltSpeed);

        //Camera rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, tiltInterpolate);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void GetSens()
    {
        if (PlayerPrefs.HasKey("sensitiviyX"))
        {
            sensX = PlayerPrefs.GetFloat("sensitiviyX");
        }
        if (PlayerPrefs.HasKey("sensitiviyY"))
        {
            sensX = PlayerPrefs.GetFloat("sensitiviyY");
        }
    }
}

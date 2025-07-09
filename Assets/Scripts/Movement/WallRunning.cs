using UnityEngine;
using static Unity.VisualScripting.Member;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    [SerializeField] private Camera cam;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    public bool upwardsRunning;
    public bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;

    [Header("Audio")]
    public AudioSource Jumpsource;
    public AudioClip JumpSound;
    public AudioClip WallRunSound;
    public AudioSource WallRunSource;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (pm.wallrunning)
        {
            WallRunningMovement();
        }
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (!pm.wallrunning)
            {

                StartWallRun();
            }

            if (wallRunTimer > 0)
            {
                wallRunTimer -= Time.deltaTime;
            }

            if (wallRunTimer <= 0 && pm.wallrunning)
            {
                StopWallRun();
            }

            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }

        else
        {
            if (pm.wallrunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {

        WallRunSource.clip = WallRunSound;
        WallRunSource.Play();
        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }

        else if (wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        pm.wallrunning = true;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        wallRunTimer = maxWallRunTime;

        //fov = 110f;
    }

    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //Moving The Player Forward
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // Upwards/Downwards Force
        if (upwardsRunning)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, wallClimbSpeed, rb.linearVelocity.z);
        }

        if (downwardsRunning)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -wallClimbSpeed, rb.linearVelocity.z);
        }

        //Pushing The Player To The Wall
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        //Weakening Gravity During Wallrun
        if (useGravity)
        {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
    }

    private void StopWallRun()
    {
        //fov = 90f;
        tilt = 0f;
        pm.wallrunning = false;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
        WallRunSource.Stop();
    }

    private void WallJump()
    {
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        Jumpsource.PlayOneShot(JumpSound);
    }
}

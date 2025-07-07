using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsClimbable;
    public PlayerMovement pm;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    public float climbTimer;

    public bool climbing;

    [Header("ClimbJump")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;

    public KeyCode jumpKey = KeyCode.Space;
    public int climbJumps;
    private int climbJumpsLeft;

[Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastwall;
    private Vector3 lastwallNormal;
    public float minWallNormalAngleChange;

    [Header("ExitingWall")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsClimbable);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        if (pm.grounded)
        {
            climbTimer = maxClimbTime;
        }

        bool newWall = frontWallHit.transform != lastwall || Mathf.Abs(Vector3.Angle(lastwallNormal, frontWallHit.normal)) > minWallNormalAngleChange;

        if((wallFront && newWall) || pm.grounded)
        {
            climbTimer = maxClimbTime;
            climbJumpsLeft = climbJumps;
        }
    }

    void Start()
    {
        
    }


    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing && !exitingWall)
        {
            ClimbingMovement();
        }
    }

    private void StateMachine()
    {
        //Stage 1 - Climbing
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && !exitingWall)
        {
            if (!climbing && climbTimer > 0)
            {
                StartClimbing();
            }

            //Timer
            if (climbTimer > 0)
            {
                climbTimer -= Time.deltaTime;
            }

            if (climbTimer < 0)
            {
                StopClimbing();
            }
        }

        //Stage 2 - Exiting The Wall
        else if (exitingWall)
        {
            if (climbing)
            {
                StopClimbing();
            }

            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }

            if (exitWallTimer < 0)
            {
                exitingWall = false;
            }
        }

        //State 3 - None
        else
        {
            if (climbing)
            {
                StopClimbing();
            }
        }

        if (wallFront && Input.GetKeyDown(jumpKey) && climbJumpsLeft > 0)
        {
            ClimbJump();
        }
    }

    private void StartClimbing()
    {
        climbing = true;
        //pm.climbing = true;

        lastwall = frontWallHit.transform;
        lastwallNormal = frontWallHit.normal;
    }

    private void ClimbingMovement()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, rb.linearVelocity.z);
    }

    private void StopClimbing()
    {
        climbing = false;
        //pm.climbing = false;
    }

    private void ClimbJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climbJumpsLeft--;
    }
}

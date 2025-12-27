using UnityEngine;

public class TurtleController : MonoBehaviour
{
    [Header("Turtle State")]
    public PhysicsState physicsState = PhysicsState.Grounded;
    public GroundedBehavior groundedBehavior = GroundedBehavior.Idle;

    [Header("Settings")]
    public float minChangeTime = 10f;
    public float maxChangeTime = 20f;
    public float walkingSpeed = 0.2f;
    public float runningSpeed = 0.6f;
    public float turningSpeed = 50f;


    private float timer=1;
    private Animator animator;
    private Quaternion targetRotation;
    public bool followMouse = false;

    public enum PhysicsState
    {
        Launched,
        Grounded
    }
    public enum GroundedBehavior
    {   
        None,
        Moving,
        Idle,
        Turning
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        SelectGroundBehavior();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        //Debug.Log("Timer: " + timer);
        if (timer <= 0f && groundedBehavior != GroundedBehavior.Turning &&physicsState == PhysicsState.Grounded)
        {
            SelectGroundBehavior();
            animator.SetBool("isIdle", groundedBehavior == GroundedBehavior.Idle);
        }

        if(groundedBehavior == GroundedBehavior.Moving)
        {
            Moveforward();
        }

        if(groundedBehavior == GroundedBehavior.Turning)
        {
            TurnAround();
        }
        //Debug.Log("Current Behavior: " + groundedBehavior);
        //Debug.Log("targetRotation: " + targetRotation.eulerAngles); 

        if (followMouse)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(1))
            {
                Toggle();
            }
        }

    }

    void TurnAround()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            transform.rotation = targetRotation;
            SelectGroundBehavior();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (groundedBehavior == GroundedBehavior.Turning) return;
        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            groundedBehavior = GroundedBehavior.Turning;
            targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
        }

    }

// Ground Behavior
    void SelectGroundBehavior() // if isGrounded = true
    {   
        timer = Random.Range(minChangeTime, maxChangeTime);
        if (Random.value > 0.6f) // randomly choose idle or moving
        {
            groundedBehavior = GroundedBehavior.Idle;
            int idleIdx = Random.Range(1, 4);
            animator.SetInteger("idleIndex", idleIdx);
        }
        else
        {
            groundedBehavior = GroundedBehavior.Moving;
        }
    }
    void Moveforward()
    {
        transform.Translate(Vector3.forward * walkingSpeed * Time.deltaTime);
    }

    public void IdleTrue()
    {
        animator.SetBool("idleTrigger", true);
    }
    public void IdleFalse()
    {
        animator.SetBool("idleTrigger", false);
    }
    

    void FollowMouse()
    {
        Vector3 mouseScreenPos = Input.mousePosition;  // screen position
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; // set z to screen postion
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);       // turn to world position
        worldPos.y = transform.position.y; // keep y the same
        worldPos.z = transform.position.z; // keep z the same
        transform.LookAt(worldPos);
        transform.Translate(Vector3.forward * runningSpeed * Time.deltaTime);
    }

    public void Toggle()
    {
        followMouse = !followMouse;
        animator.SetBool("follow", followMouse);
    }

}

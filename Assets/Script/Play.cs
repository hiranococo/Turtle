using TMPro;
using UnityEngine;

public class Play : MonoBehaviour
{
    public bool isPlaying;
    private Animator animator;
    private TurtleController turtleController;
    public GameObject bar;
    private Vector3 relativePos;
    public Bar barScript;
    public ScreenEdgeColliders screenEdgeColliders;
    private Rigidbody rb;


    public GameObject playBar;
    //public bool avoidToolClose = false;


    public void PlayTrigger()
    {
        if (coolDownTime.isCooling) 
        {return;
        //avoidToolClose = true;
        }
        else
        {
            isPlaying = true;
            playBar.SetActive(true);
            turtleController.groundedBehavior = TurtleController.GroundedBehavior.None;
            turtleController.physicsState = TurtleController.PhysicsState.Launched;
            animator.SetBool("isIdle", true);
            animator.SetBool("idleTrigger", true);
            animator.SetInteger("idleIndex",1);
            
            //animator.SetBool("isGrounded", false);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        turtleController = GetComponent<TurtleController>();
        relativePos = transform.position - bar.transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public float turtleSpeed;
    void Update()
    {
        bar.transform.position = transform.position - relativePos;
        if(isAiming)
        {
            barScript.holdTimer += Time.deltaTime;
        }
        CheckIfStopped();
        
    }

public float stopThreshold = 10f;
private float lowSpeedTimer = 0f;
public CoolDownTime coolDownTime;
    void CheckIfStopped()
    {
        if (!animator.GetBool("isGrounded"))
        {
            turtleSpeed = rb.velocity.sqrMagnitude;
            animator.SetFloat("velocity", turtleSpeed);
            if(turtleSpeed < stopThreshold)
            {
                lowSpeedTimer += Time.deltaTime;
                if (lowSpeedTimer >= 0.5f)
                {
                    screenEdgeColliders.ResetWallPhysics();
                    animator.SetBool("isGrounded", true);
                    animator.SetBool("isPlaying", false);
                    isPlaying = false;
                    lowSpeedTimer = 0f;
                    turtleController.groundedBehavior = TurtleController.GroundedBehavior.Idle;
                    turtleController.physicsState = TurtleController.PhysicsState.Grounded;
                    isCounting = false;
                    colliderCount = 0;
                    scoreScript.Reset();
                    coolDownTime.ResetCooldown();
                }
            }
        }
    }

    private bool isAiming = false;
    void OnMouseDown()
    {
        if (isPlaying)
        {
            isAiming = true;
            animator.SetBool("isPlaying", true);
        }
        
    }

    void OnMouseUp()
    {
        if (isPlaying)
        {
            isAiming = false;
            animator.SetBool("isGrounded",false);
            screenEdgeColliders.BounceWall();
            Launch();
            bar.SetActive(false);
            isCounting = true;
        }
    }
    
public GameObject pointer;
public float launchForce = 50f;
    void Launch()
    {
        if (barScript.currentPointerAngle > barScript.greenMin && barScript.currentPointerAngle < barScript.greenMax)
        {
            launchForce *= 1.5f; // in green zone, increase launch force by 50%
        }
        Vector3 launchAngle = pointer.transform.up;
        rb.AddForce(launchAngle * launchForce, ForceMode.Impulse);
        launchForce = 50f; // reset launch force
    }
public int colliderCount = 0;
private bool isCounting = false;
public Score scoreScript;
    void OnCollisionEnter(Collision collision)
    {
        if (isCounting)
        {
            scoreScript.gameObject.SetActive(true);
            colliderCount++;
            GameManager.Instance.money++;
            scoreScript.gameObject.GetComponent<TextMeshProUGUI>().text = colliderCount.ToString();
            scoreScript.Enlarge();
        }
    }
}
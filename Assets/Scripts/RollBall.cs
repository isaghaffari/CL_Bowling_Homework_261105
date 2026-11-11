using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RollBall : MonoBehaviour
{
    private Rigidbody rigidBody;
    private InputAction bowlButt;
    public float rollPower;
    
    //in games you want to have discrete states.
    private bool holdingButt;
    private bool roll;

    public bool isCharging = false;
    public float launchPower;
    public float minLaunchPower = 100f;
    public float maxLaunchPower = 400f;
    public float maxChargeTimer = 3f;
    private float chargeTimer = 0;
    
    //this is for locking the child light's rotation THIS DIDNT WORK!!!!
    //public GameObject childLight;

    [SerializeField] TextMeshProUGUI score_txt;
    private int score = 0;

    public LayerMask personLayer;

    [SerializeField] public GameManager gameManager;
    
    //"VOID" FUNCTIONS means "RETURN NOTHING" ...  like a literal void.
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //rigidBody.AddForce(transform.forward * 1000);
        bowlButt = InputSystem.actions.FindAction("Bowl");

        //giving the ball movement, or "force"
        //everything in unity has a forward direction. regardless of where we're rotated, there is a forward...
        //...When we say lowercase "transform", we are referring to the literal transforms (position, rotation, scale)...
        //...and one of those is the direction we're facing. so, ".forward" is a special thing inside of transform...
        //...that has a "magical arrow" that is always facing forward. so, if you change the object's rotation,...
        //...the direction of "forward" will change.
    }

    // Update should have NO physics stuff in it. Only Fixed update for physics please!!!
   void Update()
   {
        
        //THIS WAS CLASS STUFF!
        if (bowlButt.WasPressedThisFrame())
        
        {
            holdingButt = true;
            isCharging = true;
        }
       else if (bowlButt.WasReleasedThisFrame())
       {
            holdingButt = false;
            roll = true;
            gameManager.hasBegunRolling = true;
            isCharging = false;
       }

       if (isCharging == true)
       {
           UpdateCharge();
       }

       //THIS DIDNT WORK!!!!!
       //childLight.transform.rotation = Quaternion.Euler(0, 0, 0);

   }

   void UpdateCharge()
   {
       float launchPowerRange = maxLaunchPower - minLaunchPower;
       chargeTimer += Time.deltaTime;
       chargeTimer = Mathf.Clamp(chargeTimer, 0, maxChargeTimer);
       float percentageCharge = chargeTimer / maxChargeTimer;
       launchPower = minLaunchPower + (launchPowerRange * percentageCharge); 
   }
   
   // void OnCollisionEnter(Collision collision)
   // {
   //     if (collision.gameObject.tag == "Pin")
   //     {
   //         score++;
   //         score_txt.text = score.ToString();
   //     }
   // }
    
   void StartRoll()
   {
       Vector3 pointTo = FindDirection();

       if (Physics.Raycast(transform.position, pointTo, 1000f, personLayer))
       {
           Debug.Log("Aiming at a person");
       }

       rigidBody.isKinematic = false;
       rigidBody.AddForce(launchPower * transform.TransformDirection(pointTo), ForceMode.Impulse);
       roll = false;
   }
   
    //changed "void" to "vector3" because we want to return something, not nothing
    Vector3 FindDirection()
    {
        //you are creating and then finding the Z location of mouse, which doesn't exist unless you do this!!!
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f;
        
        //screen to world point translates function from Screen to World
        Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition);
        direction.z = 10f;
        direction.y = 0;
        return direction.normalized;
    }

    void FixedUpdate()
    {
        if (holdingButt && rigidBody.linearVelocity == Vector3.zero)
        {
            //find the direction the mouse is pointing towards
            Debug.DrawRay(transform.position, FindDirection() * 1000f, Color.red);
            //Debug.DrawLine(transform.position, FindDirection() * 1000f, Color.red);
        }

        if (roll)
        {
            StartRoll();
            //roll the ball based on the mouse's direction
        }
    }
}
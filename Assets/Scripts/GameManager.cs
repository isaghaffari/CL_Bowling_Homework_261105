using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] private RollBall playerBall;
    
    public bool hasBegunRolling = false;
    private Rigidbody playerRB;
    
    // these will help make sure that ball has started rolling before it can stop
    private bool hasStartedMoving = false;
    private bool resetQueued = false;
    private float stillTimer = 0f;
    [SerializeField] private float resetDelay = 3f;
    
    private int score = 0;
    [SerializeField] TextMeshProUGUI score_txt;

    
    void Start()
    {
        playerRB = playerBall.GetComponent<Rigidbody>();
        hasBegunRolling = false;
}
    void Update()
    {
        //Debug.Log(hasBegunRolling);
        
        
        //this is telling it to do nothing until ball has been told to move (aka pressed and released)
        //and stop once a reset is queued (aka the 3 seconds)
        if (!hasBegunRolling || resetQueued) return;

        if (!hasStartedMoving)
        {
            //now once the ball has started rolling, "has started moving" becomes true
            if (playerRB.linearVelocity.magnitude > 0.5f) hasStartedMoving = true;
            return; // i still dont fully understand return
        }
        //
        if (Math.Abs(playerRB.linearVelocity.magnitude) <= 0.1f)
        {
            stillTimer += Time.deltaTime;
            if (stillTimer >= resetDelay)
            {
                //count down for 3 seconds once it has become still
                hasBegunRolling = false;
                QueueReset();
            }
      
        }
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("ResetLevel", 3f);
        }
        
    }

   public void PinHasFallen()
    {
        score++;
        score_txt.text = score.ToString();
    
    }

    public void BallInGutter()
    {
        QueueReset();
    }

    void QueueReset()
    {
        if (resetQueued) return;
        resetQueued = true;
        Invoke("ResetLevel", resetDelay);
    }

    void ResetLevel()
    {
        SceneManager.LoadScene("BowlingGame");
    }
    
    //void ResetLevel()
    //{
        //SceneManager.LoadScene("BowlingGame");
    //}
    
    
}
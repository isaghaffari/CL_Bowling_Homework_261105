using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject bowlingBall;
    public GameObject bowlingPins;
    public GameObject bowlingPinsPrefab;
    private Vector3 startingPosition = new Vector3(0f, -0.15f, -10f);
    public float currentTimer = 0f;
    public float pinTimer = 0f;
    public bool activeTimer = false;
    public bool resetTimer = false;
    public float points;

    public float publicScore = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {publicScore}";
        
        if (activeTimer)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= 2.5f)
            {
                BallReset();
            }
        }

        if (points >= 10f)
        {
            PinsReset();
        }

        if (resetTimer)
        {
            pinTimer += Time.deltaTime;
            if (pinTimer >= 2.5f)
            {
                GameObject bowlingPins = Instantiate(bowlingPinsPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
                pinTimer = 0f;
                resetTimer = false;
            }
        }
    }
    void BallReset()
    {
        Debug.Log("Ball Reset");
        transform.position = startingPosition;
        activeTimer = false;
        currentTimer = 0f;
    }

    void PinsReset()
    {
        resetTimer = true;
        Debug.Log("Pins Reset");
        points = 0f;
    }
}

using UnityEngine;

public class BowlingPinCheck : MonoBehaviour
{
    private float currentTimer;
    private bool isActive;
    public GameManager myManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Something Left)");
        if (other.CompareTag("Pins"))
        {
            myManager.points++;
            myManager.publicScore++;
            Debug.Log(myManager.publicScore);
            GameObject pinObject = other.gameObject;
            Destroy(pinObject);
        }
    }
}

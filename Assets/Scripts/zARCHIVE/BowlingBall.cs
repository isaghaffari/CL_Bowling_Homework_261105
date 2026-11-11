using UnityEngine;
using TMPro;

public class BowlingBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0));
        {
            Debug.Log("Button pressed");
        }
        
        if (Input.GetMouseButtonUp(0))
            {
            Debug.Log("Button pressed");
            }
    }
    
    public bool pinHit;
    private int score = 0;
    
   [SerializeField] private TextMeshProUGUI score_txt;

   
}

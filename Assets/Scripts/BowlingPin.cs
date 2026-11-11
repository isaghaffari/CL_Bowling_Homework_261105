using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool hasFallen = false;
    [SerializeField] private float fallAngle = 20f;


    
    void Update()
    {
        //if ANGLE CHANGER!!!! is true, the "has fallen" is true. we will deal with the rest in Game Manager
        if (CheckHasFallen() == true && hasFallen == false)
        {
            //we need this variable so that we have a way to turn it to false after it has fallen once (so that
            //it doesnt keep adding to the score)
            hasFallen = true;

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().PinHasFallen();
        }
    }

    //ANGLE CHANGER!!!!!!
    bool CheckHasFallen()
    {
        //Euler is a number between 0 and 360, unity converts it to make it more readable for me
        
        //initial conversion to euler-style number
        Vector3 EulerRotation = transform.rotation.eulerAngles;
        if (Mathf.Abs(EulerRotation.x) > fallAngle || Mathf.Abs(EulerRotation.z) > fallAngle)
        {
            //once we get here, we exit the function
            return true;
        }

        //we are putting this here before we exit the bool function to tell the game that false should be the 
        //defaul active state
        return false;
    }
}

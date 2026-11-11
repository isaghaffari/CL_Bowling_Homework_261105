using UnityEngine;

public class LockLight : MonoBehaviour
{
   public GameObject bowlingBall;
   public int lightYPosition;

   void Update()
   {
      Vector3 bowlingBallPosition = bowlingBall.transform.position;
      bowlingBallPosition.y = lightYPosition;
      transform.position = bowlingBallPosition;
   }
}

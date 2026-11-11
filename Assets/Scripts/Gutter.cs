using UnityEngine;

public class Gutter : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            gameManager.BallInGutter();
    }
}

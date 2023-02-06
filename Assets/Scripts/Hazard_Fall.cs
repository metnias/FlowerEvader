using UnityEngine;

/// <summary>
/// Triggers game over when colliding with player; Also this spins
/// </summary>
public class Hazard_Fall : MonoBehaviour
{
    public float rotateSpeed = 10f;

    void Start()
    {
        transform.Rotate(0f, 0f, Random.value * 360f); // Set random start rotation
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Destroy(gameObject); return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance().GameOver();
            return;
        }
    }
}

using UnityEngine;

/// <summary>
/// Gives score on player trigger, destroys itself and spawn new one
/// </summary>
public class Award_Point : MonoBehaviour
{
    public int score = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.Instance().AddScore(score);
            GameManager.Instance().RequestNewAward();
            return;
        }
    }
}

using UnityEngine;

/// <summary>
/// Adds gently swinging animation
/// </summary>
public class Award_Swing : MonoBehaviour
{
    public float swingSpeed = 0.1f;
    public float swingAmount = 10f;

    private SpriteRenderer spr;
    private int timer;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        timer = 0;
    }

    void Update()
    {
        timer++;
        float rot = Mathf.Sin(timer * swingSpeed) * swingAmount; // uses Sin function for swing motion
        spr.transform.rotation = Quaternion.Euler(0f, 0f, rot);
    }
}

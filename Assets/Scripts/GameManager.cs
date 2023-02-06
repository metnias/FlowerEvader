using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton game manager for managing game which is managed by this game manager which manages game
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Returns singleton instance
    /// </summary>
    /// <returns></returns>
    public static GameManager Instance() => _instance;
    private static GameManager _instance;

    /// <summary>
    /// Game over UI object
    /// </summary>
    public GameObject gameover;
    /// <summary>
    /// Hazard Prefab
    /// </summary>
    public GameObject hazard;
    /// <summary>
    /// Award Prefab
    /// </summary>
    public GameObject award;
    /// <summary>
    /// Player Game object
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Score counter Prefab
    /// </summary>
    public GameObject counter;

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this) Destroy(gameObject);
        }
        GameReset();
    }

    /// <summary>
    /// Score
    /// </summary>
    private int score;
    //private int timer;
    /// <summary>
    /// Spawn timer until the next one spawns
    /// </summary>
    private int spawnTimer;
    /// <summary>
    /// Last hazard spawn position
    /// </summary>
    private float lastHazardX;

    /// <summary>
    /// Resets/Initializes game status
    /// </summary>
    public void GameReset()
    {
        score = 0;
        //timer = 0;
        spawnTimer = 100; // grace time before spawning first hazard
        lastHazardX = 0f; // also grace spot so the first hazard won't spawn above player spawn
        gameover.SetActive(false); // hide game over ui
        Time.timeScale = 1f; // reset timescale
        RequestNewAward(); // spawn first award
    }

    void FixedUpdate()
    {
        if (gameover.activeSelf) return; // if game over, stop game process
        //timer++;
        spawnTimer--; // ticks down spawn timer
        if (spawnTimer < 1) // spawn timer hits 0: spawn new hazard
        {
            spawnTimer = 40 - Mathf.FloorToInt(35 * Mathf.Clamp01(score / 64f));
            // difficulty scale with score: the more score you get, the faster hazard spawns
            var _h = Instantiate(hazard);
            float x;
            do
            {
                x = Random.Range(-8.4f, 8.4f);
            } while (Mathf.Abs(x - lastHazardX) < 2f); // avoid spawning hazards too close to previous one
            lastHazardX = x; // save this hazard pos
            _h.transform.position = new Vector3(x, 5.7f, -1f); // position hazard on random x pos
            // z is in front of everything so hazards never gets obscured
        }
    }

    /// <summary>
    /// Adds score
    /// </summary>
    public void AddScore(int i)
    {
        score += i;
    }

    /// <summary>
    /// Create new award instance
    /// </summary>
    public void RequestNewAward()
    {
        float x, y = Random.Range(-2f, 2f); // y position randomized, so each award require different jump
        do
        {
            x = Random.Range(-8.4f, 8.4f);
        } while (Mathf.Abs(x - player.transform.position.x) < 3f); // don't spawn award too close to player
        var _a = Instantiate(award);
        _a.transform.position = new Vector3(x, y, 1f);
    }

    public void GameOver()
    {
        gameover.SetActive(true);
        // Show scores; because we didn't learn how to text UI
        // but did learn how to Instantiate, so I'm using that
        // for counting scores
        int i = 0;
        while (i < score)
        {
            int x = i % 33, y = i / 33;
            if (score < 32) x += (32 - score) / 2; // rough centring
            var _c = Instantiate(counter);
            _c.name = "count " + (i + 1);
            _c.transform.SetParent(gameover.transform);
            _c.transform.position = new Vector3(-8f + x * 0.5f, 2f + y * 0.5f, 0f);
            i++;
        }

        Time.timeScale = 0f; // time stop
    }

}

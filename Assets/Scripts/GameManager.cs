using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton game manager for managing game which is managed by this game manager which manages game
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance() => _instance;
    private static GameManager _instance;

    public GameObject gameover;
    public GameObject hazard;
    public GameObject award;
    public GameObject player;
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

    private int score;
    //private int timer;
    private int spawnTimer;
    private float lastHazardX;

    public void GameReset()
    {
        score = 0;
        //timer = 0;
        spawnTimer = 100; // grace time before spawning first hazard
        lastHazardX = 0f;
        gameover.SetActive(false);
        Time.timeScale = 1f;
        RequestNewAward();
    }

    void FixedUpdate()
    {
        if (gameover.activeSelf) return; // stop game process
        //timer++;
        spawnTimer--;
        if (spawnTimer < 1)
        {
            spawnTimer = 40 - Mathf.FloorToInt(35 * Mathf.Clamp01(score / 64f));
            // difficulty scale with score: the more score you get, the faster hazard spawns
            var _h = Instantiate(hazard);
            float x;
            do
            {
                x = Random.Range(-8.4f, 8.4f);
            } while (Mathf.Abs(x - lastHazardX) < 2f); // avoid spawning hazards too close
            lastHazardX = x; // save this hazard pos
            _h.transform.position = new Vector3(x, 5.7f, -1f); // position hazard on random x pos
        }
    }

    public void AddScore(int i)
    {
        score += i;
    }

    public void RequestNewAward()
    {
        float x, y = Random.Range(-2f, 2f);
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
            if (score < 32) x += (32 - score) / 2; // centre
            var _c = Instantiate(counter);
            _c.name = "count " + (i + 1);
            _c.transform.SetParent(gameover.transform);
            _c.transform.position = new Vector3(-8f + x * 0.5f, 2f + y * 0.5f, 0f);
            i++;
        }

        Time.timeScale = 0f;
    }

}

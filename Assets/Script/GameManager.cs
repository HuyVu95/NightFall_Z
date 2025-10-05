using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpawnManager spawnManager;
    [Header("Wave Settings")]
    public int currentWave = 1;
    public float preWaveTime = 30f;
    public float postWaveTime = 30f;
    public bool waveActive = false;

    [Header("UI")]
    public TextMeshProUGUI countdownText;

    private float timer;
    private enum State { PreWave, Wave, PostWave }
    private State currentState;

    void Start()
    {
        currentState = State.PreWave;
        timer = preWaveTime;
        UpdateCountdownUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateCountdownUI();

        if (timer <= 0)
        {
            switch (currentState)
            {
                case State.PreWave:
                    StartWave();
                    break;
                case State.Wave:
                    EndWave();
                    break;
                case State.PostWave:
                    PrepareNextWave();
                    break;
            }
        }
    }

    void UpdateCountdownUI()
    {
        switch (currentState)
        {
            case State.PreWave:
                countdownText.text = "San sang: " + Mathf.Ceil(timer) + "s";
                break;
            case State.Wave:
                countdownText.text = "Wave " + currentWave + " dang dien ra!";
                break;
            case State.PostWave:
                countdownText.text = "Chuan bi wave tiep theo: " + Mathf.Ceil(timer) + "s";
                break;
        }
    }

    void StartWave()
    {
        currentState = State.Wave;
        waveActive = true;
        timer = 15f;
        
        Debug.Log("Wave " + currentWave + " bat dau!");
        spawnManager.SpawnWave(currentWave);


    }

    void EndWave()
    {
        currentState = State.PostWave;
        waveActive = false;
        timer = postWaveTime;
        Debug.Log("Wave " + currentWave + " ket thuc!");
    }

    void PrepareNextWave()
    {
        currentWave++;
        currentState = State.PreWave;
        timer = preWaveTime;
        Debug.Log("Chuan bi wave " + currentWave);
    }
    public void CheckWaveClear()
    {
        if (waveActive && Enemy.AliveCount <= 0)
        {
            Debug.Log($"Wave {currentWave} đã dọn sạch!");
            EndWave();
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}

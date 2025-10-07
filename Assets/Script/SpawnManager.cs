using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points Outside Map")]
    public List<Transform> spawnPointsOutsideMap = new List<Transform>();

    [Header("Prefabs")]
    public GameObject EnemyPrefab;
    public GameObject bossPrefab;

    public void SpawnWave(int wave)
    {
        if (spawnPointsOutsideMap == null || spawnPointsOutsideMap.Count == 0)
        {
            Debug.LogError("Không có điểm spawn ngoài bản đồ!");
            return;
        }

        // Tính tổng số zombie cần spawn
        int totalZombieSpawn = 1 + (wave - 1); //số lượng zombie spawn

        // Spawn boss nếu wave đặc biệt
        if (wave == 5 || wave == 10)
        {
            SpawnBoss(wave);
        }
        Debug.Log($"Wave {wave}, tổng zombie cần spawn = {totalZombieSpawn}");
        // Spawn zombie chia đều cho các điểm
        SpawnOutsideMap(totalZombieSpawn, wave);
    }

    public void SpawnOutsideMap(int totalZombieSpawn, int wave)
    {
        int points = spawnPointsOutsideMap.Count;
        int baseCount = totalZombieSpawn / points;
        int remainder = totalZombieSpawn % points;

        int totalSpawned = 0;

        for (int i = 0; i < points; i++)
        {
            int spawnCount = baseCount + (i < remainder ? 1 : 0); // chia đều + dư
            for (int j = 0; j < spawnCount; j++)
            {
                float spread = Mathf.Clamp(wave * 0.5f, 1f, 3f);
                Vector3 offset = new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f);

                Vector3 spawnPos = spawnPointsOutsideMap[i].position + offset;
                Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
                totalSpawned++;
                Debug.Log($"[{System.DateTime.Now:HH:mm:ss}] Spawn 1 zombie tại điểm {i + 1}");
            }
        }

        Debug.Log($"Tổng cộng đã spawn {totalSpawned} zombie từ {points} điểm.");
    }

    void SpawnBoss(int wave)
    {
        int index = Random.Range(0, spawnPointsOutsideMap.Count);
        GameObject boss = Instantiate(bossPrefab, spawnPointsOutsideMap[index].position, Quaternion.identity);
        Debug.Log($"Boss xuất hiện ở wave {wave} tại điểm {index + 1}");
    }
}
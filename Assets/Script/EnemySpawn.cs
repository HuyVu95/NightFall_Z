using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private float MinimumSpawnTime;
    [SerializeField]
    private float MaxximumSpawnTime;
    [SerializeField]
    private float waitTimeSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        SetWaitTimeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        waitTimeSpawn -= Time.deltaTime;
        if(waitTimeSpawn < 0)
        {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            SetWaitTimeSpawn();
        }
    }
    private void SetWaitTimeSpawn()
    {
        waitTimeSpawn = Random.Range(MinimumSpawnTime, MaxximumSpawnTime);
    }
}

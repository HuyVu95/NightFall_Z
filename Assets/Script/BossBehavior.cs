using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public float skillInterval = 5f;
    public GameObject minionPrefab;
    public int minionCount = 3;
    public float spawnRadius = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UseSkillLoop());
    }
    IEnumerator UseSkillLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(skillInterval);
            UseSkill();
        }
    }
    void UseSkill()
    {
        Debug.Log("Boss tung ky nang goi minion");
        for (int i = 0; i < minionCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + (Vector3)offset;

            Instantiate(minionPrefab, spawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

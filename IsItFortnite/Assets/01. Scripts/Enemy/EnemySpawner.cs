using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance = null;

    [SerializeField] List<PoolableMono> enemyList;
    [SerializeField] float distance = 10f;
    [SerializeField] float spawnDelay = 10f;
    private int randVal = 0;
    public Vector3 randPos;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        float balancing = GameManager.Instance.balancing;
        if(spawnDelay >= 3)
            spawnDelay -= (GameManager.Instance.currentTime / (balancing * balancing * balancing));
    }

    private IEnumerator SpawnEnemy()
    {
        while(true)
        {
            //랜덤 에너미 설정
            int randVal = Random.Range(0, enemyList.Count);

            //랜덤 위치 설정
            float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
            randPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

            //에너미 생성
            PoolableMono temp = PoolManager.Instance.Dequeue(enemyList[randVal].name);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}

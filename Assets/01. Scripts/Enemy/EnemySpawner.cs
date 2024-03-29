using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance = null;
    [SerializeField] PoolableMono itemTest;
    [SerializeField] PoolableMono test;
    [SerializeField] List<PoolableMono> phase1;
    [SerializeField] List<PoolableMono> phase2;
    [SerializeField] List<PoolableMono> phase3;
    [SerializeField] float distance = 10f;
    [SerializeField] float spawnDelay = 10f;
    [SerializeField] float limitSpawnDealay = 3f;
    private int randVal;
    public Vector3 randPos;


    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartCoroutine(PhaseUpdate());
    }

    private void TestSpawn(PoolableMono test)
    {
        //랜덤 위치 설정
        float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
        randPos = GameManager.Instance.player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

        //에너미 생성
        PoolableMono temp = PoolManager.Instance.Dequeue(test);
    }

    private void Update()
    {
        //balancing 캐싱
        float balancing = GameManager.Instance.balancing;

        //spawnDelay 시간 비례 감소
        if (spawnDelay >= limitSpawnDealay)
            spawnDelay -= (GameManager.Instance.currentTime / (balancing * balancing * balancing));

        if (Input.GetKeyDown(KeyCode.C))
            TestSpawn(test);
        if (Input.GetKeyDown(KeyCode.D))
        {
            PoolableMono a = PoolManager.Instance.Dequeue(itemTest);
            a.transform.position = GameManager.Instance.player.transform.position;
        }
    }

    private IEnumerator PhaseUpdate()
    {
        SetStart(SpawnPhase(phase1));
        yield return new WaitForSecondsRealtime(200f);
        SetStop();
        SetStart(SpawnPhase(phase2));
        yield return new WaitForSecondsRealtime(200f);
        SetStop();
        SetStart(SpawnPhase(phase3));
        yield return null;
    }

    private IEnumerator coroutine = null;
    private void SetStart(IEnumerator routine)
    {
        coroutine = routine;
        StartCoroutine(coroutine);
    }

    private void SetStop()
    {
        StopCoroutine(coroutine);
    }

    /// <summary>
    /// 랜덤 적 소환
    /// </summary>
    private IEnumerator SpawnPhase(List<PoolableMono> list)
    {
        yield return new WaitForSeconds(limitSpawnDealay);
        while (true)
        {
            //랜덤 에너미 설정
            int randVal = Random.Range(0, list.Count);

            //랜덤 위치 설정
            float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
            randPos = GameManager.Instance.player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

            //에너미 생성
            PoolableMono temp = PoolManager.Instance.Dequeue(list[randVal]);

            yield return new WaitForSecondsRealtime(spawnDelay);
        }
    }

    public void StopMethod()
    {
        StopAllCoroutines();
    }
}

using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance = null;

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
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //StartCoroutine(PhaseUpdate());
        StartCoroutine(SpawnPhase(phase2));
    }

    private void Update()
    {
        //balancing 캐싱
        float balancing = GameManager.Instance.balancing;

        //spawnDelay 시간 비례 감소
        if(spawnDelay >= limitSpawnDealay)
            spawnDelay -= (GameManager.Instance.currentTime / (balancing * balancing * balancing));
    }

    private IEnumerator PhaseUpdate()
    {
        StartCoroutine(SpawnPhase(phase1));
        yield return new WaitForSecondsRealtime(300f);
        StopMethod();
        StartCoroutine(SpawnPhase(phase2));
        yield return new WaitForSecondsRealtime(300f);
        StopMethod();
        StartCoroutine(SpawnPhase(phase3));
    }

    /// <summary>
    /// 랜덤 적 소환
    /// </summary>
    private IEnumerator SpawnPhase(List<PoolableMono> list)
    {
        yield return new WaitForSeconds(limitSpawnDealay);

        while(true)
        {
            //랜덤 에너미 설정
            int randVal = Random.Range(0, list.Count);

            //랜덤 위치 설정
            float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
            randPos = GameManager.Instance.player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

            //에너미 생성
            PoolableMono temp = PoolManager.Instance.Dequeue(list[randVal]);

            yield return new WaitForSecondsRealtime(spawnDelay);
        }
    }

    /// <summary>
    /// SpawnEnemy 스탑 메소드
    /// </summary>
    public void StopMethod()
    {
        StopAllCoroutines();
    }

}

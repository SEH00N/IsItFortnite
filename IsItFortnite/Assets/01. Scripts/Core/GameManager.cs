using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public List<PoolableMono> poolList;
    public TextMeshProUGUI scoreTxt;
    public GameObject pooler;
    public Transform minPos;
    public Transform maxPos;
    public Transform player;
    public float currentTime;
    public float score = 0;
    public int balancing = 100;

    private void Awake()
    {
        //Instance 할당
        if (GameManager.Instance == null)
            Instance = this;

        if (PoolManager.Instance == null)
            PoolManager.Instance = new PoolManager(pooler.transform);

        if (PlayerState.Instance == null)
            PlayerState.Instance = new PlayerState();

        //풀러 생성
        foreach (PoolableMono temp in poolList)
            PoolManager.Instance.CreatePool(temp, 5);
    }

    private void Start()
    {
        StartCoroutine(IncreaseScore());
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        //시간비례 스코어 증가
        //score += currentTime / (balancing * balancing);

        //스코어 타이핑
        scoreTxt.text = $"{Mathf.FloorToInt(score)}";
    }

    //스코어 증가
    public void SetScore(float scr)
    {
        score += scr * (currentTime / balancing);
    }

    private IEnumerator IncreaseScore()
    {
        while(true)
        {
            score += currentTime / balancing;
            yield return new WaitForSeconds(5);
        }
    }
}

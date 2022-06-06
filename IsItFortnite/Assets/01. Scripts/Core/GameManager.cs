using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public Transform player = null;
    public List<PoolableMono> poolList;
    public TextMeshProUGUI scoreTxt;
    public GameObject pooler;
    public Transform minPos;
    public Transform maxPos;
    public float currentTime;
    public float score = 0;
    public int balancing = 100;

    private void OnEnable()
    {
        //Instance 할당
        Instance = this;

        PoolManager.Instance = new PoolManager(pooler.transform);

        GameObject timeController = new GameObject("TimeController");
        timeController.transform.SetParent(transform);
        TimeController.Instance = timeController.AddComponent<TimeController>();

        //풀러 생성
        foreach (PoolableMono temp in poolList)
            PoolManager.Instance.CreatePool(temp, 5);

            player = GameObject.FindWithTag("Player").transform;
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

    /// <summary>
    /// 적 피격시 스코어 증가
    /// </summary>
    public void SetScore(float scr)
    {
        score += scr * (currentTime / balancing);
    }

    /// <summary>
    /// 점수 저장
    /// </summary>
    public void SaveScore()
    {
        PlayerPrefs.SetInt("CurrentTime", Mathf.FloorToInt(currentTime));
        PlayerPrefs.SetInt("CurrentScore", Mathf.FloorToInt(score));
        if (score > PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", Mathf.FloorToInt(score));
        if (currentTime > PlayerPrefs.GetInt("BestTime", 0))
            PlayerPrefs.SetInt("BestTime", Mathf.FloorToInt(currentTime));
    }

    /// <summary>
    /// 시간 비례 스코어 증가
    /// </summary>
    private IEnumerator IncreaseScore()
    {
        while (true)
        {
            score += currentTime / balancing;
            yield return new WaitForSeconds(5);
        }
    }
}

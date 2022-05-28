using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] Transform pooler;
    public List<PoolableMono> poolList;
    public Transform minPos;
    public Transform maxPos;
    public Transform player;
    public int score = 0;

    private void Awake()
    {
        //Instance 할당
        if (GameManager.Instance == null)
            Instance = this;

        if (PoolManager.Instance == null)
            PoolManager.Instance = new PoolManager(pooler);

        if (PlayerState.Instance == null)
            PlayerState.Instance = new PlayerState();

        //풀러 생성
        foreach (PoolableMono temp in poolList)
            PoolManager.Instance.CreatePool(temp, 5);
    }
}

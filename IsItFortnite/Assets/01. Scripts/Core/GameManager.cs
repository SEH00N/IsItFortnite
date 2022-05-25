using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] Transform pooler;
    [SerializeField] List<PoolableMono> poolList;
    public Transform maxPos;
    public Transform minPos;

    private void Awake()
    {
        //Instance 할당
        if(GameManager.Instance == null)
            Instance = this;

        if(PoolManager.Instance == null)
            PoolManager.Instance = new PoolManager(pooler);

        if(PlayerState.Instance == null)
            PlayerState.Instance = new PlayerState();

        //풀러 생성
        foreach (PoolableMono temp in poolList)
            PoolManager.Instance.CreatePool(temp, 5);
    }
}

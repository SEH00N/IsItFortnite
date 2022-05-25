using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance = null;
    private Dictionary<string, Pool<PoolableMono>> pools = new Dictionary<string, Pool<PoolableMono>>();
    private Transform parentTrm;

    /// <summary>
    /// 풀매니저 생성자
    /// </summary>
    public PoolManager(Transform _parentTrm)
    {
        parentTrm = _parentTrm;
    }

    /// <summary>
    /// 풀매니저 딕셔너리 생성
    /// </summary>
    public void CreatePool(PoolableMono prefab, int cnt)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, parentTrm, cnt);
        pools.Add(prefab.gameObject.name, pool);
    }

    /// <summary>
    /// 풀매니저 딕셔너리에서 해당 이름의 오브젝트 Dequeue
    /// </summary>
    public PoolableMono Dequeue(string prefabName)
    {
        PoolableMono temp = pools[prefabName].Dequeue();
        temp.Reset();
        return temp;
    }

    /// <summary>
    /// 풀매니저 딕셔너리에 해당 이름으로 Enqueue
    /// </summary>
    public void Enqueue(PoolableMono temp)
    {
        pools[temp.name].Enqueue(temp);
    }
}

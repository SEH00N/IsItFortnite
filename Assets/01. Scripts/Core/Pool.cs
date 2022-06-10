using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Queue<T> pooler = new Queue<T>();

    private T prefab;
    private Transform parent;

    /// <summary>
    /// 풀 생성자
    /// </summary>
    public Pool(T _prefab, Transform _parent, int cnt)
    {
        prefab = _prefab;
        parent = _parent;
    }

    /// <summary>
    /// 풀러의 템플릿 풀링
    /// </summary>
    /// <returns></returns>
    public T Dequeue()
    {
        T temp = null;
        if(pooler.Count > 0)
        {
            temp = pooler.Dequeue();
            temp.gameObject.SetActive(true);
        }
        
        else
        {
            temp = GameObject.Instantiate(prefab, parent);
            temp.gameObject.name = temp.name.Replace("(Clone)", "");
        }

        return temp;
    }

    /// <summary>
    /// 풀러에 템플릿 인큐
    /// </summary>
    public void Enqueue(T temp)
    {
        temp.gameObject.SetActive(false);
        pooler.Enqueue(temp);
    }
}

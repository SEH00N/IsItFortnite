using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    [SerializeField] List<UnityEvent> skillList;
    [SerializeField] Transform lookAt;
    private Camera cam = null;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        SkillOn();
    }

    /// <summary>
    /// 키가 입력됐을 때 스킬 리스트의 스킬들 모두 실행
    /// </summary>
    private void SkillOn()
    {
        if(Input.anyKeyDown)
        {
            foreach(UnityEvent item in skillList)
                item?.Invoke();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    [SerializeField] List<UnityEvent> skillList;
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
            rotation();
            foreach(UnityEvent item in skillList)
                item?.Invoke();
        }
    }

    /// <summary>
    /// 각도전환
    /// </summary>
    private void rotation()
    {
        Vector3 rotate = transform.eulerAngles;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 rotateDir = (transform.position - mousePos).normalized;
        float angle = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90f;
        transform.parent.rotation = Quaternion.Euler(rotate);
    }
}

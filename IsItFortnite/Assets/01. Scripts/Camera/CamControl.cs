using System.Collections;
using UnityEngine;
using Cinemachine;

public class CamControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] CinemachineVirtualCamera vCam1;
    [SerializeField] float noramlSize = 10f;
    [SerializeField] float zoomSize = 30f;
    private bool zoomed = false;

    private void Start()
    {
        StartCoroutine(ChangeCam());

        //orthographic 사이즈 초기화
        vCam.m_Lens.OrthographicSize = noramlSize;
    }

    private void Update()
    {
        Zoom();
    }

    /// <summary>
    /// 줌 or 리줌
    /// </summary>
    private void Zoom()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !zoomed)
        {
            vCam.m_Lens.OrthographicSize = zoomSize;
            zoomed = true;
            Time.timeScale = 0.1f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && zoomed)
        {
            Time.timeScale = 1;
            vCam.m_Lens.OrthographicSize = noramlSize;
            zoomed = false;
        }
    }

    /// <summary>
    /// 카메라 전환 연출
    /// </summary>
    private IEnumerator ChangeCam()
    {
        yield return null;
        vCam1.Priority = 0;
    }
}

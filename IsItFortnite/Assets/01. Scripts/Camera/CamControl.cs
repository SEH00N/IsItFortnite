using System.Collections;
using UnityEngine;
using Cinemachine;

public class CamControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] CinemachineVirtualCamera vCam1;
    [SerializeField] GameObject pausePanel;
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
            zoomed = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && zoomed)
        {
            zoomed = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
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

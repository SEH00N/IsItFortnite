using System.Collections;
using UnityEngine;
using Cinemachine;

public class CamControl : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    [SerializeField] CinemachineVirtualCamera vCam1;
    [SerializeField] GameObject pausePanel;
    [SerializeField] float noramlSize = 10f;
    private bool zoomed = false;
    private CinemachineBasicMultiChannelPerlin perlin = null;

    private void Awake()
    {
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        StartCoroutine(ChangeCam());
        perlin.m_AmplitudeGain = 0f;
        //orthographic 사이즈 초기화
        vCam.m_Lens.OrthographicSize = noramlSize;
        vCam.m_Follow = GameManager.Instance.player.transform;
    }

    public void Shake(float val, float endtime)
    {
        StopAllCoroutines();
        StartCoroutine(CamShake(val, endtime));
    }

    private IEnumerator CamShake(float val, float endtime)
    {
        perlin.m_AmplitudeGain = val;

        float currentTime = 0f;
        while(currentTime < endtime)
        {
            yield return null;
            perlin.m_AmplitudeGain = Mathf.Lerp(val, 0, currentTime / endtime);
            currentTime += Time.deltaTime;
        }
        perlin.m_AmplitudeGain = 0f;
    }

    public void SetFollow(Transform trm)
    {
        vCam.m_Follow = trm;
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

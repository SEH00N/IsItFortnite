using UnityEngine;
using Cinemachine;

public class CamControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] float noramlSize = 10f;
    [SerializeField] float zoomSize = 30f;
    private bool zoomed = false;

    private void Update()
    {
        Zoom();
    }

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
}

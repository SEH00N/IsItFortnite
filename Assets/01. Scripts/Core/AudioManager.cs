using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private Slider volumeCtroller = null;
    private float volume = 0;

    private void Awake()
    {
        volumeCtroller = GameObject.Find("VolumeController").GetComponent<Slider>();
        volumeCtroller.value = PlayerPrefs.GetFloat("volume", 1);
    }

    private void OnDisable()
    {
        volume = volumeCtroller.value;
        PlayerPrefs.SetFloat("volume", volume);
    }
}

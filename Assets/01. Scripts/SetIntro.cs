using UnityEngine;

public class SetIntro : MonoBehaviour
{
    private int isWatched = 0;
    [SerializeField] RectTransform rt;

    private void Awake()
    {
        isWatched = PlayerPrefs.GetInt("IsWatched", 0);
        if(isWatched == 0)
            rt.localPosition = new Vector3(1920, 0, 0);
        else
            rt.localPosition = Vector3.zero;
        
        isWatched++;
        PlayerPrefs.SetInt("IsWatched", isWatched);
    }
}

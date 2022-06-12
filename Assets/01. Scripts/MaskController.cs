using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    [SerializeField] string skillName;
    private Image mask = null;
    private SkillBase sb = null;

    private void Awake()
    {
        sb = GameObject.Find($"Skills/{skillName}").GetComponent<SkillBase>();
        mask = GetComponent<Image>();
    }

    private void Update()
    {
        mask.fillAmount = (sb.coolTime - sb.coolDown) / sb.coolTime;
    }
}

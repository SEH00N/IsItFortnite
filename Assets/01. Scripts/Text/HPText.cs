using TMPro;
using UnityEngine;

public class HPText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    private PlayerDamaged pd = null;

    private void Start()
    {
        pd = GameManager.Instance.player.GetComponent<PlayerDamaged>();
    }

    private void Update()
    {
        TextHP();
    }

    private void TextHP()
    {
        hpText.text = $"{pd.currentHP}/{pd.maxHP}";
    }
}

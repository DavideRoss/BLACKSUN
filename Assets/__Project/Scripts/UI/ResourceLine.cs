using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

public class ResourceLine : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Sprite;
    public TMP_Text Text;

    ResourceDefinition _def;
    int _count = 0;

    public void SetDefinition(ResourceDefinition def)
    {
        _def = def;
        Sprite.sprite = def.Sprite;
        UpdateText();
    }

    public void UpdateText()
    {
        // Text.text = $"{_def.Name}: {_count}";
        Text.text = _count.ToString();
    }

    public void UpdateText(int count)
    {
        _count = count;
        UpdateText();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Instance.ShowResource(_def);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Hide();
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceLine : MonoBehaviour
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
}
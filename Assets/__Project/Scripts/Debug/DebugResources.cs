using UnityEngine;
using TMPro;

public class DebugResources : MonoBehaviour
{
    TMP_Text Text;

    private void Start()
    {
        Text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        Text.text = "";
        foreach (var res in ResourceManager.Instance.Count) Text.text += $"{res.Resource.ToString()}: {res.Count}\n";
    }
}
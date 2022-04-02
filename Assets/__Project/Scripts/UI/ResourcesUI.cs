using System.Collections.Generic;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    static ResourcesUI _instance;
    public static ResourcesUI Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ResourcesUI>();
            return _instance;
        }
    }

    public ResourceLine LinePrefab;

    Dictionary<Resource, ResourceLine> _lines = new Dictionary<Resource, ResourceLine>();

    public void RefreshUI()
    {
        foreach (var res in ResourceManager.Instance.Count)
        {
            if (_lines.ContainsKey(res.Resource)) _lines[res.Resource].UpdateText(res.Count);
            else
            {
                ResourceDefinition def = ResourceManager.Instance.Definitions.GetDefinition(res.Resource);
                ResourceLine rl = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity, transform);
                rl.SetDefinition(def);
                rl.UpdateText(res.Count);

                _lines.Add(res.Resource, rl);
            }
        }
    }
}
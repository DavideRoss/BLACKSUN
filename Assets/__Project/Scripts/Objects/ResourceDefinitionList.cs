using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceDefinitionList", menuName = "BLACKSUN/Resource Definition List")]
public class ResourceDefinitionList : ScriptableObject
{
    public List<ResourceDefinition> Definitions;

    public ResourceDefinition GetDefinition(Resource res) => Definitions.Find(e => e.Resource == res);
}

[Serializable]
public struct ResourceDefinition
{
    public Resource Resource;
    public Sprite Sprite;
    public string Name;
}

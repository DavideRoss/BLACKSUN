using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResourceCount
{
    public Resource Resource;
    public int Count;

    public ResourceCount(Resource res, int count)
    {
        Resource = res;
        Count = count;
    }

    public int Add(int c)
    {
        Count += c;
        return Count;
    }
}

public class ResourceManager : MonoBehaviour
{
    #region Singleton

    static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ResourceManager>();
            return _instance;
        }
    }

    #endregion

    public List<ResourceCount> Count;

    public bool CheckRequirements(List<ResourceCount> req)
    {
        if (req.Count == 0) return true;
        
        return req.All(e => {
            ResourceCount rc = Count.Find(k => k.Resource == e.Resource);
            return rc != null && rc.Count >= e.Count;
        });
    }

    public bool CheckAvailability(Resource type, int count = 1)
    {
        ResourceCount rc = Count.Find(e => e.Resource == type);
        return rc != null && rc.Count >= count;
    }

    public int GetQuantity(Resource type)
    {
        ResourceCount rc = Count.Find(e => e.Resource == type);
        if (rc == null) return 0;
        return rc.Count;
    }

    public int GetVillagers() => Count.Find(e => e.Resource == Resource.Villager).Count;

    public void Add(Resource res, int count)
    {
        int i = Count.FindIndex(e => e.Resource == res);
        if (i > -1) Count[i].Count += count;
        else Count.Add(new ResourceCount(res, count));
    }

    public void Add(List<ResourceCount> result)
    {
        foreach (var rc in result) Add(rc.Resource, rc.Count);
    }
    
    public void Remove(List<ResourceCount> req)
    {
        foreach (var rc in req) Add(rc.Resource, -rc.Count);
    }
}

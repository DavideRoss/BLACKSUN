using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job", menuName = "BLACKSUN/Job")]
public class Job : ScriptableObject
{
    public String Name;

    public List<ResourceCount> Requirements;
    public List<ResourceCount> Result;

    public float MaxPersons = -1;
    public float TicksToComplete;
}

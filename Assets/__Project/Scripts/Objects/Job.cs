using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job", menuName = "BLACKSUN/Job")]
public class Job : ScriptableObject
{
    public String Name;
    public int Level = 0;

    public List<ResourceCount> Requirements;
    public List<ResourceCount> Result;
    public string SpecialOutput;

    public float MaxPersons = -1;
    public float TicksToComplete;

    [HideInInspector] public bool Unlocked = false;
}

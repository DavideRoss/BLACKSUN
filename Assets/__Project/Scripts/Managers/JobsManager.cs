using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobsManager : MonoBehaviour
{
    static JobsManager _instance;
    public static JobsManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<JobsManager>();
            return _instance;
        }
    }

    [Header("Globals")]
    public int MaxLevel = 4;

    [Header("References")]
    public List<Job> Jobs;
    public JobView JobViewPrefab;
    public ResearchView ResearchViewPrefab;

    [Header("Starting Settings")]
    public List<Job> StartingJobs;

    bool _researchUnlocked = false;

    private void Start()
    {
        foreach (Job j in Jobs) j.Unlocked = false;
        foreach (Job j in StartingJobs) SpawnJob(j);
    }

    public void SpawnJob(Job j)
    {
        JobView jv = Instantiate(JobViewPrefab, Vector3.zero, Quaternion.identity, transform);
        jv.Job = j;
        jv.Initialize();
        
        MarkJobAsUnlocked(j);
    }

    public void UnlockResearch()
    {
        if (_researchUnlocked) return;
        _researchUnlocked = true;
        
        Instantiate(ResearchViewPrefab, Vector3.zero, Quaternion.identity, transform);
    }

    public void UnlockNewJob()
    {
        List<Job> avail = Jobs.Where(e => !e.Unlocked).OrderBy(e => e.Level).ToList();
        if (avail.Count > 0) SpawnJob(avail[0]);
    }

    private void MarkJobAsUnlocked(Job j)
    {
        int i = Jobs.FindIndex(e => e == j);
        Jobs[i].Unlocked = true;
    }

    public int GetHighestLevel()
    {
        return Jobs.Where(e => e.Unlocked).Max(e => e.Level);
    }
}
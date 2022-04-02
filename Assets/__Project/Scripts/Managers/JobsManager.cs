using System.Collections.Generic;
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

    [Header("Grid Settings")]
    public int CellsPerRow = 5;
    public int Rows = 3;
    public float HorizontalOffset = 2f;
    public float VerticalOffset = 2f;

    [Header("References")]
    public List<Job> Jobs;
    public JobView JobViewPrefab;

    [Header("Starting Settings")]
    public List<Job> StartingJobs;

    Vector2Int _nextCell = new Vector2Int(0, 0);

    private void Start()
    {
        foreach (Job j in StartingJobs) SpawnJob(j);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < CellsPerRow; x++)
            {
                Gizmos.DrawWireSphere(transform.position + new Vector3(x * HorizontalOffset, y * -VerticalOffset, 0f), .25f);
            }
        }
    }

    public void SpawnJob(Job j)
    {
        JobView jv = Instantiate(JobViewPrefab, new Vector3(_nextCell.x * HorizontalOffset, _nextCell.y * -VerticalOffset, 0f) + transform.position, Quaternion.identity, transform);
        jv.Job = j;
        jv.Initialize();

        _nextCell.x++;
        if (_nextCell.x >= CellsPerRow)
        {
            _nextCell.x = 0;
            _nextCell.y++;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class AltarManager : MonoBehaviour
{
    static AltarManager _instance;
    public static AltarManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<AltarManager>();
            return _instance;
        }
    }

    [Header("Generation")]
    public List<Sprite> Sprites = new List<Sprite>();
    public List<Color> Colors = new List<Color>();

    [Header("References")]
    public Transform SpawnPoint;
    public GodHead GodHeadPrefab;

    [Header("Starting Settings")]
    public List<ResourceCount> StartingDemands = new List<ResourceCount>();
    public float StartDelay = 3f;
    public float StartOffset = 1f;

    private void Start()
    {
        this.Invoke(() => {
            for (int i = 0; i < StartingDemands.Count; i++) 
            {
                int j = i;
                this.Invoke(() => {
                    SpawnHead(StartingDemands[j]);
                }, i * StartOffset);
            }
        }, StartDelay);
    }

    public void SpawnHead()
    {
        if (!GameManager.Instance.Playing) return;

        // TODO: change parameters to spawn
        Instantiate(GodHeadPrefab, SpawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnHead(ResourceCount demand)
    {
        if (!GameManager.Instance.Playing) return;

        GodHead gh = Instantiate(GodHeadPrefab, SpawnPoint.position, Quaternion.identity, transform);
        gh.Demands = demand;
        gh.Initialize(Sprites.PickRandom(), Colors.PickRandom());
    }

    public void AskForNewHead(float interval = 3f)
    {
        // TODO: change fixed spawn time
        this.Invoke(SpawnHead, interval);
    }
}
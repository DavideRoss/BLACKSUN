using System.Collections.Generic;
using System.Linq;
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

        int count = Mathf.RoundToInt(Random.Range(1, 3) * GameManager.Instance.Difficulty * (Random.value < .5f ? 3 : 5));

        var defs = ResourceManager.Instance.Definitions.Definitions;
        int maxLevel = JobsManager.Instance.GetHighestLevel();
        int resLevel = maxLevel;
        if (maxLevel < JobsManager.Instance.MaxLevel) resLevel = Random.value < .25f ? maxLevel + 1 : maxLevel;

        Resource res = defs.Where(e => e.Level == resLevel).PickRandom().Resource;

        SpawnHead(new ResourceCount(res, count));
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
        this.Invoke(SpawnHead, interval);
    }
}
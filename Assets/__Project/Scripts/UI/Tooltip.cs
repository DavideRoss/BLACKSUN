using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Tooltip : MonoBehaviour
{
    static Tooltip _instance;
    public static Tooltip Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Tooltip>();
            return _instance;
        }
    }

    public bool UpdatePosition = false;
    public GameObject Canvas;

    [Header("Job")]
    public GameObject JobMain;
    public TMP_Text JobTitle;
    public List<Image> InputSlots;
    public List<Image> OutputSlots;

    [Header("God")]
    public GameObject GodMain;
    public TMP_Text GodName;
    public TMP_Text GodDomain;
    public TMP_Text RequestCount;
    public Image RequestImage;

    [Header("Resource")]
    public GameObject ResourceMain;
    public TMP_Text ResourceName;

    Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (!UpdatePosition) return;

        Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    public void Show()
    {
        Canvas.SetActive(true);
        UpdatePosition = true;
    }

    public void Hide()
    {
        Canvas.SetActive(false);
        UpdatePosition = false;
    }

    public void ShowJob(Job j)
    {
        Show();

        JobMain.SetActive(true);
        GodMain.SetActive(false);
        ResourceMain.SetActive(false);

        JobTitle.text = j.Name;

        for (int i = 0; i < InputSlots.Count; i++)
        {
            if (i >= j.Requirements.Count) InputSlots[i].gameObject.SetActive(false);
            else
            {
                InputSlots[i].gameObject.SetActive(true);
                InputSlots[i].sprite = ResourceManager.Instance.Definitions.GetDefinition(j.Requirements[i].Resource).Sprite;
            }
        }

        for (int i = 0; i < OutputSlots.Count; i++)
        {
            if (i >= j.Result.Count) OutputSlots[i].gameObject.SetActive(false);
            else
            {
                OutputSlots[i].gameObject.SetActive(true);
                OutputSlots[i].sprite = ResourceManager.Instance.Definitions.GetDefinition(j.Result[i].Resource).Sprite;
            }
        }
    }

    public void ShowResource(ResourceDefinition rd)
    {
        Show();

        JobMain.SetActive(false);
        GodMain.SetActive(false);
        ResourceMain.SetActive(true);
        
        ResourceName.text = rd.Name;
    }

    public void ShowGod(GodHead head)
    {
        Show();

        JobMain.SetActive(false);
        GodMain.SetActive(true);
        ResourceMain.SetActive(false);

        GodName.text = head.Name;
        GodDomain.text = head.Domain;

        RequestCount.text = head.Demands.Count.ToString();
        RequestImage.sprite = ResourceManager.Instance.Definitions.GetDefinition(head.Demands.Resource).Sprite;
    }
}
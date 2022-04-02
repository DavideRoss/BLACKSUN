using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class JobView : MonoBehaviour, IPointerClickHandler, IOnTickHandler
{
    public Job Job;
    public int AssignedPersons = 0;

    [Header("UI")]
    public TMP_Text Text_JobTitle;
    public TMP_Text Text_Assigned;
    public RectTransform Panel_ProgressBar;

    bool _started = false;
    float _jobAdvancement = 0f;

    private void Start()
    {
        GameManager.Instance.Register(this);

        Text_JobTitle.text = Job.Name;
        RefreshUI();
    }

    public void OnTick()
    {
        if (!_started && AssignedPersons > 0 && ResourceManager.Instance.CheckRequirements(Job.Requirements))
        {
            ResourceManager.Instance.Remove(Job.Requirements);
            _started = true;
        }

        if (!_started) return;

        _jobAdvancement += AssignedPersons;
        if (_jobAdvancement > Job.TicksToComplete)
        {
            ResourceManager.Instance.Add(Job.Result);
            _jobAdvancement = 0f;
            _started = false;
        }

        UpdateProgressBar();
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (e.button == PointerEventData.InputButton.Left && ResourceManager.Instance.GetVillagers() > 0 && (Job.MaxPersons < 0 || AssignedPersons < Job.MaxPersons))
        {
            AssignedPersons++;
            ResourceManager.Instance.Add(Resource.Villager, -1);
        }

        if (e.button == PointerEventData.InputButton.Right && AssignedPersons > 0)
        {
            AssignedPersons--;
            ResourceManager.Instance.Add(Resource.Villager, 1);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        Text_Assigned.text = AssignedPersons.ToString();
        if (Job.MaxPersons > -1) Text_Assigned.text += $" / {Job.MaxPersons}";
    }
    public void UpdateProgressBar()
    {
        Panel_ProgressBar.localScale = new Vector3(_jobAdvancement / Job.TicksToComplete, 1f, 1f);
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using TMPro;
using DG.Tweening;

public class JobView : MonoBehaviour, IPointerClickHandler, IOnTickHandler
{
    public Job Job;

    [Header("UI")]
    public SpriteRenderer SpriteBorder;
    public TMP_Text Text_JobTitle;
    public TMP_Text Text_Assigned;
    public RectTransform Panel_ProgressBar;

    int _assignedPersons = 0;
    float _jobAdvancement = 0f;
    bool _started = false;
    bool _errorState = false;

    [HideInInspector]
    public UnityEvent OnJobDone;

    private void Start()
    {
        GameManager.Instance.Register(this);
    }

    public void Initialize()
    {
        Text_JobTitle.text = Job.Name;
        RefreshUI();
    }

    // TODO: add minimum requirement for a job
    public void OnTick()
    {
        if (!_started && _assignedPersons > 0)
        {
            if (ResourceManager.Instance.CheckRequirements(Job.Requirements))
            {
                ResourceManager.Instance.Remove(Job.Requirements);
                _started = true;

                if (_errorState) ToggleErrorState();
            }
            else if (!_errorState) ToggleErrorState();
        }

        if (!_started && _errorState && _assignedPersons == 0) ToggleErrorState();

        if (!_started) return;

        _jobAdvancement += _assignedPersons;
        if (_jobAdvancement > Job.TicksToComplete)
        {
            ResourceManager.Instance.Add(Job.Result);
            if (OnJobDone != null) OnJobDone.Invoke();

            _jobAdvancement = 0f;
            _started = false;
        }

        UpdateProgressBar();
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (e.button == PointerEventData.InputButton.Left && ResourceManager.Instance.GetVillagers() > 0 && (Job.MaxPersons < 0 || _assignedPersons < Job.MaxPersons))
        {
            _assignedPersons++;
            ResourceManager.Instance.Add(Resource.Villager, -1);
        }

        if (e.button == PointerEventData.InputButton.Right && _assignedPersons > 0)
        {
            _assignedPersons--;
            ResourceManager.Instance.Add(Resource.Villager, 1);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        Text_Assigned.text = _assignedPersons.ToString();
        if (Job.MaxPersons > -1) Text_Assigned.text += $"/{Job.MaxPersons}";
    }
    public void UpdateProgressBar()
    {
        Panel_ProgressBar.localScale = new Vector3(_jobAdvancement / Job.TicksToComplete, 1f, 1f);
    }

    private void ToggleErrorState()
    {
        _errorState = !_errorState;
        SpriteBorder.DOColor(_errorState ? Color.red : Color.white, .125f);
    }
}
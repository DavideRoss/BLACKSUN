using UnityEngine;

public class ResearchView : MonoBehaviour
{
    JobView _view;

    private void Start()
    {
        _view = GetComponent<JobView>();
        _view.Initialize();

        _view.OnJobDone.AddListener(HandleResearchDone);
    }

    private void HandleResearchDone()
    {
        // TODO: handle unlocking new jobs
        Debug.Log("Handle research");
    }
}

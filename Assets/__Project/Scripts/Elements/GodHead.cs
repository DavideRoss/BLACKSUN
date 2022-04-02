using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GodHead : MonoBehaviour, IOnTickHandler, IPointerClickHandler
{
    public ResourceType Type;
    public ResourceCount Demands;
    public float TotalTicks;

    public float BonusTicks;
    public float MalusTicks;

    [Header("UI")]
    public TMP_Text Text_Demands;
    public RectTransform Panel_ProgressBar;

    int _currentCount;
    float _pastTicks;

    private void Start()
    {
        GameManager.Instance.Register(this);
    }

    public void OnTick()
    {
        _pastTicks++;

        if (_pastTicks >= TotalTicks) DestroyHead(-MalusTicks);
        
        Text_Demands.text = $"{Demands.Resource.ToString()}\n{_currentCount} / {Demands.Count}";

        float progress = Mathf.Min(1f, _pastTicks / TotalTicks);
        Panel_ProgressBar.sizeDelta = new Vector2(progress, Panel_ProgressBar.sizeDelta.y);
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        int available = ResourceManager.Instance.GetQuantity(Demands.Resource);
        if (available == 0) return;

        int qtyToAsk = Mathf.Min(Demands.Count - _currentCount, available);
        ResourceManager.Instance.Add(Demands.Resource, -qtyToAsk);
        _currentCount += qtyToAsk;

        if (_currentCount == Demands.Count) DestroyHead(BonusTicks);
    }

    private void DestroyHead(float ticksToAdd)
    {
        // TODO: animate
        // TODO: spawn a new head
        
        GameManager.Instance.AddTicksToDoom(ticksToAdd);
        GameManager.Instance.Unregister(this);
        Destroy(this.gameObject);
    }
}
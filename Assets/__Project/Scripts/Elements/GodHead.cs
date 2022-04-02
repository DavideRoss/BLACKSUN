using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GodHead : MonoBehaviour, IOnTickHandler, IPointerClickHandler
{
    public ResourceCount Demands;
    public float TotalTicks;

    public float BonusTicks;
    public float MalusTicks;

    // TODO: add camera shake when the head hit the altar (or another head) for the first time

    [Header("UI")]
    public TMP_Text Text_Demands;
    public RectTransform Panel_ProgressBar;

    int _currentCount;
    float _pastTicks;

    Rigidbody2D _rb;
    bool _shaken = false;

    private void Start()
    {
        GameManager.Instance.Register(this);
    }

    public void Initialize()
    {
        RefreshUI();
    }

    public void OnTick()
    {
        _pastTicks++;

        if (_pastTicks >= TotalTicks) DestroyHead(-MalusTicks);
        
        RefreshUI();
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
        GameManager.Instance.AddTicksToDoom(ticksToAdd);
        AltarManager.Instance.AskForNewHead();
        
        GameManager.Instance.Unregister(this);
        Destroy(this.gameObject);
    }
    
    private void RefreshUI()
    {
        Text_Demands.text = $"{Demands.Resource.ToString()}\n{_currentCount} / {Demands.Count}";

        float progress = Mathf.Min(1f, _pastTicks / TotalTicks);
        Panel_ProgressBar.sizeDelta = new Vector2(progress, Panel_ProgressBar.sizeDelta.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_shaken)
        {
            _shaken = true;
            ShakeManager.Instance.Shake();
        }
    }
}

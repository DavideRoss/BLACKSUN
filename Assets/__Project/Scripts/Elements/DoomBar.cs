using UnityEngine;
using TMPro;

public class DoomBar : MonoBehaviour, IOnTickHandler
{
    public RectTransform ProgressBar;
    public TMP_Text Text_Remaining;

    float _startWidth;

    private void Start()
    {
        GameManager.Instance.Register(this);
        _startWidth = ProgressBar.sizeDelta.x;
    }

    public void OnTick()
    {
        ProgressBar.sizeDelta = new Vector2(_startWidth * GameManager.Instance.GetDoomPercentage(), ProgressBar.sizeDelta.y);
        Text_Remaining.text = GameManager.Instance.GetRemainingDays().ToString() + " remaining days";
    }
}
using UnityEngine;
using TMPro;

public class DoomBar : MonoBehaviour
{
    static DoomBar _instance;
    public static DoomBar Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<DoomBar>();
            return _instance;
        }
    }

    public RectTransform ProgressBar;
    public TMP_Text Text_Remaining;

    float _startWidth;

    private void Start()
    {
        _startWidth = ProgressBar.sizeDelta.x;
    }

    public void UpdateUI()
    {
        ProgressBar.sizeDelta = new Vector2(_startWidth * GameManager.Instance.GetDoomPercentage(), ProgressBar.sizeDelta.y);
        Text_Remaining.text = Mathf.Max(0, GameManager.Instance.GetRemainingDays()).ToString() + " remaining days";
    }
}
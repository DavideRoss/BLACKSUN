using UnityEngine;

using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public RectTransform CreditsPanel;
    public Transform Logo;

    bool _credits = false;
    float _creditsAnim = .5f;

    public void ShowCredits()
    {
        if (_credits) return;
        _credits = true;

        CreditsPanel.DOMoveX(0, _creditsAnim).SetEase(Ease.InOutQuad);
        Logo.DOMoveX(-9f, _creditsAnim).SetEase(Ease.InOutQuad);
    }

    public void HideCredits()
    {
        if (!_credits) return;
        _credits = false;

        CreditsPanel.DOMoveX(9f, _creditsAnim).SetEase(Ease.InOutQuad);
        Logo.DOMoveX(0, _creditsAnim).SetEase(Ease.InOutQuad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
}

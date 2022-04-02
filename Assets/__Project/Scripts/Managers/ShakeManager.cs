using UnityEngine;
using DG.Tweening;

public class ShakeManager : MonoBehaviour
{
    static ShakeManager _instance;
    public static ShakeManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ShakeManager>();
            return _instance;
        }
    }

    Camera _cam;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    
    }

    public void Shake()
    {
        _cam.DOShakePosition(.5f, .1f, 20);
    }
}

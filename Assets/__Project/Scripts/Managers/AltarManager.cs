using UnityEngine;

public class AltarManager : MonoBehaviour
{
    static AltarManager _instance;
    public static AltarManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<AltarManager>();
            return _instance;
        }
    }

    
}
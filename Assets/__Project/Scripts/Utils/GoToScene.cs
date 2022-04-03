using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string Scene;

    public void Go()
    {
        SceneManager.LoadScene(Scene);
    }
}
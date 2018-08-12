using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameSceneLoad : MonoBehaviour
{
    public void SceneReplace()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SceneEnd()
    {
        Application.Quit();
    }
}

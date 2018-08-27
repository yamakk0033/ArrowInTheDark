using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
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
}

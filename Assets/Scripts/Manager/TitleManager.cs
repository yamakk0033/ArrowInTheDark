using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Manager
{
    [DisallowMultipleComponent]
    public class TitleManager : MonoBehaviour
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

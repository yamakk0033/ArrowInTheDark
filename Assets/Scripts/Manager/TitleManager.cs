using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Manager
{
    [DisallowMultipleComponent]
    public class TitleManager : MonoBehaviour
    {
        public enum eScreenMode
        {
            None,
            Title,
            StageSelect,
        }


        public void DoStart()
        {

        }

        public void DoContinue()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void DoExit()
        {
            Application.Quit();
        }
    }
}

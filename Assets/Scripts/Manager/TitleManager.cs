using Assets.Constants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        [SerializeField] private Canvas titleCanvas = null;
        [SerializeField] private Canvas stageSelectCanvas = null;
        [SerializeField] private Text stageSelectText = null;


        private int stageNumber;


        private eScreenMode _screenMode = eScreenMode.None;
        private eScreenMode ScreenMode
        {
            get
            {
                return _screenMode;
            }

            set
            {
                if (_screenMode == value) return;

                ChangeScreenMode(value);
                _screenMode = value;
            }
        }



        private void Awake()
        {
            ScreenMode = eScreenMode.Title;
        }



        private void ChangeScreenMode(eScreenMode mode)
        {
            new Dictionary<eScreenMode, GameObject>()
            {
                { eScreenMode.Title, titleCanvas.gameObject },
                { eScreenMode.StageSelect, stageSelectCanvas.gameObject },
            }
            .ToList().ForEach(pair => pair.Value.SetActive(pair.Key == mode));
        }



        public void DoStageSelect()
        {
            ScreenMode = eScreenMode.StageSelect;
        }

        public void DoContinue()
        {
            // セーブデータを読み込み、Scene切り替え
            StageSelectData.Number = 1;

            SceneManager.LoadScene(SceneName.GAME_SCENE);
        }

        public void DoExit()
        {
            Application.Quit();
        }



        public void DoLeftButton()
        {
            stageNumber --;
            stageSelectText.text = "Stage" + stageNumber;
        }

        public void DoRightButton()
        {
            stageNumber ++;
            stageSelectText.text = "Stage" + stageNumber;
        }

        public void DoStart()
        {
            StageSelectData.Number = stageNumber;

            SceneManager.LoadScene(SceneName.GAME_SCENE);
        }

        public void DoBack()
        {
            ScreenMode = eScreenMode.Title;
        }
    }
}

using Assets.Constants;
using Assets.Controller;
using Assets.Generator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Manager
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public enum eScreenMode
        {
            None,
            Start,
            Game,
            Pause,
            Clear,
            GameOver,
        }


        [SerializeField] private GameObject enemyGeneratorPrefab = null;
        [SerializeField] private GameObject protectedObjectPrefab = null;
        [SerializeField] private Canvas startCanvas = null;
        [SerializeField] private Canvas gameCanvas = null;
        [SerializeField] private Canvas pauseCanvas = null;
        [SerializeField] private Canvas clearCanvas = null;
        [SerializeField] private Canvas gameOverCanvas = null;


        private ProtectedObjectController protectedObject;

        private GameObject enemyGenerator;
        private EnemyGenerator enemyGeneratorComponent;
        private bool isPause = false;


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
            protectedObject = Instantiate(protectedObjectPrefab).GetComponent<ProtectedObjectController>();

            enemyGenerator = Instantiate(enemyGeneratorPrefab);
            enemyGeneratorComponent = enemyGenerator.GetComponent<EnemyGenerator>();

            StartCoroutine(GameLoop());
        }

        private void OnDestroy()
        {
            if(protectedObject != null) Destroy(protectedObject.gameObject);

            Destroy(enemyGenerator);
        }



        private void ChangeStage()
        {
            protectedObject.gameObject.SetActive(false);
            Destroy(protectedObject.gameObject);
            Destroy(enemyGenerator);

            protectedObject = Instantiate(protectedObjectPrefab).GetComponent<ProtectedObjectController>();

            enemyGenerator = Instantiate(enemyGeneratorPrefab);
            enemyGeneratorComponent = enemyGenerator.GetComponent<EnemyGenerator>();

            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            ScreenMode = eScreenMode.Start;
            yield return new WaitForSeconds(2.0f);

            ScreenMode = eScreenMode.Game;
            while (true)
            {
                if(enemyGeneratorComponent.GetEnemysCount() <= enemyGeneratorComponent.GetEraseEnemys())
                {
                    ScreenMode = eScreenMode.Clear;
                    break;
                }
                else if(protectedObject.GetHp() <= 0)
                {
                    ScreenMode = eScreenMode.GameOver;
                    break;
                }

                yield return null;
            }
        }

        private void ChangeScreenMode(eScreenMode mode)
        {
            new Dictionary<eScreenMode, GameObject>()
            {
                { eScreenMode.Start, startCanvas.gameObject },
                { eScreenMode.Game, gameCanvas.gameObject },
                { eScreenMode.Pause, pauseCanvas.gameObject },
                { eScreenMode.Clear, clearCanvas.gameObject },
                { eScreenMode.GameOver, gameOverCanvas.gameObject },
            }
            .ToList().ForEach(pair => pair.Value.SetActive(pair.Key == mode));


            switch (mode)
            {
                case eScreenMode.Start:
                    if (enemyGenerator.activeSelf) enemyGenerator.SetActive(false);
                    break;
                case eScreenMode.Game:
                    if (!enemyGenerator.activeSelf) enemyGenerator.SetActive(true);
                    break;
            }
        }



        public void DoPause()
        {
            isPause = (!isPause);
            Time.timeScale = (isPause) ? 0.0f : 1.0f;

            ScreenMode = (isPause) ? eScreenMode.Pause : eScreenMode.Game;
        }

        public void DoTitleScene()
        {
            SceneManager.LoadScene(SceneName.TITLE_SCENE);
        }

        public void DoNextStage()
        {
            //StageSelectData.Number ++;
            ChangeStage();
        }

        public void DoRetryStage()
        {
            ChangeStage();
        }
    }
}

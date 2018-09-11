using Assets.Constants;
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
        }

        [SerializeField] private GameObject arrowGeneratorPrefab;
        [SerializeField] private GameObject enemyGeneratorPrefab;
        [SerializeField] private Canvas startCanvas;
        [SerializeField] private Canvas gameCanvas;
        [SerializeField] private Canvas pauseCanvas;
        [SerializeField] private Canvas clearCanvas;

        private GameObject arrowGenerator;
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
            arrowGenerator = Instantiate(arrowGeneratorPrefab);
            enemyGenerator = Instantiate(enemyGeneratorPrefab);

            enemyGeneratorComponent = enemyGenerator.GetComponent<EnemyGenerator>();

            StartCoroutine(GameLoop());
        }

        private void OnDestroy()
        {
            Destroy(arrowGenerator);
            Destroy(enemyGenerator);

            Destroy(startCanvas);
            Destroy(gameCanvas);
            Destroy(pauseCanvas);
            Destroy(clearCanvas);
        }



        private void NextWave()
        {
            Destroy(enemyGenerator);

            enemyGenerator = Instantiate(enemyGeneratorPrefab);
            enemyGeneratorComponent = enemyGenerator.GetComponent<EnemyGenerator>();

            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            ScreenMode = eScreenMode.Start;
            yield return new WaitForSeconds(2.0f);

            ScreenMode = eScreenMode.Game;
            while (enemyGeneratorComponent.GetEnemysCount() > enemyGeneratorComponent.GetEraseEnemys()) yield return null;

            ScreenMode = eScreenMode.Clear;
        }

        private void ChangeScreenMode(eScreenMode mode)
        {
            new Dictionary<eScreenMode, GameObject>()
            {
                { eScreenMode.Start, startCanvas.gameObject },
                { eScreenMode.Game, gameCanvas.gameObject },
                { eScreenMode.Pause, pauseCanvas.gameObject },
                { eScreenMode.Clear, clearCanvas.gameObject },
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

        public void DoNextWave()
        {
            NextWave();
        }
    }
}

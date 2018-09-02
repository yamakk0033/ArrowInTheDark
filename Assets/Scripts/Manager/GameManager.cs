using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Manager
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public enum eScreenMode
        {
            Start,
            Game,
            Pause,
            Clear,
        }

        public static eScreenMode ScreenMode { get; set; } = eScreenMode.Start;

        private static eScreenMode lastScreenMode;

        [SerializeField] private GameObject arrowGeneratorPrefab;
        [SerializeField] private GameObject enemyGeneratorPrefab;
        [SerializeField] private Canvas startCanvasPrefab;
        [SerializeField] private Canvas gameCanvasPrefab;
        [SerializeField] private Canvas pauseCanvasPrefab;
        [SerializeField] private Canvas clearCanvasPrefab;


        private GameObject arrowGenerator = null;
        private GameObject enemyGenerator = null;
        private Canvas startCanvas = null;
        private Canvas gameCanvas  = null;
        private Canvas pauseCanvas = null;
        private Canvas clearCanvas = null;


        private bool isPause = false;


        private void Start()
        {
            arrowGenerator = Instantiate(arrowGeneratorPrefab);
            enemyGenerator = Instantiate(enemyGeneratorPrefab);
            startCanvas = Instantiate(startCanvasPrefab);
            gameCanvas  = Instantiate(gameCanvasPrefab);
            pauseCanvas = Instantiate(pauseCanvasPrefab);
            clearCanvas = Instantiate(clearCanvasPrefab);

            ScreenMode = eScreenMode.Start;
            lastScreenMode = ScreenMode;
            this.ChangeScreenMode(ScreenMode);

            StartCoroutine(StartModeLoop());
        }

        private void Update()
        {
            if (ScreenMode == lastScreenMode) return;

            this.ChangeScreenMode(ScreenMode);
            lastScreenMode = ScreenMode;
        }


        private IEnumerator StartModeLoop()
        {
            yield return new WaitForSeconds(2.0f);

            ScreenMode = eScreenMode.Game;
        }


        private void ChangeScreenMode(eScreenMode sm)
        {
            new Dictionary<eScreenMode, Canvas>()
            {
                { eScreenMode.Start, startCanvas },
                { eScreenMode.Game,  gameCanvas },
                { eScreenMode.Pause, pauseCanvas },
                { eScreenMode.Clear, clearCanvas },
            }
            .ToList().ForEach(pair => pair.Value.enabled = (pair.Key == sm));


            switch (sm)
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
    }
}

using Assets.Generator;
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

        [SerializeField] private GameObject arrowGeneratorPrefab = null;
        [SerializeField] private GameObject enemyGeneratorPrefab = null;
        [SerializeField] private Canvas startCanvasPrefab = null;
        [SerializeField] private Canvas gameCanvasPrefab = null;
        [SerializeField] private Canvas pauseCanvasPrefab = null;
        [SerializeField] private Canvas clearCanvasPrefab = null;


        private GameObject arrowGenerator = null;
        private GameObject enemyGeneratorObject = null;
        private Canvas startCanvas = null;
        private Canvas gameCanvas = null;
        private Canvas pauseCanvas = null;
        private Canvas clearCanvas = null;

        private EnemyGenerator enemyGenerator = null;

        private eScreenMode screenMode = eScreenMode.Start;
        private eScreenMode lastScreenMode = eScreenMode.Start;

        private bool isPause = false;


        private void Start()
        {
            arrowGenerator = Instantiate(arrowGeneratorPrefab);
            enemyGeneratorObject = Instantiate(enemyGeneratorPrefab);
            startCanvas = Instantiate(startCanvasPrefab);
            gameCanvas  = Instantiate(gameCanvasPrefab);
            pauseCanvas = Instantiate(pauseCanvasPrefab);
            clearCanvas = Instantiate(clearCanvasPrefab);

            enemyGenerator = enemyGeneratorObject.GetComponent<EnemyGenerator>();

            screenMode = eScreenMode.Start;
            lastScreenMode = screenMode;
            this.ChangeScreenMode(screenMode);

            StartCoroutine(StartModeLoop());
        }

        private void Update()
        {
            if (screenMode == lastScreenMode) return;

            this.ChangeScreenMode(screenMode);
            lastScreenMode = screenMode;
        }


        private IEnumerator StartModeLoop()
        {
            yield return new WaitForSeconds(2.0f);

            screenMode = eScreenMode.Game;


            while(enemyGenerator.GetEnemysCount() > enemyGenerator.GetEraseEnemys())
            {
                yield return null;
            }

            screenMode = eScreenMode.Clear;
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
                    if (enemyGeneratorObject.activeSelf) enemyGeneratorObject.SetActive(false);
                    break;
                case eScreenMode.Game:
                    if (!enemyGeneratorObject.activeSelf) enemyGeneratorObject.SetActive(true);
                    break;
            }
        }



        public void DoPause()
        {
            isPause = (!isPause);
            Time.timeScale = (isPause) ? 0.0f : 1.0f;

            screenMode = (isPause) ? eScreenMode.Pause : eScreenMode.Game;
        }
    }
}

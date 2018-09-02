using System.Collections.Generic;
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

        [SerializeField] private Canvas startCanvas;
        [SerializeField] private Canvas gameCanvas;
        [SerializeField] private Canvas pauseCanvas;
        [SerializeField] private Canvas clearCanvas;


        private void Start()
        {
            ScreenMode = eScreenMode.Start;
            lastScreenMode = ScreenMode;

            this.ChangeScreenMode(ScreenMode);
        }

        private void Update()
        {
            if (ScreenMode == lastScreenMode) return;

            this.ChangeScreenMode(ScreenMode);
            lastScreenMode = ScreenMode;
        }

        private void ChangeScreenMode(eScreenMode sm)
        {
            var dictionary = new Dictionary<eScreenMode, Canvas>()
            {
                { eScreenMode.Start, startCanvas },
                { eScreenMode.Game,  gameCanvas },
                { eScreenMode.Pause, pauseCanvas },
                { eScreenMode.Clear, clearCanvas },
            };

            foreach (var pair in dictionary)
            {
                bool isEnabled = (pair.Key == sm);
                if (pair.Value.enabled != isEnabled) pair.Value.enabled = isEnabled;
            }
        }
    }
}

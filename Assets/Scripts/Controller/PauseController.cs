using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects = null;

        private List<MonoBehaviour> list = null;

        private bool isPause = false;



        private void Start()
        {
            list = objects.SelectMany(o => o.GetComponents<MonoBehaviour>()).ToList();
        }



        public void OnClick()
        {
            isPause = (!isPause);
            Time.timeScale = (isPause) ? 0.0f : 1.0f;

            list.ForEach(i => i.enabled = !isPause);
        }
    }
}

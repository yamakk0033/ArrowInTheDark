using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class ManagerCreator : MonoBehaviour
    {
        [SerializeField] GameObject gameManagerPrefab = null;

        private GameObject gameManager = null;

        // Use this for initialization
        void Start()
        {
            gameManager = Instantiate(gameManagerPrefab);
        }
    }
}

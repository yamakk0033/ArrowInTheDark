using Assets.Constants;
using Assets.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Generator
{
    [DisallowMultipleComponent]
    public class AmmoGenerator : MonoBehaviour
    {
        private static readonly int MAX_COUNT = 20;


        [SerializeField] private GameObject ammoPrefab = null;

        private Queue<Tuple<GameObject, Rigidbody2D>> ammoQueue = new Queue<Tuple<GameObject, Rigidbody2D>>(MAX_COUNT);



        private void Awake()
        {
            BaseAmmoController.ParentGenerator = this;

            ammoQueue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_COUNT))
            {
                var go = Instantiate(ammoPrefab);
                go.SetActive(false);

                var rb = go.GetComponent<Rigidbody2D>();

                ammoQueue.Enqueue(Tuple.Create(go, rb));
            }
        }

        private void OnDestroy()
        {
            while (ammoQueue.Count > 0) Destroy(ammoQueue.Dequeue().Item1);
        }



        public void Appear(float x, float y, float rad, Vector2 force)
        {
            var tpl = ammoQueue.Dequeue();
            tpl.Item1.transform.position = new Vector3(x, y);
            tpl.Item1.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

            tpl.Item1.SetActive(true);
            tpl.Item2.AddForce(force, ForceMode2D.Impulse);
        }

        public void EraseWeapon(GameObject go, Rigidbody2D rb)
        {
            ammoQueue.Enqueue(Tuple.Create(go, rb));
        }
    }
}

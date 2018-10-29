﻿using Assets.Controller;
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
        [SerializeField] private GameObject barrel = null;

        private Queue<Tuple<GameObject, Rigidbody2D>> ammoQueue = new Queue<Tuple<GameObject, Rigidbody2D>>(MAX_COUNT);



        private void Awake()
        {
            BaseAmmoController.ParentGenerator = this;

            ammoQueue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_COUNT))
            {
                var go = Instantiate(ammoPrefab);
                var rb = go.GetComponent<Rigidbody2D>();
                go.SetActive(false);

                ammoQueue.Enqueue(Tuple.Create(go, rb));
            }
        }

        private void OnDestroy()
        {
            while (ammoQueue.Count > 0) Destroy(ammoQueue.Dequeue().Item1);
        }



        public void Appear(Vector2 force)
        {
            var tpl = ammoQueue.Dequeue();
            tpl.Item1.transform.position = barrel.transform.position;

            tpl.Item1.SetActive(true);
            tpl.Item2.AddForce(force, ForceMode2D.Impulse);
        }

        public void EraseWeapon(GameObject go, Rigidbody2D rb)
        {
            ammoQueue.Enqueue(Tuple.Create(go, rb));
        }
    }
}

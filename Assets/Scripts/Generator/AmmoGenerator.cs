using Assets.Constants;
using Assets.Controller;
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

        private Queue<GameObject> ammoQueue = new Queue<GameObject>(MAX_COUNT);



        private void Awake()
        {
            BaseAmmoController.ParentGenerator = this;

            ammoQueue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_COUNT))
            {
                var go = Instantiate(ammoPrefab);
                go.SetActive(false);

                ammoQueue.Enqueue(go);
            }
        }

        private void OnDestroy()
        {
            while (ammoQueue.Count > 0) Destroy(ammoQueue.Dequeue());
        }



        public void Appear(float x, float y, float rad, Vector2 force)
        {
            var item = ammoQueue.Dequeue();
            item.transform.position = new Vector3(x, y);
            item.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

            item.SetActive(true);
            item.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }

        public void EraseWeapon(GameObject go)
        {
            ammoQueue.Enqueue(go);
        }
    }
}

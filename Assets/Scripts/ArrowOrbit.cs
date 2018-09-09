using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    [DisallowMultipleComponent]
    public class ArrowOrbit : MonoBehaviour
    {
        private static readonly int MAX_COUNT = 100;

        [SerializeField] private GameObject origin;

        private List<GameObject> children = new List<GameObject>(MAX_COUNT);



        private void Awake()
        {
            children.Clear();
            foreach (int i in Enumerable.Range(0, MAX_COUNT))
            {
                var go = Instantiate(origin);
                go.transform.parent = gameObject.transform;

                children.Add(go);
            }
        }

        private void OnDestroy()
        {
            children.ForEach(c => Destroy(c));
            children.Clear();
        }



        public void UpdatePos(Vector2 gravity, Vector2 speed, Vector2 pos)
        {
            foreach (var item in children)
            {
                // 現在の速度に重力加速度を足す
                speed += gravity;
                pos += (speed * Time.fixedDeltaTime);

                speed += gravity;
                pos += (speed * Time.fixedDeltaTime);

                // 線のリストに加える
                item.transform.position = pos;
            }
        }
    }
}

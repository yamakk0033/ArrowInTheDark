using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class ArrowController : MonoBehaviour
    {
        private static readonly int MAX_COUNT = 20;
        private static readonly int MAX_ATTACHED_COUNT = 50;

        private static Queue<GameObject> queue = new Queue<GameObject>(MAX_COUNT);
        private static Queue<GameObject> attachedQueue = new Queue<GameObject>(MAX_ATTACHED_COUNT);


        private Rigidbody2D rigid2d = null;


        public static void Init(GameObject arrowPrefab, GameObject stickPrefab)
        {
            queue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_COUNT))
            {
                var go = Instantiate(arrowPrefab);
                go.SetActive(false);

                queue.Enqueue(go);
            }

            attachedQueue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_ATTACHED_COUNT))
            {
                var go = Instantiate(stickPrefab);
                go.SetActive(false);

                attachedQueue.Enqueue(go);
            }
        }

        public static void Appear(float x, float y, float rad, Vector2 force)
        {
            var item = queue.Dequeue();
            item.transform.position = new Vector3(x, y);
            item.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

            item.SetActive(true);
            item.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }




        // Use this for initialization
        private void Start()
        {
            rigid2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float rad = Mathf.Atan2(rigid2d.velocity.y, rigid2d.velocity.x);
            transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!gameObject.activeSelf) return;

            gameObject.SetActive(false);
            queue.Enqueue(gameObject);


            if (!collision.gameObject.activeSelf) return;

            var item = attachedQueue.Dequeue();
            if (item != null)
            {
                var tran = item.transform;

                tran.parent = null;
                tran.localPosition = gameObject.transform.localPosition;
                tran.localRotation = gameObject.transform.localRotation;
                tran.parent = collision.gameObject.transform;

                if (!item.activeSelf) item.SetActive(true);
                attachedQueue.Enqueue(item);
            }
        }
    }
}

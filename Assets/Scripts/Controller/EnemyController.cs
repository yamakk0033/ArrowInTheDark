using Assets.Constants;
using Assets.ScriptableObj;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {
        private static Queue<GameObject> queue = new Queue<GameObject>();
        //private static int EnemyTotalCount = 0;
        private static int EraseCounter = 0;


        private SpriteRenderer spriteRend = null;
        private Rigidbody2D rigid2d = null;
        private EnemyStatusData status = null;
        //private int hitPoint = 2;


        public static void Init(IReadOnlyCollection<EnemyStatusData> list)
        {
            queue.Clear();
            foreach (var item in list)
            {
                var go = Instantiate<GameObject>(item.Prefab);
                var controller = go.GetComponent<EnemyController>();

                controller.status = ScriptableObject.CreateInstance<EnemyStatusData>();
                controller.status.Set(item);

                go.SetActive(false);
                queue.Enqueue(go);
            }

            //EnemyTotalCount = list.Count;
            EraseCounter = 0;
        }

        public static void Appear(float x, float y)
        {
            if (queue.Count <= 0) return;

            var item = queue.Dequeue();
            item.transform.position = new Vector3(x, y);

            item.SetActive(true);
        }



        // Use this for initialization
        private void Start()
        {
            spriteRend = GetComponent<SpriteRenderer>();
            rigid2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rigid2d.velocity = Vector2.left * 30.0f * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            status.Hp --;
            spriteRend.color = (status.Hp == 1) ? Color.red : Color.white;

            if (status.Hp <= 0)
            {
                gameObject.SetActive(false);

                var items = Enumerable.Range(0, gameObject.transform.childCount).Select(i => gameObject.transform.GetChild(i)).ToArray();
                foreach (var item in items)
                {
                    if (item.gameObject.tag != TagName.ARROW) continue;
                    item.gameObject.SetActive(false);
                    item.parent = null;
                }

                EraseCounter ++;

                //status.Hp = 2;
                //queue.Enqueue(gameObject);
            }
        }
    }
}

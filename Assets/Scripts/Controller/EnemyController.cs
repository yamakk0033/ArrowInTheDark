using Assets.Constants;
using Assets.Generator;
using Assets.ScriptableObj;
using System.Linq;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {
        public static EnemyGenerator ParentGenerator { private get; set; }

        public EnemyStatusData Status { private get; set; }

        private SpriteRenderer spriteRend;
        private Rigidbody2D rigid2d;



        private void Awake()
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
            Status.Hp --;
            if (Status.Hp > 0) return;


            gameObject.SetActive(false);

            foreach (var arrow in Enumerable.Range(0, gameObject.transform.childCount).Select(i => gameObject.transform.GetChild(i)))
            {
                if (arrow.gameObject.tag != TagName.ARROW) continue;
                arrow.gameObject.SetActive(false);
                arrow.parent = null;
            }

            ParentGenerator.EraseEnemy(gameObject);
        }
    }
}

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
        public static EnemyGenerator parentGenerator { private get; set; }

        public EnemyStatusData Status { private get; set; }


        private SpriteRenderer spriteRend = null;
        private Rigidbody2D rigid2d = null;


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
            Status.Hp --;
            spriteRend.color = (Status.Hp == 1) ? Color.red : Color.white;

            if (Status.Hp <= 0)
            {
                gameObject.SetActive(false);

                var attachedArrows = Enumerable.Range(0, gameObject.transform.childCount).Select(i => gameObject.transform.GetChild(i)).ToArray();
                foreach (var arrow in attachedArrows)
                {
                    if (arrow.gameObject.tag != TagName.ARROW) continue;
                    arrow.gameObject.SetActive(false);
                    arrow.parent = null;
                }

                parentGenerator.EraseEnemy(gameObject);
            }
        }
    }
}

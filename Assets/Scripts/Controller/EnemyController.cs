using Assets.Constants;
using Assets.Generator;
using Assets.ScriptableObj;
using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {
        public static EnemyGenerator ParentGenerator { private get; set; }
        public static ProtectedObjectController ProtectController { private get; set; }


        public EnemyStatusData Status { private get; set; }


        private SpriteRenderer rend;


        private void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            transform
                .DOMove(Status.MoveStopPos, Status.MoveDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => StartCoroutine(AttackLoop()));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Status.Hp -= collision.gameObject.GetComponent<ArrowController>().GetAttack();

            rend.color = new Color(1.0f
                , (Status.Hp <= 0) ? 0 : ((float)Status.Hp / (float)Status.MaxHp)
                , (Status.Hp <= 0) ? 0 : ((float)Status.Hp / (float)Status.MaxHp)
                );

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



        private IEnumerator AttackLoop()
        {
            var wait = new WaitForSeconds(Status.AttackInterval);

            while (Status.Hp > 0)
            {
                yield return wait;
                ProtectController.Damage(Status.Attack);
            }
        }
    }
}

using Assets.Constants;
using Assets.Generator;
using Assets.ScriptableObj;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {
        public static EnemyGenerator ParentGenerator { private get; set; }

        public EnemyStatusData Status { private get; set; }



        private void Start()
        {
            transform.DOMove(Status.MoveStopPos, Status.MoveDuration);
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

using Assets.Generator;
using Assets.ScriptableObj;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] private StatusBaseData status = null;

        public static ArrowGenerator ParentGenerator { private get; set; }


        private Rigidbody2D rigid2d;



        private void Awake()
        {
            rigid2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var pos = transform.position;

            if (pos.x < -Screen.width || Screen.width < pos.x ||
                pos.y < -Screen.height || Screen.height < pos.y)
            {
                Erase();
            }

            float rad = Mathf.Atan2(rigid2d.velocity.y, rigid2d.velocity.x);
            transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!gameObject.activeSelf) return;

            Erase();


            if (!collision.gameObject.activeSelf) return;

            ParentGenerator.ActivePierceArrow(gameObject.transform, collision.gameObject.transform);
        }



        private void Erase()
        {
            gameObject.SetActive(false);
            ParentGenerator.EraseArrow(gameObject);
        }



        public int GetAttack()
        {
            return status.Attack;
        }
    }
}

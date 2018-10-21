using Assets.Generator;
using Assets.ScriptableObj;
using UnityEngine;

namespace Assets.Controller
{
    public class BaseAmmoController : MonoBehaviour
    {
        public static AmmoGenerator ParentGenerator { private get; set; }


        [SerializeField] protected BaseStatusData status = null;

        private Vector3 pos;
        private Rigidbody2D rb2d;


        private void Start()
        {
            pos = transform.position;
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (pos.x < -Screen.width  || Screen.width  < pos.x ||
                pos.y < -Screen.height || Screen.height < pos.y)
            {
                Erase();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!gameObject.activeSelf) return;
            this.Erase();
        }



        private void Erase()
        {
            gameObject.SetActive(false);
            ParentGenerator.EraseWeapon(gameObject, rb2d);
        }



        public int GetAttack()
        {
            return status.Attack;
        }
    }
}

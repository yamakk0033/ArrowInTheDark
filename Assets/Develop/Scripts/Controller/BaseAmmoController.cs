using Assets.Generator;
using Assets.ScriptableObj;
using System.Collections;
using UnityEngine;

namespace Assets.Controller
{
    public class BaseAmmoController : MonoBehaviour
    {
        public static AmmoGenerator ParentGenerator { private get; set; }


        [SerializeField] protected BaseStatusData status = null;
        private Rigidbody2D rb2d;



        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            StartCoroutine(ErasePeriod());
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

        private IEnumerator ErasePeriod()
        {
            yield return new WaitForSeconds(5.0f);
            this.Erase();
        }



        public int GetAttack()
        {
            return status.Attack;
        }
    }
}

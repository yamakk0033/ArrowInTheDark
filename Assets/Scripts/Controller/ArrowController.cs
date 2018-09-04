using Assets.Generator;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class ArrowController : MonoBehaviour
    {
        public static ArrowGenerator parentGenerator { private get; set; }

        private Rigidbody2D rigid2d = null;


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
            parentGenerator.EraseArrow(gameObject);

            if (!collision.gameObject.activeSelf) return;

            parentGenerator.CreatePierceArrow(gameObject.transform, collision.gameObject.transform);
        }
    }
}

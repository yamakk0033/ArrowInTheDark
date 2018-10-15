using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class ArrowController : BaseAmmoController
    {
        private Rigidbody2D rigid2d;


        private void Awake()
        {
            rigid2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float rad = Mathf.Atan2(rigid2d.velocity.y, rigid2d.velocity.x);
            transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
        }
    }
}

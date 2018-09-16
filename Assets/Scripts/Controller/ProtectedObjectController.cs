using Assets.ScriptableObj;
using UnityEngine;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class ProtectedObjectController : MonoBehaviour
    {
        [SerializeField] private StatusBaseData statusPrefab = null;


        private StatusBaseData status;


        private SpriteRenderer rend;


        private void Awake()
        {
            status = ScriptableObject.CreateInstance<StatusBaseData>();
            status.Set(statusPrefab);

            rend = GetComponent<SpriteRenderer>();
        }


        public void Damage(int attack)
        {
            status.Hp -= attack;
            rend.color = new Color(1.0f
                , (status.Hp <= 0) ? 0 : ((float)status.Hp / (float)status.MaxHp)
                , (status.Hp <= 0) ? 0 : ((float)status.Hp / (float)status.MaxHp)
                );
        }

        public int GetHp()
        {
            return status.Hp;
        }
    }
}

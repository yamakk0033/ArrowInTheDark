using UnityEngine;

namespace Assets.ScriptableObj
{
    [CreateAssetMenu]
    public class BaseStatusData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private string _characterName = "";
        [SerializeField] private int _hp = 0;
        [SerializeField] private int _attack = 0;


        public GameObject Prefab    { get { return _prefab;        } }
        public string CharacterName { get { return _characterName; } set { _characterName  = value; } }
        public int Hp               { get { return _hp;            } set { _hp             = Mathf.Clamp(value, 0, MaxHp); } }
        public int Attack           { get { return _attack;        } set { _attack         = value; } }

        public int MaxHp { get; private set; }


        public void Set(BaseStatusData status)
        {
            CharacterName = status.CharacterName;
            MaxHp = status.Hp;
            Hp = status.Hp;
            Attack = status.Attack;
        }
    }
}

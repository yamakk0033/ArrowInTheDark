using UnityEngine;

namespace Assets.ScriptableObj
{
    [CreateAssetMenu]
    public class EnemyStatusData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private string _name = "";
        [SerializeField] private int _hp = 0;
        [SerializeField] private int _attack = 0;
        [SerializeField] private Vector2 _spawnPos = Vector2.zero;
        [SerializeField] private Vector2 _moveStopPos = Vector2.zero;
        [SerializeField] private float _moveDuration = 0.0f;
        [SerializeField] private float _interval = 0.0f;


        public GameObject Prefab   { get { return _prefab;       } }
        public string Name         { get { return _name;         } set { _name         = value; } }
        public int Hp              { get { return _hp;           } set { _hp           = value; } }
        public int Attack          { get { return _attack;       } set { _attack       = value; } }
        public Vector2 SpawnPos    { get { return _spawnPos;     } set { _spawnPos     = value; } }
        public Vector2 MoveStopPos { get { return _moveStopPos;  } set { _moveStopPos  = value; } }
        public float MoveDuration  { get { return _moveDuration; } set { _moveDuration = value; } }
        public float Interval      { get { return _interval;     } set { _interval     = value; } }


        public void Set(EnemyStatusData status)
        {
            Name = status.Name;
            Hp = status.Hp;
            Attack = status.Attack;
            SpawnPos = status.SpawnPos;
            MoveStopPos = status.MoveStopPos;
            MoveDuration = status.MoveDuration;
            Interval = status.Interval;
        }
    }
}

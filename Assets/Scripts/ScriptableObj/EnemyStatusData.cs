using UnityEngine;

namespace Assets.ScriptableObj
{
    [CreateAssetMenu]
    public class EnemyStatusData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private string _name = "";
        [SerializeField] private int _hp = 0;
        [SerializeField] private int _strength = 0;
        [SerializeField] private float _speed = 0.0f;
        [SerializeField] private Vector2 _spawnVec = Vector2.zero;
        [SerializeField] private float _interval = 0.0f;

        public GameObject Prefab { get { return _prefab;   } }
        public string Name       { get { return _name;     } set { _name     = value; } }
        public int Hp            { get { return _hp;       } set { _hp       = value; } }
        public int Strength      { get { return _strength; } set { _strength = value; } }
        public float Speed       { get { return _speed;    } set { _speed    = value; } }
        public Vector2 SpawnVec  { get { return _spawnVec; } set { _spawnVec = value; } }
        public float Interval    { get { return _interval; } set { _interval = value; } }

        public void Set(EnemyStatusData status)
        {
            Name = status.Name;
            Hp = status.Hp;
            Strength = status.Strength;
            Speed = status.Speed;
            SpawnVec = status.SpawnVec;
            Interval = status.Interval;
        }
    }
}

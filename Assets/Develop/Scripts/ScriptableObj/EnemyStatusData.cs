using UnityEngine;

namespace Assets.ScriptableObj
{
    [CreateAssetMenu]
    public class EnemyStatusData : BaseStatusData
    {
        [SerializeField] private float _spawnPosY = 0.0f;
        [SerializeField] private Vector2 _moveStopPos = Vector2.zero;
        [SerializeField] private float _moveDuration = 0.0f;

        [SerializeField] private float _attackTime = 0.0f;
        [SerializeField] private float _attackInterval = 0.0f;
        [SerializeField] private float _spawnInterval = 0.0f;


        public float SpawnPosY     { get { return _spawnPosY;    } set { _spawnPosY    = value; } }
        public Vector2 MoveStopPos { get { return _moveStopPos;  } set { _moveStopPos  = value; } }
        public float MoveDuration  { get { return _moveDuration; } set { _moveDuration = value; } }

        public float AttackTime     { get { return _attackTime;     } set { _attackTime     = value; } }
        public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }
        public float SpawnInterval  { get { return _spawnInterval;  } set { _spawnInterval  = value; } }


        public void Set(EnemyStatusData status)
        {
            base.Set(status);

            SpawnPosY = status.SpawnPosY;
            MoveStopPos = status.MoveStopPos;
            MoveDuration = status.MoveDuration;

            AttackTime = status.AttackTime;
            AttackInterval = status.AttackInterval;
            SpawnInterval = status.SpawnInterval;
        }
    }
}

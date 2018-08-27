using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScriptableObj
{
    [CreateAssetMenu]
    public class StageData : ScriptableObject
    {
        [SerializeField] private List<EnemyStatusData> _statusList = new List<EnemyStatusData>();

        public List<EnemyStatusData> StatusList { get { return _statusList; } }
    }
}

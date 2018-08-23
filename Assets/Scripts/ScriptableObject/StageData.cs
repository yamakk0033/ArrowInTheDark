using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField] private List<Appear> _enemyAppear = new List<Appear>();


    public IReadOnlyList<Appear> EnemyAppear { get { return _enemyAppear; } }


    [Serializable]
    public struct Appear
    {
        [SerializeField] private EnemyStatus _state;
        [SerializeField] private float _interval;

        public EnemyStatus State { get { return _state; } }
        public float Interval { get { return _interval; } }
    }
}

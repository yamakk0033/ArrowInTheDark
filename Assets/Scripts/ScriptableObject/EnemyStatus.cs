using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatus : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _hp;
    [SerializeField] private float _speed;

    public string Name { get { return _name;  } }
    public int Hp      { get { return _hp;    } }
    public float Speed { get { return _speed; } }
}

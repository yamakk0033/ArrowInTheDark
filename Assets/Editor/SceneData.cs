using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneData : ScriptableObject
{
    public List<Param> list = new List<Param>();

    [System.Serializable]
    public class Param
    {
        public int No;
        public string Name;
    }

}
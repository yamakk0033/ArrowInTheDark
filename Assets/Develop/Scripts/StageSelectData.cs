using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageSelectData
{
    public static string StageDataName
    {
        get
        {
            return "StageData" + Number;
        }
    }

    public static int Number { get; set; }
}

using Assets.Controller;
using Assets.ScriptableObj;
using System.Collections;
using UnityEngine;

namespace Assets.Generator
{
    [DisallowMultipleComponent]
    public class EnemyGenerator : MonoBehaviour
    {
        private StageData stageData = null;


        // Use this for initialization
        private void Start()
        {
            stageData = Resources.Load<StageData>("StageData");

            EnemyController.Init(stageData.StatusList);

            StartCoroutine(LoopTest());
        }


        private IEnumerator LoopTest()
        {
            foreach (var status in stageData.StatusList)
            {
                yield return new WaitForSeconds(status.Interval);

                EnemyController.Appear(status.SpawnVec.x, status.SpawnVec.y);
            }
        }
    }
}

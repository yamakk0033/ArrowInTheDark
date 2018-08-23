using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;

    private float bufferTime = 5.0f;


    private StageData stageData = null;



    // Use this for initialization
    private void Start()
    {
        EnemyController.Init(enemyPrefab);

        stageData = Resources.Load<StageData>("StageData");
        Debug.Log(stageData.EnemyAppear.Count);

        StartCoroutine(LoopTest());
    }

    // Update is called once per frame
    private void Update()
    {
        //bufferTime += Time.deltaTime;
        //if (bufferTime > 1.0f)
        //{
        //    bufferTime = 0.0f;
        //    EnemyController.Appear(5, 0);
        //}
    }


    private IEnumerator LoopTest()
    {
        foreach(var item in stageData.EnemyAppear)
        {
            yield return new WaitForSeconds(item.Interval);

            EnemyController.Appear(5, 0);
        }
    }
}

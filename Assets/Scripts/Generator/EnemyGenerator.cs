using Assets.Controller;
using Assets.ScriptableObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Generator
{
    [DisallowMultipleComponent]
    public class EnemyGenerator : MonoBehaviour
    {
        private StageData stageData = null;

        private Queue<GameObject> queue = new Queue<GameObject>();
        private Queue<GameObject> EraseQueue = new Queue<GameObject>();

        private int enemysCount = 0;


        private void Awake()
        {
            stageData = Resources.Load<StageData>("StageData");

            this.InitEnemys(stageData.StatusList);

            StartCoroutine(SpawnLoop());
        }


        private IEnumerator SpawnLoop()
        {
            foreach (var status in stageData.StatusList)
            {
                yield return new WaitForSeconds(status.Interval);

                this.Appear(status.SpawnVec.x, status.SpawnVec.y);
            }
        }


        private void InitEnemys(IReadOnlyCollection<EnemyStatusData> list)
        {
            EnemyController.parentGenerator = this;

            queue.Clear();
            foreach (var item in list)
            {
                var go = Instantiate<GameObject>(item.Prefab);

                var status = ScriptableObject.CreateInstance<EnemyStatusData>();
                status.Set(item);

                var controller = go.GetComponent<EnemyController>();
                controller.Status = status;

                go.SetActive(false);
                queue.Enqueue(go);
            }

            EraseQueue.Clear();

            enemysCount = queue.Count;
        }

        private void Appear(float x, float y)
        {
            if (queue.Count <= 0) return;

            var item = queue.Dequeue();
            item.transform.position = new Vector3(x, y);

            item.SetActive(true);
            queue.Enqueue(item);
        }

        public void EraseEnemy(GameObject go)
        {
            EraseQueue.Enqueue(go);
        }


        public int GetEnemysCount()
        {
            return enemysCount;
        }

        public int GetEraseEnemys()
        {
            return EraseQueue.Count;
        }
    }
}

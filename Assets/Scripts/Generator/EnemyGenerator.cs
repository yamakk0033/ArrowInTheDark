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
        private Queue<GameObject> queue = new Queue<GameObject>();
        private Queue<GameObject> EraseQueue = new Queue<GameObject>();

        private StageData stageData;
        private int enemysCount;



        private void Awake()
        {
            stageData = Resources.Load<StageData>(StageSelectData.StageDataName);

            this.InitEnemys(stageData.StatusList);
        }

        private void OnDestroy()
        {
            Resources.UnloadAsset(stageData);

            while (queue.Count > 0) Destroy(queue.Dequeue());
            while (EraseQueue.Count > 0) Destroy(EraseQueue.Dequeue());
        }

        private void Start()
        {
            StartCoroutine(SpawnLoop());
        }



        private void InitEnemys(IReadOnlyCollection<EnemyStatusData> list)
        {
            EnemyController.ParentGenerator = this;

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

        private IEnumerator SpawnLoop()
        {
            foreach (var status in stageData.StatusList)
            {
                yield return new WaitForSeconds(status.Interval);

                this.Appear(status.SpawnPos.x, status.SpawnPos.y);
            }
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

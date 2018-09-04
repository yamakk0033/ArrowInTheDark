using Assets.Constants;
using Assets.Controller;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Generator
{
    [DisallowMultipleComponent]
    public class ArrowGenerator : MonoBehaviour
    {
        private static readonly int MAX_COUNT = 20;
        private static readonly int MAX_ATTACHED_COUNT = 50;

        private Queue<GameObject> queue = new Queue<GameObject>(MAX_COUNT);
        private Queue<GameObject> attachedQueue = new Queue<GameObject>(MAX_ATTACHED_COUNT);


        [SerializeField] private GameObject orbitPrefab = null;
        [SerializeField] private GameObject arrowPrefab = null;
        [SerializeField] private GameObject stickPrefab = null;
        [SerializeField] private GameObject bowPrefab = null;


        private ArrowOrbit arrowOrbit = null;
        private GameObject arrowObject = null;
        private GameObject bowObject = null;


        // Use this for initialization
        private void Awake()
        {
            this.InitArrows();

            arrowOrbit = new ArrowOrbit(orbitPrefab);

            arrowObject = Instantiate(stickPrefab);
            arrowObject.SetActive(false);

            bowObject = Instantiate(bowPrefab);
        }

        // Update is called once per frame
        private void Update()
        {
            if (TouchInput.GetLayerNo() == LayerNo.UI) return;

            var aimPos = Vector3.zero;
            float rad = 0f;
            var force = Vector2.zero;

            if (TouchInput.GetState() == TouchInput.State.Moved ||
                TouchInput.GetState() == TouchInput.State.Ended)
            {
                var beginPos = TouchInput.GetBeganWorldPosision(Camera.main);
                var endPos = TouchInput.GetWorldPosision(Camera.main);

                aimPos = (endPos - beginPos) + bowObject.transform.position;

                rad = Calculation.Radian(endPos, beginPos);
                force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * Calculation.Distance(endPos, beginPos) * 5;
            }



            if (TouchInput.GetState() == TouchInput.State.Moved)
            {
                arrowOrbit.Update(Physics2D.gravity * Time.fixedDeltaTime, force, aimPos);

                bowObject.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg + 90.0f);

                arrowObject.transform.position = new Vector3(aimPos.x, aimPos.y);
                arrowObject.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
            }

            if (TouchInput.GetState() == TouchInput.State.Began)
            {
                arrowOrbit.SetActive(true);
                arrowObject.SetActive(true);
            }
            else if (TouchInput.GetState() == TouchInput.State.Ended)
            {
                this.Appear(aimPos.x, aimPos.y, rad, force);

                arrowOrbit.SetActive(false);
                arrowObject.SetActive(false);
            }
        }


        private void InitArrows()
        {
            ArrowController.parentGenerator = this;

            queue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_COUNT))
            {
                var go = Instantiate(arrowPrefab);
                go.SetActive(false);

                queue.Enqueue(go);
            }

            attachedQueue.Clear();
            foreach (int i in Enumerable.Range(0, MAX_ATTACHED_COUNT))
            {
                var go = Instantiate(stickPrefab);
                go.SetActive(false);

                attachedQueue.Enqueue(go);
            }
        }

        public void Appear(float x, float y, float rad, Vector2 force)
        {
            var item = queue.Dequeue();
            item.transform.position = new Vector3(x, y);
            item.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

            item.SetActive(true);
            item.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }

        public void EraseArrow(GameObject go)
        {
            queue.Enqueue(go);
        }

        public void CreatePierceArrow(Transform arrow, Transform parent)
        {
            var item = attachedQueue.Dequeue();
            if (item == null) return;

            var tran = item.transform;

            tran.parent = null;
            tran.localPosition = arrow.localPosition;
            tran.localRotation = arrow.localRotation;
            tran.parent = parent;

            if (!item.activeSelf) item.SetActive(true);
            attachedQueue.Enqueue(item);
        }
    }
}

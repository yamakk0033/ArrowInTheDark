using Assets.Constants;
using Assets.Controller;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Generator
{
    [DisallowMultipleComponent]
    public class ArrowGenerator : MonoBehaviour
    {
        private static readonly int MAX_COUNT = 20;
        private static readonly int MAX_ATTACHED_COUNT = 50;

        [SerializeField] private GameObject bowPrefab;
        [SerializeField] private GameObject stickPrefab;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private GameObject orbitPrefab;

        private Queue<GameObject> queue = new Queue<GameObject>(MAX_COUNT);
        private Queue<GameObject> attachedQueue = new Queue<GameObject>(MAX_ATTACHED_COUNT);

        private GameObject bow;
        private GameObject stick;
        private GameObject orbit;

        private ArrowOrbit arrowOrbitComponent;



        private void Awake()
        {
            ArrowController.ParentGenerator = this;

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

            bow = Instantiate(bowPrefab);

            orbit = Instantiate(orbitPrefab);
            orbit.SetActive(false);

            stick = Instantiate(stickPrefab);
            stick.SetActive(false);

            arrowOrbitComponent = orbit.GetComponent<ArrowOrbit>();
        }

        private void OnDestroy()
        {
            while (queue.Count > 0) Destroy(queue.Dequeue());
            while (attachedQueue.Count > 0) Destroy(attachedQueue.Dequeue());

            Destroy(orbit);
            Destroy(stick);
            Destroy(bow);
        }

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

                aimPos = (endPos - beginPos) + bow.transform.position;

                rad = Calculation.Radian(endPos, beginPos);
                force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * Calculation.Distance(endPos, beginPos) * 5;
            }



            if (TouchInput.GetState() == TouchInput.State.Moved)
            {
                arrowOrbitComponent.UpdatePos(Physics2D.gravity * Time.fixedDeltaTime, force, aimPos);

                bow.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg + 90.0f);

                stick.transform.position = new Vector3(aimPos.x, aimPos.y);
                stick.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
            }

            if (TouchInput.GetState() == TouchInput.State.Began)
            {
                orbit.SetActive(true);
                stick.SetActive(true);
            }
            else if (TouchInput.GetState() == TouchInput.State.Ended)
            {
                this.Appear(aimPos.x, aimPos.y, rad, force);

                orbit.SetActive(false);
                stick.SetActive(false);
            }
        }



        private void Appear(float x, float y, float rad, Vector2 force)
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

        public void ActivePierceArrow(Transform arrow, Transform parent)
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

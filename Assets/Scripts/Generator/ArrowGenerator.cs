using Assets.Constants;
using Assets.Controller;
using UnityEngine;

namespace Assets.Generator
{
    [DisallowMultipleComponent]
    public class ArrowGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject OrbitPrefab = null;
        [SerializeField] private GameObject ArrowPrefab = null;
        [SerializeField] private GameObject StickPrefab = null;
        [SerializeField] private GameObject BowPrefab = null;


        private ArrowOrbit arrowOrbit = null;
        private GameObject arrowObject = null;
        private GameObject bowObject = null;


        // Use this for initialization
        private void Start()
        {
            ArrowController.Init(ArrowPrefab, StickPrefab);

            arrowOrbit = new ArrowOrbit(OrbitPrefab);

            arrowObject = Instantiate(StickPrefab);
            arrowObject.SetActive(false);

            bowObject = Instantiate(BowPrefab);
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
                ArrowController.Appear(aimPos.x, aimPos.y, rad, force);

                arrowOrbit.SetActive(false);
                arrowObject.SetActive(false);
            }
        }
    }
}

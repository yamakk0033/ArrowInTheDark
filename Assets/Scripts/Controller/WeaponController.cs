using Assets.Generator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject orbitPrefab = null;

        private GameObject orbit;
        private AmmoOrbit arrowOrbitComponent;
        private AmmoGenerator ammoGenerator;



        private void Awake()
        {
            orbit = Instantiate(orbitPrefab);
            orbit.SetActive(false);

            arrowOrbitComponent = orbit.GetComponent<AmmoOrbit>();

            ammoGenerator = GetComponent<AmmoGenerator>();
        }

        private void OnDestroy()
        {
            Destroy(orbit);
        }






        //private static readonly int MAX_COUNT = 20;


        //[SerializeField] private GameObject bowPrefab = null;
        //[SerializeField] private GameObject bulletPrefab = null;
        //[SerializeField] private GameObject orbitPrefab = null;

        //private Queue<GameObject> bulletQueue = new Queue<GameObject>(MAX_COUNT);

        //private GameObject bow;
        //private GameObject orbit;
        //private WeaponOrbit arrowOrbitComponent;



        //private void Awake()
        //{
        //    ArrowController.ParentGenerator = this;

        //    bulletQueue.Clear();
        //    foreach (int i in Enumerable.Range(0, MAX_COUNT))
        //    {
        //        var go = Instantiate(bulletPrefab);
        //        go.SetActive(false);

        //        bulletQueue.Enqueue(go);
        //    }

        //    bow = Instantiate(bowPrefab);

        //    orbit = Instantiate(orbitPrefab);
        //    orbit.SetActive(false);

        //    arrowOrbitComponent = orbit.GetComponent<WeaponOrbit>();
        //}

        //private void OnDestroy()
        //{
        //    while (bulletQueue.Count > 0) Destroy(bulletQueue.Dequeue());

        //    Destroy(orbit);
        //    Destroy(bow);
        //}

        //private void Update()
        //{
        //    if (TouchInput.GetLayerNo() == LayerNo.UI) return;

        //    var aimPos = Vector3.zero;
        //    float rad = 0f;
        //    var force = Vector2.zero;

        //    if (TouchInput.GetState() == TouchInput.State.Moved ||
        //        TouchInput.GetState() == TouchInput.State.Ended)
        //    {
        //        var beginPos = TouchInput.GetBeganWorldPosision(Camera.main);
        //        var endPos = TouchInput.GetWorldPosision(Camera.main);

        //        aimPos = (endPos - beginPos) + bow.transform.position;

        //        rad = Calculation.Radian(endPos, beginPos);
        //        force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * Calculation.Distance(endPos, beginPos) * 5;
        //    }



        //    if (TouchInput.GetState() == TouchInput.State.Moved)
        //    {
        //        arrowOrbitComponent.UpdatePos(Physics2D.gravity * Time.fixedDeltaTime, force, aimPos);

        //        bow.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg + 90.0f);
        //    }

        //    if (TouchInput.GetState() == TouchInput.State.Began)
        //    {
        //        orbit.SetActive(true);
        //    }
        //    else if (TouchInput.GetState() == TouchInput.State.Ended)
        //    {
        //        this.Appear(aimPos.x, aimPos.y, rad, force);

        //        orbit.SetActive(false);
        //    }
        //}
    }
}

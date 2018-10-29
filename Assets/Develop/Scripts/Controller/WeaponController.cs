using Assets.Constants;
using Assets.Generator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller
{
    public class WeaponController : MonoBehaviour
    {
        //[SerializeField] private GameObject orbitPrefab = null;
        [SerializeField] private GameObject fulcrum = null;

        //private GameObject orbit;
        //private AmmoOrbit ammoOrbitComponent;
        private AmmoGenerator ammoGenerator;



        private void Awake()
        {
            //orbit = Instantiate(orbitPrefab);
            //orbit.SetActive(false);

            //ammoOrbitComponent = orbit.GetComponent<AmmoOrbit>();
            ammoGenerator = GetComponentInChildren<AmmoGenerator>();
        }

        private void OnDestroy()
        {
            //Destroy(orbit);
        }

        private void Update()
        {
            if (TouchInput.GetLayerNo() == LayerNo.UI) return;

            if (TouchInput.GetState() == TouchInput.State.Ended)
            {
                //float rad = Calculation.Radian(transform.position, TouchInput.GetWorldPosision(Camera.main));
                float rad = fulcrum.transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
                var force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * 50.0f;

                //transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

                //Debug.Log(transform.rotation.eulerAngles);
                //Debug.Log(transform.localRotation.eulerAngles);
                //Debug.Log(fulcrum.transform.rotation.eulerAngles);
                //Debug.Log(fulcrum.transform.localRotation.eulerAngles);

                ammoGenerator.Appear(force);
            }
        }
    }
}

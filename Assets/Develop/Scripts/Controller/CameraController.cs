﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Controller
{
    [DisallowMultipleComponent]
    public class CameraController : MonoBehaviour
    {
        //[SerializeField] Transform target1 = null;
        //[SerializeField] Transform target2 = null;
        //[SerializeField] Vector2 offset = new Vector2(1, 1);

        //[SerializeField] Text textUI;

        //private float screenAspect = 0;
        //private Camera cameraComponent = null;

        //void Awake()
        //{
        //    screenAspect = (float)Screen.height / Screen.width;
        //    cameraComponent = GetComponent<Camera>();
        //}

        //void Update()
        //{
        //    UpdateCameraPosition();
        //    UpdateOrthographicSize();
        //}

        //void UpdateCameraPosition()
        //{
        //    // 2点間の中心点からカメラの位置を更新
        //    Vector3 center = Vector3.Lerp(target1.position, target2.position, 0.5f);
        //    transform.position = center + Vector3.forward * -10;
        //}

        //void UpdateOrthographicSize()
        //{
        //    // ２点間のベクトルを取得
        //    Vector3 targetsVector = AbsPositionDiff(target1, target2) + (Vector3)offset;

        //    // アスペクト比が縦長ならyの半分、横長ならxとアスペクト比でカメラのサイズを更新
        //    float targetsAspect = targetsVector.y / targetsVector.x;
        //    float targetOrthographicSize = 0;
        //    if (screenAspect < targetsAspect)
        //    {
        //        targetOrthographicSize = targetsVector.y * 0.5f;
        //    }
        //    else
        //    {
        //        targetOrthographicSize = targetsVector.x * (1 / cameraComponent.aspect) * 0.5f;
        //    }
        //    cameraComponent.orthographicSize = targetOrthographicSize;
        //}

        //Vector3 AbsPositionDiff(Transform target1, Transform target2)
        //{
        //    Vector3 targetsDiff = target1.position - target2.position;
        //    return new Vector3(Mathf.Abs(targetsDiff.x), Mathf.Abs(targetsDiff.y));
        //}
    }
}

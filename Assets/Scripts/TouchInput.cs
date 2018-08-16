﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class TouchInput : MonoBehaviour
{
    public enum State
    {
        /// <summary>
        /// タッチなし
        /// </summary>
        None,

        /// <summary>
        /// タッチ開始
        /// </summary>
        Began,

        /// <summary>
        /// タッチ移動
        /// </summary>
        Moved,

        /// <summary>
        /// タッチ終了
        /// </summary>
        Ended,
    }

    public enum Layer
    {
        None,
        UI,
    }


    private static Vector3 began = Vector3.zero;

    private static State state = State.None;
    private static Layer layer = Layer.None;


    public static State GetState()
    {
        return state;
    }

    public static Layer GetLayer()
    {
        return layer;
    }

    public static Vector3 GetBeganPosision()
    {
        return began;
    }

    public static Vector3 GetPosision()
    {
        return Input.mousePosition;
    }

    public static Vector3 GetBeganWorldPosision(Camera camera)
    {
        return camera.ScreenToWorldPoint(began);
    }

    public static Vector3 GetWorldPosision(Camera camera)
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }



    // Update is called once per frame
    private void Update()
    {
        state = State.None;
        if      (Input.GetMouseButtonDown(0)) state = State.Began;
        else if (Input.GetMouseButton(0))     state = State.Moved;
        else if (Input.GetMouseButtonUp(0))   state = State.Ended;


        if (state == State.Began)
        {
            began = Input.mousePosition;


            var pointer = new PointerEventData(EventSystem.current);
            var result = new List<RaycastResult>();

            pointer.position = Input.mousePosition;


            EventSystem.current.RaycastAll(pointer, result);
            if(result.Count > 0)
            {
                layer = Layer.UI;
                Debug.Log(layer);
            }
        }
        else if(state == State.None)
        {
            began = Vector3.zero;
            layer = Layer.None;
        }
    }
}

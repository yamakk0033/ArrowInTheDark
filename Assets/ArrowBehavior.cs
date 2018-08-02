using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowBehavior
{
    //private static readonly int MAX_ARROW_COUNT = 20;
    //private static readonly int MAX_STICK_COUNT = 5;


    //private Queue<GameObject> arrowStuck = new Queue<GameObject>(MAX_ARROW_COUNT);
    //private List<GameObject> arrowList = new List<GameObject>(MAX_ARROW_COUNT);


    //private Queue<GameObject> stickArrowStuck = new Queue<GameObject>(MAX_STICK_COUNT);
    //private Queue<GameObject> stickArrowQueue = new Queue<GameObject>(MAX_STICK_COUNT);


    //public ArrowBehavior(GameObject arrowPrefab, GameObject stickPrefab)
    //{
    //    foreach (int i in Enumerable.Range(0, MAX_ARROW_COUNT))
    //    {
    //        var go = Object.Instantiate(arrowPrefab) as GameObject;
    //        go.SetActive(false);

    //        arrowStuck.Enqueue(go);
    //    }

    //    foreach (int i in Enumerable.Range(0, MAX_STICK_COUNT))
    //    {
    //        var go = Object.Instantiate(stickPrefab) as GameObject;
    //        go.SetActive(false);

    //        stickArrowStuck.Enqueue(go);
    //    }
    //}

    //public void AddForce(float x, float y, float rad, Vector3 force)
    //{
    //    var tmp = arrowStuck.Dequeue();
    //    tmp.transform.position = new Vector3(x, y);
    //    tmp.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

    //    tmp.SetActive(true);
    //    tmp.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    //    arrowList.Add(tmp);
    //}

    //public void Update()
    //{
    //    arrowList.RemoveAll(item =>
    //    {
    //        if (item.GetComponent<ArrowController>().IsEnemyCollision)
    //        {
    //            var stick = (stickArrowStuck.Count > 0) ? stickArrowStuck.Dequeue() : stickArrowQueue.Dequeue();
    //            if (stick != null)
    //            {
    //                stick.transform.position = item.transform.position;
    //                stick.transform.rotation = item.transform.rotation;
    //                stick.SetActive(true);
    //                stickArrowQueue.Enqueue(stick);

    //                item.GetComponent<ArrowController>().VariableReset();
    //                arrowStuck.Enqueue(item);
    //                return true;
    //            }
    //        }

    //        return false;
    //    });
    //}
}

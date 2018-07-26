using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallGenerator : MonoBehaviour {

    [SerializeField] private GameObject orbitPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private GameObject bowPrefab;

    private Vector3 aimPos = new Vector3();

    private Vector3 startMousePos = new Vector3();
    private Vector3 endMousePos = new Vector3();




    private ArrowOrbit arrowOrbit = null;



    private static readonly int MAX_ARROW_COUNT = 20;
    private static readonly int MAX_STICK_COUNT = 50;

    private Queue<GameObject> arrowStuck = new Queue<GameObject>(MAX_ARROW_COUNT);
    private List<GameObject> arrowList = new List<GameObject>(MAX_ARROW_COUNT);
    private GameObject targetArrow = null;


    //private Queue<GameObject> stickArrowStuck = new Queue<GameObject>(MAX_STICK_COUNT);
    //private Queue<GameObject> stickArrowQueue = new Queue<GameObject>(MAX_STICK_COUNT);


    // Use this for initialization
    private void Start()
    {
        arrowOrbit = new ArrowOrbit(orbitPrefab);

        foreach (int i in Enumerable.Range(0, MAX_ARROW_COUNT))
        {
            var go = Instantiate(arrowPrefab) as GameObject;
            go.SetActive(false);

            arrowStuck.Enqueue(go);
        }

        //foreach (int i in Enumerable.Range(0, MAX_STICK_COUNT))
        //{
        //    var go = Instantiate(stickPrefab, transform) as GameObject;
        //    go.SetActive(false);

        //    stickArrowStuck.Enqueue(go);
        //}
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endMousePos = startMousePos;

            aimPos = (endMousePos - startMousePos) + bowPrefab.transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            aimPos = (endMousePos - startMousePos) + bowPrefab.transform.position;
        }

        float rad = GetRadian(endMousePos, startMousePos);
        var force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        force *= GetDistance(endMousePos, startMousePos) * 5;



        if (Input.GetMouseButton(0))
        {
            arrowOrbit.Update(Physics2D.gravity * Time.fixedDeltaTime, force, aimPos);

            bowPrefab.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg + 90.0f);

            if (targetArrow != null)
            {
                targetArrow.transform.position = new Vector3(aimPos.x, aimPos.y);
                targetArrow.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            arrowOrbit.SetActive(true);

            targetArrow = null;
            if (arrowStuck.Count > 0) targetArrow = arrowStuck.Dequeue();
            if (targetArrow != null)
            {
                targetArrow.transform.position = new Vector3(aimPos.x, aimPos.y);
                targetArrow.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
                targetArrow.GetComponent<Rigidbody2D>().gravityScale = 0f;
                targetArrow.SetActive(true);

                arrowList.Add(targetArrow);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            arrowOrbit.SetActive(false);

            if (targetArrow != null)
            {
                targetArrow.transform.position = new Vector3(aimPos.x, aimPos.y);
                targetArrow.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

                targetArrow.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                targetArrow.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

                targetArrow = null;
            }
        }


        //arrowList.RemoveAll(item =>
        //{
        //    if (item.GetComponent<ArrowController>().IsEnemyCollision)
        //    {
        //        var stick = (stickArrowStuck.Count > 0) ? stickArrowStuck.Dequeue() : stickArrowQueue.Dequeue();
        //        if (stick != null)
        //        {
        //            stick.transform.position = item.transform.position;
        //            stick.transform.rotation = item.transform.rotation;
        //            stick.SetActive(true);
        //            stickArrowQueue.Enqueue(stick);

        //            item.GetComponent<ArrowController>().Reset();
        //            arrowStuck.Enqueue(item);
        //            return true;
        //        }
        //    }

        //    return false;
        //});
    }

    private float GetDistance(Vector3 vec1, Vector3 vec2)
    {
        var v = vec2 - vec1;
        return Mathf.Sqrt(v.x * v.x + v.y * v.y);
    }

    private float GetRadian(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }
}

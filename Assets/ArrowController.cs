using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private static readonly int MAX_ARROW_COUNT = 20;
    private static readonly int MAX_ATTACHED_COUNT = 5;

    private static Queue<GameObject> arrowQueue = new Queue<GameObject>(MAX_ARROW_COUNT);
    private static Queue<GameObject> attachedQueue = new Queue<GameObject>(MAX_ATTACHED_COUNT);


    private Rigidbody2D rigid2d = null;



    public static void Init(GameObject arrowPrefab, GameObject stickPrefab)
    {
        arrowQueue.Clear();
        foreach (int i in Enumerable.Range(0, MAX_ARROW_COUNT))
        {
            var go = Instantiate<GameObject>(arrowPrefab);
            go.SetActive(false);

            arrowQueue.Enqueue(go);
        }

        attachedQueue.Clear();
        foreach (int i in Enumerable.Range(0, MAX_ATTACHED_COUNT))
        {
            var go = Instantiate<GameObject>(stickPrefab);
            go.SetActive(false);

            attachedQueue.Enqueue(go);
        }
    }

    public static void AddForce(float x, float y, float rad, Vector3 force)
    {
        var item = arrowQueue.Dequeue();
        item.transform.position = new Vector3(x, y);
        item.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);

        item.SetActive(true);
        item.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }




    // Use this for initialization
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float rad = Mathf.Atan2(rigid2d.velocity.y, rigid2d.velocity.x);
        transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var item = attachedQueue.Dequeue();
        if (item != null)
        {
            item.transform.position = gameObject.transform.position;
            item.transform.rotation = gameObject.transform.rotation;
            item.transform.parent = collision.gameObject.transform;
            if(!item.activeSelf) item.SetActive(true);
            attachedQueue.Enqueue(item);

            if (gameObject.activeSelf) gameObject.SetActive(false);
            arrowQueue.Enqueue(gameObject);
        }
    }
}

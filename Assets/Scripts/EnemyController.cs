using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int MAX_COUNT = 20;

    private static Queue<GameObject> queue = new Queue<GameObject>(MAX_COUNT);


    private Rigidbody2D rigid2d = null;
    private SpriteRenderer spriteRend = null;
    private int hitPoint = 2;


    public static void Init(GameObject prefab)
    {
        queue.Clear();
        foreach (int i in Enumerable.Range(0, MAX_COUNT))
        {
            var go = Instantiate<GameObject>(prefab);
            go.SetActive(false);

            queue.Enqueue(go);
        }
    }

    public static void Appear(float x, float y)
    {
        if (queue.Count <= 0) return;

        var item = queue.Dequeue();
        item.transform.position = new Vector3(x, y);

        item.SetActive(true);
    }



    // Use this for initialization
    private void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rigid2d.velocity = Vector2.left * 30.0f * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitPoint --;
        spriteRend.color = (hitPoint == 1) ? Color.red : Color.white;

        if(hitPoint <= 0)
        {
            gameObject.SetActive(false);

            var items = Enumerable.Range(0, gameObject.transform.childCount).Select(i => gameObject.transform.GetChild(i)).ToArray();
            foreach (var item in items)
            {
                if (item.gameObject.tag != TagConst.Arrow) continue;
                item.gameObject.SetActive(false);
                item.parent = null;
            }

            hitPoint = 2;
            queue.Enqueue(gameObject);
        }
    }
}

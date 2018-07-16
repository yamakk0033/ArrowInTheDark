using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject orbitPrefab;

    [SerializeField]
    private GameObject goAim;


    private List<GameObjectEx> list = new List<GameObjectEx>();



    private const int max_num = 100;



    private struct GameObjectEx
    {
        public GameObject go { get; set; }
        public Rigidbody2D rb { get; set; }

        public GameObjectEx(GameObject go, Rigidbody2D rb)
        {
            this.go = go;
            this.rb = rb;
        }
    }




    // Use this for initialization
    private void Start ()
    {
        foreach (int i in Enumerable.Range(0, max_num))
        {
            var go = Instantiate(orbitPrefab, transform) as GameObject;
            var rb = go.GetComponent<Rigidbody2D>();

            list.Add(new GameObjectEx(go, rb));
        }
    }

    // Update is called once per frame
    private void Update ()
    {
        float rad = getRadian(goAim.transform.position, transform.position);
        var force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        force *= getDistance(goAim.transform.position, transform.position) * 2;


        if (Input.GetMouseButtonUp(0))
        {
            var element = Instantiate(arrowPrefab, transform) as GameObject;
            var rb = element.GetComponent<Rigidbody2D>();

            element.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
            rb.AddForce(force, ForceMode2D.Impulse);
        }


        Vector2 gravity = Physics2D.gravity * Time.fixedDeltaTime;
        Vector2 currentSpeed = force;
        Vector2 prevPosition = transform.position;

        foreach (var item in list)
        {
            // 現在の速度に重力加速度を足す
            currentSpeed += gravity;

            Vector2 nextPosition = prevPosition + (currentSpeed * Time.fixedDeltaTime);

            // 線のリストに加える
            item.go.transform.position = nextPosition;

            prevPosition = nextPosition;
        }
    }


    private float getDistance(Vector3 vec, Vector3 vec2)
    {
        return Mathf.Sqrt((vec2.x - vec.x) * (vec2.x - vec.x) + (vec2.y - vec.y) * (vec2.y - vec.y));
    }

    private float getRadian(Vector3 vec, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec.y, vec2.x - vec.x);
    }
}

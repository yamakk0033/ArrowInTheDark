using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowOrbit
{
    private static readonly int MAX_ORBIT_COUNT = 100;

    // 
    private List<GameObject> orbitList = new List<GameObject>(MAX_ORBIT_COUNT);


    // Use this for initialization
    public ArrowOrbit(GameObject orbitPrefab)
    {
        foreach (int i in Enumerable.Range(0, MAX_ORBIT_COUNT))
        {
            var go = Object.Instantiate(orbitPrefab) as GameObject;
            go.SetActive(false);

            orbitList.Add(go);
        }
    }

    public void SetActive(bool act)
    {
        orbitList.ForEach(element => element.SetActive(act));
    }

    public void Update(Vector2 gravity, Vector2 speed, Vector2 pos)
    {
        Vector2 prevPos = pos;

        foreach (var item in orbitList)
        {
            // 現在の速度に重力加速度を足す
            speed += gravity;

            Vector2 nextPos = prevPos + (speed * Time.fixedDeltaTime);

            speed += gravity;
            nextPos += (speed * Time.fixedDeltaTime);

            // 線のリストに加える
            item.transform.position = nextPos;

            prevPos = nextPos;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class ArrowOrbit
{
    private static readonly int MAX_COUNT = 100;

    private GameObject parent = new GameObject();
    private List<GameObject> children = new List<GameObject>(MAX_COUNT);


    public ArrowOrbit(GameObject orbitPrefab)
    {
        parent.transform.position = Vector3.zero;
        parent.transform.rotation = Quaternion.identity;
        parent.transform.localScale = Vector3.one;
        parent.SetActive(false);


        children.Clear();
        foreach (int i in Enumerable.Range(0, MAX_COUNT))
        {
            var go = Object.Instantiate(orbitPrefab);
            go.transform.parent = parent.transform;

            children.Add(go);
        }
    }

    public void SetActive(bool isActive)
    {
        parent.SetActive(isActive);
    }

    public void Update(Vector2 gravity, Vector2 speed, Vector2 pos)
    {
        Vector2 prevPos = pos;

        foreach (var item in children)
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

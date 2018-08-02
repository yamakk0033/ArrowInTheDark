using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    [SerializeField] private GameObject orbitPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private GameObject bowPrefab;


    private Vector3 startMousePos = Vector3.zero;


    private ArrowOrbit arrowOrbit = null;
    private GameObject targetArrow = null;


    // Use this for initialization
    private void Start()
    {
        ArrowController.Init(arrowPrefab, stickPrefab);

        arrowOrbit = new ArrowOrbit(orbitPrefab);

        targetArrow = Instantiate(stickPrefab) as GameObject;
        targetArrow.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        var aimPos = Vector3.zero;
        float rad = 0f;
        var force = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            var endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            aimPos = (endMousePos - startMousePos) + bowPrefab.transform.position;

            rad = GetRadian(endMousePos, startMousePos);
            force = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * GetDistance(endMousePos, startMousePos) * 5;
        }



        if (Input.GetMouseButton(0))
        {
            arrowOrbit.Update(Physics2D.gravity * Time.fixedDeltaTime, force, aimPos);

            bowPrefab.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg + 90.0f);

            targetArrow.transform.position = new Vector3(aimPos.x, aimPos.y);
            targetArrow.transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
        }

        if (Input.GetMouseButtonDown(0))
        {
            arrowOrbit.SetActive(true);
            targetArrow.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            arrowOrbit.SetActive(false);

            ArrowController.AddForce(aimPos.x, aimPos.y, rad, force);
            targetArrow.SetActive(false);
        }
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

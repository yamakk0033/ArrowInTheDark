using UnityEngine;

public class ArrowController : MonoBehaviour {

    private Rigidbody2D rb;

    public bool IsEnemyCollision { get; private set; }

    public void Reset()
    {
        //if (gameObject.activeSelf) gameObject.SetActive(false);
        //IsEnemyCollision = false;
    }


    // Use this for initialization
    void Start ()
    {
        Reset();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        float rad = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
    }

    void OnCollisionEnter(Collision col)
    {
        IsEnemyCollision = true;
    }
}

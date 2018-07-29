using UnityEngine;

public class ArrowController : MonoBehaviour {

    public bool IsEnemyCollision { get; private set; }

    public Rigidbody2D Rigidbody { get; set; }

    // Use this for initialization
    void Start ()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        float rad = Mathf.Atan2(Rigidbody.velocity.y, Rigidbody.velocity.x);
        transform.rotation = Quaternion.Euler(0, 0, rad * Mathf.Rad2Deg);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsEnemyCollision = true;
    }

    public void VariableReset()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        IsEnemyCollision = false;
    }
}

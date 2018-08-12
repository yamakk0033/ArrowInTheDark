using UnityEngine;

[DisallowMultipleComponent]
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab = null;

    private float bufferTime = 5.0f;

    // Use this for initialization
    private void Start()
    {
        EnemyController.Init(enemyPrefab);
    }

    // Update is called once per frame
    private void Update()
    {
        bufferTime += Time.deltaTime;
        if (bufferTime > 1.0f)
        {
            bufferTime = 0.0f;
            EnemyController.Appear(5, 0);
        }
    }
}

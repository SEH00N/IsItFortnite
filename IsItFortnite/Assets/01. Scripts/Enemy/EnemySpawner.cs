using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject test;
    [SerializeField] float distance = 10f;
    private Transform player;

    private void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        player = GameManager.Instance.player;
        //Vector3 randPos = Random.insideUnitCircle;
        float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
        Vector3 randPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        Instantiate(test, player.position + randPos * distance, Quaternion.identity);
    }
}

using UnityEngine;

public class DustbinSpawner : MonoBehaviour
{
    public GameObject dustbinPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnMultiplier;

    private float timeElapsed;
    private float enemyCount;

    void Start()
    {
        timeElapsed = spawnTime - 1f;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= spawnTime)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject currBin = Instantiate(dustbinPrefab, spawnPoint.position, Quaternion.identity);
            timeElapsed = 0f;
            if(spawnTime >= 0.5f)
            spawnTime *= spawnMultiplier;
        }
    }

}

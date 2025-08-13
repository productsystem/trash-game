using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject dustbinPrefab;
    public int dustbinCount = 5;
    public float spawnInterval = 1f;
}

public class DustbinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeForWaves = 5f;

    public Wave[] waves;

    private int currWave = 0;
    private List<GameObject> aliveDustbins = new List<GameObject>();
    private bool isWaveOn = false;

    void Start()
    {
        StartCoroutine(WaveSpawner());
    }

    void Update()
    {
        if (isWaveOn && aliveDustbins.Count == 0)
        {
            isWaveOn = false;
            currWave++;
            if (currWave < waves.Length)
            {
                StartCoroutine(WaveSpawner());
            }
            else
            {
                //complete
            }
        }
    }

    IEnumerator WaveSpawner()
    {
        yield return new WaitForSeconds(timeForWaves);
        StartCoroutine(SpawnWave(waves[currWave]));
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isWaveOn = true;
        for (int i = 0; i < wave.dustbinCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject d = Instantiate(wave.dustbinPrefab, spawnPoint.position, Quaternion.identity);
            aliveDustbins.Add(d);
            Dustbin.BinType randType = (Dustbin.BinType)Random.Range(0, System.Enum.GetValues(typeof(Dustbin.BinType)).Length);
            d.GetComponent<Dustbin>().SetBinType(randType);
            //remove
            StartCoroutine(NullCheck(d));
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    IEnumerator NullCheck(GameObject dustbin)
    {
        yield return new WaitUntil(() => dustbin == null);
        for (int i = aliveDustbins.Count - 1; i >= 0; i--)
        {
            if (aliveDustbins[i] == null)
            {
                aliveDustbins.RemoveAt(i);
            }
        }
    }

}

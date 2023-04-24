using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingSpawner : MonoBehaviour
{
	public GameObject flyingEnemyPrefab;
	public Transform[] spawnPoints;
	public float waveDelay = 5f;
	public int[] waveSizes = { 1, 3, 4, 6 };

	private int currentWave = 0;
	public int enemiesRemaining = 0;

	private void Start()
	{
		Invoke("StartWave", waveDelay);
	}

	  private void StartWave()
    {
        enemiesRemaining = waveSizes[currentWave];

        for (int i = 0; i < enemiesRemaining; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(flyingEnemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }

        currentWave++;

        if (currentWave >= waveSizes.Length)
        {
            currentWave = 0;
        }
    }

    public void EnemyDied()
    {
        enemiesRemaining--;

        if (enemiesRemaining == 0)
        {
            StartCoroutine(WaveDelayCoroutine());
        }
    }

    private IEnumerator WaveDelayCoroutine()
    {
        yield return new WaitForSeconds(waveDelay);
        StartWave();
    }
}

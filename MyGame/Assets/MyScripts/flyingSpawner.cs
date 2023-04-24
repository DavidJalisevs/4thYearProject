using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class flyingSpawner : MonoBehaviour
{
	public GameObject flyingEnemyPrefab;
	public Transform[] spawnPoints;
	public float waveDelay = 5f;
	public int[] waveSizes = { 1, 3, 4, 6 };

	private int currentWave = 0;
    public int wavesCompleted = 0;
	public int enemiesRemaining = 0;
	public TMP_Text waveText;


	private void Start()
	{
		Invoke("StartWave", waveDelay);
	}

	private void StartWave()
    {
        enemiesRemaining = waveSizes[currentWave];

		waveText.text = "NEW WAVE OF ENEMIES FLYING IN";
		StartCoroutine(DisplayWaveAnnouncement(2.0f));

		for (int i = 0; i < enemiesRemaining; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(flyingEnemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }

        currentWave++;
        wavesCompleted = wavesCompleted + 1;


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

	private IEnumerator DisplayWaveAnnouncement(float displayTime)
	{
		yield return new WaitForSeconds(displayTime);
		waveText.text = ""; // Clear the text
	}
}

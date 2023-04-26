using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class flyingSpawner : MonoBehaviour
{
	public GameObject flyingEnemyPrefab;
	public Transform[] spawnPoints;
	public float waveDelay = 5f;// The delay between waves

	public int[] waveSizes = { 1, 3, 4, 6 };// The number of enemies in each wave
	private int currentWave = 0;// The current wave number
	public int wavesCompleted = 0;// The number of waves completed
	public int enemiesRemaining = 0;// The number of enemies remaining in the current wave

	public TMP_Text waveText;// The text object used to display wave announcements
	public TMP_Text waveText2;// The text object used to display wave announcements for vr


	private void Start()
	{
		Invoke("StartWave", waveDelay);
	}

	private void StartWave()
    {
		// Set the number of enemies for this wave
		enemiesRemaining = waveSizes[currentWave];

		// Display a message indicating that a new wave is starting
		waveText.text = "NEW WAVE OF ENEMIES FLYING IN";
		waveText2.text = "NEW WAVE OF ENEMIES FLYING IN";

		StartCoroutine(DisplayWaveAnnouncement(2.0f));

		// Spawn the enemies at random spawn points
		for (int i = 0; i < enemiesRemaining; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(flyingEnemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
		// Update the wave and completed wave counters
		currentWave++;
        wavesCompleted = wavesCompleted + 1;

		// If all waves have been completed, start from the beginning
		if (currentWave >= waveSizes.Length)
        {
            currentWave = 0;
        }
    }
	// This method is called when an enemy dies
	public void EnemyDied()
    {
        enemiesRemaining--;
		// If all enemies have been killed, start the next wave after a delay
		if (enemiesRemaining == 0)
        {
            StartCoroutine(WaveDelayCoroutine());
        }
    }
	// This coroutine adds a delay before starting the next wave
	private IEnumerator WaveDelayCoroutine()
    {
        yield return new WaitForSeconds(waveDelay);
        StartWave();
    }
	// This coroutine clears the wave announcement text after a delay
	private IEnumerator DisplayWaveAnnouncement(float displayTime)
	{
		yield return new WaitForSeconds(displayTime);
		waveText.text = ""; // Clear the text
		waveText2.text = ""; // Clear the text

	}
}

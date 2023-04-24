using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
	public TMP_Text npcCountText;
	public TMP_Text flyingEnemyCount;
	public TMP_Text scoreText;
	public int npcCount;
	private flyingSpawner flyingSpawnerScript;
	private bool increasedText = false;
	public int score= 0;

	// Start is called before the first frame update
	void Start()
	{
		flyingSpawnerScript = FindObjectOfType<flyingSpawner>();
		GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
		npcCount = npcs.Length;
		InvokeRepeating("IncrementScore", 2f, 2f);

	}

	// Update is called once per frame
	void Update()
	{
		GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
		npcCount = npcs.Length;

		npcCountText.text = "Humans Alive: " + npcCount;

		flyingEnemyCount.text = "Flying Enemy Alive: " + flyingSpawnerScript.enemiesRemaining;


		if (npcCount <= 0)
		{
			npcCountText.text = "Game Over";
			npcCountText.color = Color.red;
			npcCountText.rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f) * new Vector2(-Screen.width, -Screen.height);
			
		}
		if (npcCount <= 0 && !increasedText)
		{
			npcCountText.fontSize = npcCountText.fontSize * 3;
			increasedText = true;
			Time.timeScale = 0;

		}





	}

	//IEnumerator IncrementScore()
	//{
	//	while (true)
	//	{
	//		yield return new WaitForSeconds(3);
	//		score++;
	//		scoreText.text = "Score: " + score;
	//	}
	//}

	private void IncrementScore()
	{
		if (npcCount > 0)
		{
			score++;
			scoreText.text = "Score: " + score;
		}
	}

}


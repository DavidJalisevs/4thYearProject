using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

	[System.Serializable]
	public class GameState
	{
		public float scoreToSend;
		public int wavesCompletedToSend;
		public int playerHitToSend;
		public string sessionID = " ";
	}

	private string sessionIDDevice;

	public TMP_Text npcCountText;
	public TMP_Text flyingEnemyCount;
	public TMP_Text scoreText;
	public int npcCount;
	private bool increasedText = false;
	public int score = 0;
	private bool dataSent = false;

	private flyingSpawner flyingSpawnerScript;
	private fireBallScript fireBallscr;
	private HealthManager healthManagerScript;

	// Start is called before the first frame update
	void Start()
	{
		flyingSpawnerScript = FindObjectOfType<flyingSpawner>();
		GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
		npcCount = npcs.Length;
		InvokeRepeating("IncrementScore", 2f, 2f);
		sessionIDDevice = SystemInfo.deviceUniqueIdentifier;
		fireBallscr = FindObjectOfType<fireBallScript>();
		healthManagerScript = FindObjectOfType<HealthManager>();

	}

	// Update is called once per frame
	void Update()
	{
		GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
		npcCount = npcs.Length;

		npcCountText.text = "Humans Alive: " + npcCount;

		flyingEnemyCount.text = "Flying Enemy Alive: " + flyingSpawnerScript.enemiesRemaining;


		//if (npcCount <= 0 || healthManagerScript.healthAmount <= 0)
		//{
			

		//}

		if (healthManagerScript.healthAmount <= 0 || npcCount <= 0 && !increasedText)
		{
			npcCountText.text = "Game Over";
			npcCountText.color = Color.red;
			npcCountText.rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f) * new Vector2(-Screen.width, -Screen.height);
			increasedText = true;
			//npcCountText.fontSize = npcCountText.fontSize * 3;
			Time.timeScale = 0;

		}


		if (healthManagerScript.healthAmount <= 0 || npcCount <= 0 && !dataSent)
		{
			SendData();
			dataSent = true;
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



	public void SendData()
	{
		GameState data = new GameState();
		sessionIDDevice = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-") + sessionIDDevice;

		data.scoreToSend = score;//bulletsNumShotcount;
		data.wavesCompletedToSend = flyingSpawnerScript.wavesCompleted;// enemiesSpawnedCount;
		data.playerHitToSend = fireBallscr.fireBallCount;// enemiesKilledCount;
		//data.sulfurCountToSend = sulfurCount;
		//data.sodiumCountToSend = sodiumCount;
		//data.arsenicCountToSend = arsenicCount;
		//data.carbonCountToSend = carbonCount;
		//data.heliumCountToSend = heliumCount;
		//data.nitrogenCountToSend = nitrogenCount;
		//data.phosphorusCountToSend = phosphorusCount;

		data.sessionID = sessionIDDevice;
		string jsonData = JsonUtility.ToJson(data);
		StartCoroutine(postToServer.PostData(jsonData));
		Debug.Log(sessionIDDevice);

	}

}


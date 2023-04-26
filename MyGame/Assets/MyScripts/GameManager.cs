using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
	// Define a public GameState class that stores game state data
	[System.Serializable]
	public class GameState
	{
		public float scoreToSend;
		public int wavesCompletedToSend;
		public int playerHitToSend;
		public int buildingCountToSend;
		public string sessionID = " ";
	}

	private string sessionIDDevice; // unique session devuce id 

	public TMP_Text npcCountText; // npc text
	public TMP_Text flyingEnemyCount; // tex for enemy count
	public TMP_Text scoreText; // text for score


	public TMP_Text npcCountText2; // npc text for vr
	public TMP_Text flyingEnemyCount2; // tex for enemy count for vr
	public TMP_Text scoreText2; // text for score for vr


	public int npcCount; // Stores the number of non-playable characters (NPCs) in the game
	public int score = 0;// Stores the player's current score
	private int buildingCount = 999; // count of buildings in the scene 

	private bool dataSent = false; // checker for either data sent or not 
	private bool increasedText = false; // either text is increase or not

	private flyingSpawner flyingSpawnerScript; // Used to spawn flying enemies
	private fireBallScript fireBallscr; // Used to shoot fireballs
	private HealthManager healthManagerScript; // Used to manage player health



	// Start is called before the first frame update
	void Start()
	{
		// Get references to the flyingSpawner, fireBallScript, and HealthManager scripts
		flyingSpawnerScript = FindObjectOfType<flyingSpawner>();
		fireBallscr = FindObjectOfType<fireBallScript>();
		healthManagerScript = FindObjectOfType<HealthManager>();

		// Get the initial number of NPCs in the game
		GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
		npcCount = npcs.Length;

		// Call IncrementScore() every 2 seconds to increment the player's score
		InvokeRepeating("IncrementScore", 2f, 2f);

		// Get the device's unique identifier
		sessionIDDevice = SystemInfo.deviceUniqueIdentifier;
		sessionIDDevice = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-") + sessionIDDevice;
	

		Debug.Log(sessionIDDevice);
	}

	// Update is called once per frame
	void Update()
	{
		// Get the current number of NPCs in the game and update the NPC count text

		GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
		npcCount = npcs.Length;

		npcCountText.text = "Humans Alive: " + npcCount;
		npcCountText2.text = "Humans Alive: " + npcCount;
		// Update the flying enemy count text
		flyingEnemyCount.text = "Flying Enemy:" + flyingSpawnerScript.enemiesRemaining;
		flyingEnemyCount2.text = "Flying Enemy:" + flyingSpawnerScript.enemiesRemaining;


		//if (npcCount <= 0 || healthManagerScript.healthAmount <= 0)
		//{


		//}
		// Check if the game is over and display "Game Over" text if necessary

		if (healthManagerScript.healthAmount <= 0 || npcCount <= 0 && !increasedText)
		{
			npcCountText.text = "Game Over";
			npcCountText.color = Color.red;
			npcCountText.rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f) * new Vector2(-Screen.width, -Screen.height);
			increasedText = true;
			//npcCountText.fontSize = npcCountText.fontSize * 3;
			Time.timeScale = 0;

		}
		if (healthManagerScript.healthAmount <= 0 || npcCount <= 0 && !increasedText)
		{
			npcCountText2.text = "Game Over";
			npcCountText2.color = Color.red;
			npcCountText2.rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f) * new Vector2(-Screen.width, -Screen.height);
			increasedText = true;
			//npcCountText.fontSize = npcCountText.fontSize * 3;
			Time.timeScale = 0;

		}


		// Check if the game is over and send data to the servewr

		if (healthManagerScript.healthAmount <= 0 || npcCount <= 0 && !dataSent)
		{
			SendData();
		}

		if (buildingCount > 0)
		{
			// Find all game objects with the "buildings" tag
			GameObject[] buildings = GameObject.FindGameObjectsWithTag("building");

			// Count the number of buildings found
			buildingCount = buildings.Length;
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

	//score increment
	private void IncrementScore()
	{
		if (npcCount > 0)
		{
			score++;
			scoreText.text = "Score: " + score;
			scoreText2.text = "Score: " + score;

		}
	}


	//send data to anvil server
	public void SendData()
	{
		// Create a new GameState object to hold the game state data.
		GameState data = new GameState();

		// Add the session ID to the device timestamp.

		// Set the data to be sent to the server.
		data.scoreToSend = score;
		data.wavesCompletedToSend = flyingSpawnerScript.wavesCompleted;
		data.playerHitToSend = fireBallscr.fireBallCount;
		data.buildingCountToSend = buildingCount;
		// Set the session ID.
		data.sessionID = sessionIDDevice;

		// Convert the data to JSON.
		string jsonData = JsonUtility.ToJson(data);

		// Send the data to the server.
		StartCoroutine(postToServer.PostData(jsonData));

		// Log the session ID.
		Debug.Log(sessionIDDevice);
		dataSent = true;


	}

}


using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class postToServer : MonoBehaviour
{
	public static IEnumerator PostData(string jsonData)
	{
		// URL to Post the data.

		string url = "https://c00239534-analysis.anvil.app/_/api/metric";

		using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))

		{
			request.method = UnityWebRequest.kHttpVerbPOST;

			request.SetRequestHeader("Content-Type", "application/json");

			request.SetRequestHeader("Accept", "application/json");

			yield return request.SendWebRequest();

			if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)

				Debug.Log("Data successfully sent to the server");

			else

				Debug.Log("Error sending data to the server: Error " + request.responseCode);

		}

	}
}
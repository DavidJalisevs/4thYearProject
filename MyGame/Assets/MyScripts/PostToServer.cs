using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class postToServer : MonoBehaviour
{
	// This method takes in a JSON string and posts it to a specified URL.
	public static IEnumerator PostData(string jsonData)
	{
		// URL to Post the data.
		string url = "https://c00239534-analysis.anvil.app/_/api/metric";
		// Create a UnityWebRequest object with the given URL and data.
		using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))

		{
			// Set the HTTP method to POST.
			request.method = UnityWebRequest.kHttpVerbPOST;
			// Set the Content-Type header to indicate that the data being sent is in JSON format.
			request.SetRequestHeader("Content-Type", "application/json");
			// Set the Accept header to indicate that the expected response should also be in JSON format.
			request.SetRequestHeader("Accept", "application/json");
			// Send the request asynchronously and wait for the response.
			yield return request.SendWebRequest();
			// Check if the request was successful and the response code is OK (200).
			if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)

				Debug.Log("Data successfully sent to the server");

			else

				Debug.Log("Error sending data to the server: Error " + request.responseCode);

		}

	}
}
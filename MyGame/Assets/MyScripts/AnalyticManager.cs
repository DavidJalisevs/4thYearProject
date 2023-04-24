//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using UnityEngine;
//using UnityEngine.Networking;

//public class AnalyticManager : MonoBehaviour
//{
//	public static IEnumerator PostMethod(string jsonData)
//	{

//		string url = "http://44.211.62.253/upload_data";
//		using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
//		{
//			request.method = UnityWebRequest.kHttpVerbPOST;
//			request.SetRequestHeader("Content-Type", "application/json");
//			request.SetRequestHeader("Accept", "application/json");

//			yield return request.SendWebRequest();

//			if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
//				Debug.Log("Data successfully sent to the server");
//			else
//				Debug.Log("Error sending data to the server: Error " + request.responseCode);
//		}
//	}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class HttpPostHandler : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(Upload());
	}
	IEnumerator Upload()
	{
		string endpointURL = "https://asia-northeast1-one-phrase.cloudfunctions.net/api/new";
        WWWForm form = new WWWForm();
        form.AddField("userId", "0vnYn8ti8NIFNyWYNc0m");
        form.AddField("height", "1.5");
        form.AddField("lat", "43.0101");
        form.AddField("lng", "138.1212");
        //  see this...
        // https://docs.unity3d.com/ja/2018.4/Manual/UnityWebRequest-SendingForm.html
		// List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		// formData.Add(new MultipartFormDataSection("userId=0vnYn8ti8NIFNyWYNc0m&height=1.3&lat=43&lng=139"));
		// formData.Add( new MultipartFormFileSection("my file data", "myfile.txt") );

		UnityWebRequest www = UnityWebRequest.Post(endpointURL, form);
		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError)
		{
            Debug.Log("error!");
			Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
		}
		else
		{
            Debug.Log("successful");
            Debug.Log(www.downloadHandler.text);
			Debug.Log("Form upload complete!");
		}
	}


	// Update is called once per frame
	// void Update(){}
}

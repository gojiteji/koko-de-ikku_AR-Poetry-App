using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;

public class FirebaseController : MonoBehaviour
{
	// Start is called before the first frame update
	public Firebase.FirebaseApp app;
	public async void GetSenryus(float lat, float lng)
	{
		CollectionReference senryusRef = FirebaseFirestore.DefaultInstance.Collection("senryus");
		// Query query = senryusRef.WhereGreaterThan(); // 近くの川柳を絞り込みたかった
		QuerySnapshot querySnapshot = await senryusRef.GetSnapshotAsync();
		var items = new List<object>();
		foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
		{
			Debug.Log(documentSnapshot.Id + "!=================");
			Dictionary<string, object> senryu = documentSnapshot.ToDictionary();
			items.Add(senryu);
			// foreach (KeyValuePair<string, object> pair in city)
			// {
			// 	Debug.Log(pair.Key + pair.Value);
			// }
		}
		// Debug.Log(items[0]);
	}
	void Start()
	{
		Debug.Log("Hello, world! start したよ");
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
			{
				var dependencyStatus = task.Result;
				if (dependencyStatus == DependencyStatus.Available)
				{
					// Create and hold a reference to your FirebaseApp,
					// where app is a Firebase.FirebaseApp property of your application class.
					app = FirebaseApp.DefaultInstance;
					// enable analytics collection
					Debug.Log(app);
					FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
					// Set a flag here to indicate whether Firebase is ready to use by your app.
					GetSenryus(43, 138);
				}
				else
				{
					Debug.LogError(System.String.Format(
						"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
					// Firebase Unity SDK is not safe to use here.
				}
			});
	}

	// Update is called once per frame
	// void Update()
	// {
	// }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;

public class InitFirebase : MonoBehaviour
{
	// Start is called before the first frame update
	public Firebase.FirebaseApp app;
	public async void GetSenryus(){
			// ここでFirestoreに取得しに行ってみる
			Firebase.Firestore.CollectionReference senryusRef = Firebase.Firestore.FirebaseFirestore.DefaultInstance.Collection("senryus");
			Firebase.Firestore.DocumentReference docRef = senryusRef.Document("fEZaEdDvAu8Y1hKWkeSe");
			// CollectionReference citiesRef = db.Collection("cities");
			Debug.Log(docRef);
			DocumentSnapshot doc = await docRef.GetSnapshotAsync();
			Debug.Log(doc.Id);
			Dictionary<string, object> docDictionary = doc.ToDictionary();
			if (docDictionary.ContainsKey("imageURL"))
			{
					Debug.Log($"Point:{docDictionary["imageURL"]}");
			}
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
				GetSenryus();

				// docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
				// {
				// 	DocumentSnapshot snapshot = task.Result;
				// 	if (snapshot.Exists) {
				// 		Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
				// 		Dictionary<string, object> city = snapshot.ToDictionary();
				// 		foreach (KeyValuePair<string, object> pair in city) {
				// 			Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
				// 		}
				// 	} else {
				// 		Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
				// 	}
				// });

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



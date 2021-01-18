using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class InitFirebase : MonoBehaviour
{
	// Start is called before the first frame update
	public Firebase.FirebaseApp app;

	void Start()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		{
			var dependencyStatus = task.Result;
			if (dependencyStatus == DependencyStatus.Available)
			{
				// Create and hold a reference to your FirebaseApp,
				// where app is a Firebase.FirebaseApp property of your application class.
				app = FirebaseApp.DefaultInstance;

				// enable analytics collection
				FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

				// Set a flag here to indicate whether Firebase is ready to use by your app.
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
	void Update()
	{

	}
}

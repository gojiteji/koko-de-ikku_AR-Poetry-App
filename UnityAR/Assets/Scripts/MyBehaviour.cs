using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
 
public class MyBehaviour : MonoBehaviour {
    Texture myTexture;
    void Start() {
        StartCoroutine(GetTexture());
    }
 
    IEnumerator GetTexture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://optipng.sourceforge.net/pngtech/img/lena.png");
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        
        }
    }
}
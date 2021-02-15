using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Networking;

[RequireComponent(typeof(ARRaycastManager))]
public class json2gen : MonoBehaviour
{
    [SerializeField, Tooltip("AR空間に召喚する俳句")] GameObject haiku;
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
    var JsonStringList = new List<string>(){
                            "{\"image\":\"http://optipng.sourceforge.net/pngtech/img/lena.png\",\"lat\":43.087655,\"lng\":141.349499,\"height\":1.0,}",
                            "{\"image\":\"https://livedoor.blogimg.jp/mac_cafe/imgs/8/f/8f6435dd.jpg\",\"lat\":43.077655,\"lng\":141.349499,\"height\":1.0,}",
                            "{\"image\":\"https://cdn-ak.f.st-hatena.com/images/fotolife/m/mochiyu7uta/20201004/20201004085738.jpg\",\"lat\":43.067655,\"lng\":141.349499,\"height\":1.0,}"
                                };
    GenerateObjects(JsonStringList);
        for (int i = 0; i < JsonStringList.Count; i++)
        {
            GameObject go = Instantiate(haiku, new Vector3(i, 0, 2), Quaternion.identity) as GameObject;
            go.name = i.ToString();
            var jsonDic = Json.Deserialize(JsonStringList[i]) as Dictionary<string, object>;
            StartCoroutine(GetTexture(jsonDic["image"].ToString(),go));
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetTexture(string url,GameObject obj) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            obj.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Vector3 scale = obj.transform.localScale;
            // ここではx軸をマイナスにしている 
            scale.y = -scale.y;
            scale.x = -scale.x;
            // スケールを再設定
            obj.transform.localScale = scale;
        }
    }

    void GenerateObjects(List<string> args){
        for (int i = 0; i < args.Count; i++)
        {
            var jsonDic = Json.Deserialize(args[i]) as Dictionary<string, object>;
        }
    }
    
}

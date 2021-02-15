using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Networking;
using UnityEngine.UI; 

[RequireComponent(typeof(ARRaycastManager))]
public class json2gen : MonoBehaviour
{
    [SerializeField, Tooltip("AR空間に召喚する俳句")] GameObject haiku;
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Text targetText;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Start is called before the first frame update
    void Start()
    {


    var JsonStringList = new List<string>(){
                            "{\"image\":\"https://i.pinimg.com/originals/66/15/85/66158514b3b1b1c9769f3bc0938fcd2c.jpg\",\"lat\":43.087655,\"lng\":141.349499,\"height\":1.0,}",
                            "{\"image\":\"https://livedoor.blogimg.jp/mac_cafe/imgs/8/f/8f6435dd.jpg\",\"lat\":43.077655,\"lng\":141.349499,\"height\":1.0,}",
                            "{\"image\":\"https://cdn-ak.f.st-hatena.com/images/fotolife/m/mochiyu7uta/20201004/20201004085738.jpg\",\"lat\":43.067655,\"lng\":141.349499,\"height\":1.0,}"
                                };
    GenerateObjects(JsonStringList);
        for (int i = 0; i < JsonStringList.Count; i++)
        {
            var jsonDic = Json.Deserialize(JsonStringList[i]) as Dictionary<string, object>;
            var scale=100;
            GameObject go = Instantiate(haiku, new Vector3(((float)43.078269999999996- float.Parse(jsonDic["lat"].ToString()))*scale, 0,((float)141.3511359- float.Parse(jsonDic["lng"].ToString()))*scale ), Quaternion.identity) as GameObject;
            go.name = i.ToString();
            StartCoroutine(GetTexture(jsonDic["image"].ToString(),go));
        }
    StartCoroutine(updateMap());
    
    }

   private IEnumerator updateMap()
    {
    // 最初に、ユーザーがロケーションサービスを有効にしているかを確認する。無効の場合は終了する
        if (!Input.location.isEnabledByUser)
            yield break;

        // 位置を取得する前にロケーションサービスを開始する
        Input.location.Start();

        // 初期化が終了するまで待つ
        int maxWait = 20; // タイムアウトは20秒
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1); // 1秒待つ
            maxWait--;
        }

        // サービスの開始がタイムアウトしたら（20秒以内に起動しなかったら）、終了
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // サービスの開始に失敗したら終了
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // アクセスの許可と位置情報の取得に成功
            print("Location: " + Input.location.lastData.latitude + " "
                               + Input.location.lastData.longitude + " "
                               + Input.location.lastData.altitude + " "
                               + Input.location.lastData.horizontalAccuracy + " "
                               + Input.location.lastData.timestamp);
        }
            this.targetText.text = Input.location.lastData.latitude.ToString();

       
    }
    // Update is called once per frame
    void Update()
    {        
    //this.targetText.text = Input.location.lastData.latitude.ToString();


        
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

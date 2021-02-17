using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class next : MonoBehaviour
{
    public GameObject objGet;
    public Text targetText;
    private int counter = 0;
    private string[] ku = new string[] { "中の句", "下の句", "完成" };
    GameObject[] tag1_Objects; //代入用のゲームオブジェクト配列を用意
    private byte[] bytes1;
    private byte[] bytes2;
    private byte[] bytes3;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR  //エディタの場合
      Directory.CreateDirectory(Path.Combine(Application.dataPath, "Photos"));
#elif UNITY_IOS  //iOSの場合
      Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Photos"));
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
     public int resWidth = 20; 
     public int resHeight = 300;
 
     private bool takeHiResShot = false;
 
     public static string ScreenShotName(int width, int height,int num) {
         return Application.persistentDataPath +"/"+num.ToString()+".jpg";
         //return Application.dataPath+"/Photos/"+num.ToString()+".png";
         //return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
         //                     Application.dataPath, 
         //                     width, height, 
         //                     System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
     }
 
     public void TakeHiResShot() {
         takeHiResShot = true;
     }
 
    public void OnNext()
    {

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        GetComponent<Camera>().targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        if(counter==0){
            bytes1 = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight,counter);
        }else  if(counter==1){
            bytes2 = screenShot.EncodeToPNG();
            //string filename = ScreenShotName(resWidth, resHeight,counter);
            //System.IO.File.WriteAllBytes(filename, bytes2);
        }else  if(counter==2){
            bytes3 = screenShot.EncodeToPNG();
            //string filename = ScreenShotName(resWidth, resHeight,counter);
            //System.IO.File.WriteAllBytes(filename, bytes3);
            //byte[] tmpbytes=new byte[bytes1.Length+bytes2.Length+bytes3.Length];
            //bytes1.CopyTo(tmpbytes,0);
            //bytes2.CopyTo(tmpbytes,bytes1.Length-10);
            //bytes3.CopyTo(tmpbytes,bytes1.Length+bytes2.Length-10);
            //System.IO.File.WriteAllBytes(Application.dataPath+"/Photos/"+"tmp.png", tmpbytes);
            //Debug.Log("worknig");

        }



        tag1_Objects = GameObject.FindGameObjectsWithTag("ink");
        //ScreenCapture.CaptureScreenshot("./" + counter.ToString() + ".png");
        for (int i = 0; i < tag1_Objects.Length; i++)
        {
            Destroy(tag1_Objects[i]);
        }
        if (counter < 2)
        {
            objGet.transform.position += new Vector3(Screen.currentResolution.width / 3, 0, 0);
            this.targetText.text = ku[counter++];
        }else{
            //image post
            StartCoroutine(Upload());
            SceneManager.LoadScene("Scenes/title");
        }
    }


    IEnumerator Upload()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add( new MultipartFormDataSection("lat=43.0782624&lng=141.351128&userId=0vnYn8ti8NIFNyWYNc0m&height=1.0"));
        form.Add( new MultipartFormFileSection("image1",bytes1,"image1.png", "image/png"));

        UnityWebRequest www = UnityWebRequest.Post("https://asia-northeast1-one-phrase.cloudfunctions.net/api/new", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }


    public void OnClear()
    {
        tag1_Objects = GameObject.FindGameObjectsWithTag("ink");
        //ScreenCapture.CaptureScreenshot("Photos/" + counter.ToString() + "hoge.png");
        for (int i = 0; i < tag1_Objects.Length; i++)
        {
            Destroy(tag1_Objects[i]);
        }
    }

}

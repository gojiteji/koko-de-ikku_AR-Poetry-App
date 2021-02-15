using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class next : MonoBehaviour
{
    public GameObject objGet;
    public Text targetText;
    private int counter = 0;
    private string[] ku = new string[] { "中の句", "下の句", "完成" };
    GameObject[] tag1_Objects; //代入用のゲームオブジェクト配列を用意

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
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidth, resHeight,counter);
        System.IO.File.WriteAllBytes(filename, bytes);



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

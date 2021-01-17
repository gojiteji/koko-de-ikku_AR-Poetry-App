using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    public void OnYomu()
{
    SceneManager.LoadScene("Scenes/yomu");
}
    public void OnMiru()
{
    SceneManager.LoadScene("Scenes/miru");
}

}

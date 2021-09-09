using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//menuteste
public class MenuCtrl : MonoBehaviour
{
    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

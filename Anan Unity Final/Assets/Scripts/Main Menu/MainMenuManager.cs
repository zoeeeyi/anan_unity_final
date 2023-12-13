using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}

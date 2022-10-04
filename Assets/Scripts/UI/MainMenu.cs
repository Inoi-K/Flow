﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadLevel() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit() {
        Application.Quit();
    }
}

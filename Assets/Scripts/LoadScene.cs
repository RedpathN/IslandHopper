﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
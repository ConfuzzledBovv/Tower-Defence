﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public void ButtonPress(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMenu : MonoBehaviour
{
    public Transform MenuTransform;
    
    public Button BackButton;


 

    public Transform buttonsTransform;
    public LevelButton LevelButtonPrefab;

   
    
    private void Start()
    {
        var LevelCount = GameManager.instance.LevelConfigs.Length - 1;
        
        //loadSave
        int lastLevelIndex = GameManager.instance.LastLevelIndex;
        
        for (int i = 0; i < LevelCount; i++)
        {
            var isLock = true;

            if (i < lastLevelIndex) isLock = false;

            if (i + 1 == 1) isLock = false;
            
            var b = Instantiate(LevelButtonPrefab, buttonsTransform);
            b.Init(i + 1, isLock);
            b.OnClick += OnSelectLevel;
        }
        
        BackButton.onClick.AddListener(Close);
    }

    public void Open()
    {
        MenuTransform.gameObject.SetActive(true);
    }

    public void Close()
    {
        MenuTransform.gameObject.SetActive(false);
    }

    private void OnSelectLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}

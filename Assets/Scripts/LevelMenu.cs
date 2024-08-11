using System;
using System.Collections;
using System.Collections.Generic;
using Core.Game.GamePopWindow;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMenu : MonoBehaviour
{
   // public Transform MenuTransform;
    
    public Button BackButton;

    public MainMenu MainMenu;
 

    public Transform buttonsTransform;
    public LevelButton LevelButtonPrefab;

    [SerializeField] private WindowScaleAnimationSettings _windowScaleAnimationSettings;
    
    private WindowScaleAnimation _windowScaleAnimation;
   
    
    private void Start()
    {
        var LevelCount = GameManager.instance.LevelConfigs.Length - 1;
        
        //loadSave
        int lastLevelIndex = GameManager.instance.LastLevelIndex;
        
        foreach (Transform g in buttonsTransform.GetComponentsInChildren<Transform>())
        {
            g.gameObject.SetActive(false);
        }
        buttonsTransform.gameObject.SetActive(true);
        
        for (int i = 0; i < LevelCount; i++)
        {
            var isLock = true;

            if (i < lastLevelIndex) isLock = false;

            if (i + 1 == 1) isLock = false;
            
            var b = Instantiate(LevelButtonPrefab, buttonsTransform);
            b.Init(i + 1, isLock);
            b.OnClick += OnSelectLevel;
        }

        _windowScaleAnimation = new WindowScaleAnimation(_windowScaleAnimationSettings);
        
        BackButton.onClick.AddListener(Close);
    }

    public void Open()
    {
        //MenuTransform.gameObject.SetActive(true);
        _windowScaleAnimation.Raise();
    }

    public void Close()
    {
        _windowScaleAnimation.Lower();
       // MenuTransform.gameObject.SetActive(false);
        
    }

    private void OnSelectLevel(int level)
    {
        if (level == 1)
        {
            Close();
            
            MainMenu.OpenTutorial();

            return;
        }
        
        SceneManager.LoadScene(level);
    }
}

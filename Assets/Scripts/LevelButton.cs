using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour
{
    public Transform LockTransform;
    public Button Button;
    public TextMeshProUGUI TextMesh;

    private int _numLevel;
    
    
    public void Init(int levelNum, bool isLock = true)
    {
        
        
        _numLevel = levelNum;

        TextMesh.text = levelNum.ToString();
        
        if (isLock)
        {
            LockTransform.gameObject.SetActive(true);
            Button.interactable = false;
            return;
        }
        else
        {
            LockTransform.gameObject.SetActive(false);
            Button.interactable = true;
        }
        
        Button.onClick.AddListener((Call));
        
    }

    private void Call()
    {
        OnClick?.Invoke(_numLevel);
    }

    public Action<int> OnClick;
}

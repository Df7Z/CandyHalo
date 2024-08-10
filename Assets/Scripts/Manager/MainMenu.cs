using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    
    private AudioSource sound;


    public Text bestScore;
    [SerializeField]
    private Sprite[] soundBtnSprites; 

    public Button playBtn;
    public Button soundBtn, slideBtn;
    public string gameScene;
    
    private bool hidden;
    private bool canTouchSlideButton;

    void Start()
    {
        bestScore.text = "Best" + "\n" + GameManager.instance.hiScore;
        canTouchSlideButton = true;
        hidden = true;
        sound = GetComponent<AudioSource>();
       
        playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    
      


        soundBtn.GetComponent<Button>().onClick.AddListener(() => { SoundBtn(); });  
       

        
        if (PlayerPrefs.GetInt("gameMuted") == 0)
        {
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[0];
            AudioListener.volume = 1f;
        }
        else if (PlayerPrefs.GetInt("gameMuted") == 1)
        {
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[1];
            AudioListener.volume = 0f;

        }
    }
    
    void PlayBtn()
    {
        sound.Play();
        SceneManager.LoadScene(gameScene);
    }

    
    void SoundBtn()
    {
        sound.Play();

        if (GameManager.instance.isMusicOn)
        {
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[1];
            GameManager.instance.isMusicOn = false;
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("gameMuted", 1);
            GameManager.instance.Save();
        }
        else
        {
            soundBtn.transform.GetChild(0).GetComponent<Image>().sprite = soundBtnSprites[0];
            GameManager.instance.isMusicOn = true;
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("gameMuted", 0);
            GameManager.instance.Save();

        }
    }
    

}
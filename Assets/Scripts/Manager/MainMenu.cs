using System;
using UnityEngine;

using Core.Game.GamePopWindow;
using Game.TextMeshProExtenstion;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenu : MonoBehaviour
{
    
    private AudioSource sound;
    [SerializeField] private AudioClip[] PrintSound;

    public Text bestScore;
    [SerializeField]
    private Sprite[] soundBtnSprites;
    public Transform MainMenuTransform;
    public Transform TutorialTransform;
    
    public Button playBtn;
    public Button ExitButton;
    public Button soundBtn, slideBtn;
    public string gameScene;
    
    private bool hidden;
    private bool canTouchSlideButton;

    [SerializeField] private WindowScaleAnimationSettings _windowScaleAnimationSettingsOpenTutorial;
    [SerializeField] private AnimatedPrinterSettings _animatedPrinterSettingsTutorial;
    
    private WindowScaleAnimation _windowScaleAnimationOpenTutorial;

    private void OnDestroy()
    {
        _animatedPrinter?.Terminate();
    }

    void Start()
    {
        bestScore.text = "Best" + "\n" + GameManager.instance.hiScore;
        canTouchSlideButton = true;
        hidden = true;
        sound = GetComponent<AudioSource>();
       
        playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });

        _windowScaleAnimationOpenTutorial = new WindowScaleAnimation(_windowScaleAnimationSettingsOpenTutorial);

        soundBtn.GetComponent<Button>().onClick.AddListener(() => { SoundBtn(); });  
       
        ExitButton.onClick.AddListener((() =>
        {
            Debug.Log("Exit");
            Application.Quit();
        }));

        
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

    public void PlaySound()
    {
        sound.Play();
    }
    
    void PlayBtn()
    {
        sound.Play();

        if (GameManager.instance.LastLevelIndex > 1)
        {
            GameManager.instance.LoadLevel(GameManager.instance.LastLevelIndex);
        }
        else
        {
            OpenTutorial();
        }
        
        //SceneManager.LoadScene(gameScene);
    }

    private AnimatedPrinter _animatedPrinter;
    
    public void OpenTutorial()
    {
        sound.Play();
        
        TutorialTransform.gameObject.SetActive(true);

        _animatedPrinter = new AnimatedPrinter(_animatedPrinterSettingsTutorial);
        _animatedPrinter.OnPrint += () => { sound.PlayOneShot(PrintSound[Random.Range(0, PrintSound.Length - 1)]); };
        _animatedPrinter.Start();
        
        _windowScaleAnimationOpenTutorial.Raise();
    //    MainMenuTransform.gameObject.SetActive(false);
    
    }
    
    public void TutorialOK()
    {
        GameManager.instance.LoadLevel(1);
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
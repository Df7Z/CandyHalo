using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameGui : MonoBehaviour {

    private AudioSource sound;
    public GameObject gameOn , gameOver, gameWin;
    public TextMeshProUGUI score, ingameScore;
    public Color[] medalCols;
    public Image medal;
    public Button homeBtn, retryBtn;
    public string mainMenu;
    int i = 0;
    int jSafe = 0;

    public Button NextLevel;
    
    private int NbTimePlayed;

    private LevelConfig levelConfig;
    
	void Start ()
    {
        sound = GetComponent<AudioSource>();
        GameManager.instance.currentScore = 0;
        //ingameScore.text = "" + GameManager.instance.currentScore;
        homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });   
        retryBtn.GetComponent<Button>().onClick.AddListener(() => { RetryBtn(); });    
        jSafe = 0;

        levelConfig = GameManager.instance.GetLevelConfig();
        
        NextLevel.onClick.AddListener((() =>
        {
            GameManager.instance.NextLevel();
        }));
    }

    private bool _stop;
    
    private void LevelComplete()
    {
        _stop = true;
        
       // Debug.Log("Level complete");
        GameManager.instance.isGameOver = true; //Чтобы всё остановить,,,
        
        var next = SceneManager.GetActiveScene().buildIndex + 1;

        if (GameManager.instance.LastLevelIndex < next)
        {
            GameManager.instance.LastLevelIndex = next;
        }
        
        gameOn.SetActive(false);
        gameOver.SetActive(false);
        gameWin.SetActive(true);
    }
    
    void Update()
    {
        if (_stop) return;
        
        ingameScore.text = IngameScoreText();

        if (GameManager.instance.currentScore >= levelConfig.ScoreToWin)
        {
            LevelComplete();
            return;
        }

    /*
    if (GameManager.instance.currentScore >= GameManager.instance.hiScore)
    {
        GameManager.instance.hiScore = GameManager.instance.currentScore;
        GameManager.instance.Save();
    }*/

        if (GameManager.instance.isGameOver)
        {

            score.text = IngameScoreText();
            //best.text = GameManager.instance.hiScore.ToString();
            //MedalColor();
            gameOn.SetActive(false);
            gameOver.SetActive(true);
            gameWin.SetActive(false);
            if (GameManager.instance.currentScore >= 10 && i == 0)
            {
                int point = GameManager.instance.currentScore / 10;
               // pointText.text = "Points +" + point;
                GameManager.instance.points += point;
                GameManager.instance.Save();
                
                i = 1;
            }
            else if (GameManager.instance.currentScore < 10)
            {
                //pointText.gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.isGameOver && jSafe == 0)
        {
            NbTimePlayed = PlayerPrefs.GetInt("NbTimePlayed") + 1;
            PlayerPrefs.SetInt("NbTimePlayed", NbTimePlayed);
            jSafe = 1;
        }

    }

    private string IngameScoreText()
    {
        return GameManager.instance.currentScore.ToString() + "/" + levelConfig.ScoreToWin.ToString();
    }

    void HomeBtn()
    {
        sound.Play();
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(mainMenu);
        jSafe = 0;
    }

    void RetryBtn()
    {
        sound.Play();
        GameManager.instance.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        jSafe = 0;
    }
    
}

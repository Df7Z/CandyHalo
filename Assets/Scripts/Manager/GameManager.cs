using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// This script helps in saving and loading data in device
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private GameData data;
    
    public bool isGameOver = false;
    public int currentScore;

    public bool isGameStartedFirstTime;
    public bool isMusicOn;
    public int hiScore, points, textureStyle;

    public bool[] textureUnlocked;
  
    void Awake()
    {
        MakeInstance();
        InitializeVariables();
        
    }

    void Start()
    {
        
    }

    void MakeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void InitializeVariables()
    {
        //first we load any data is avialable
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }
        if (isGameStartedFirstTime)
        {
            //when game is started for 1st time on device we set the initial values
            isGameStartedFirstTime = false;
            hiScore = 0;
            points = 0;
            textureStyle = 0;
            textureUnlocked = new bool[4];
            textureUnlocked[0] = true;
            for (int i = 1; i < textureUnlocked.Length; i++)
            {
                textureUnlocked[i] = false;
            }
            isMusicOn = true;
         
            data = new GameData();

         
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);
            data.setHiScore(hiScore);
            data.setPoints(points);
            data.setTexture(textureStyle);
            data.setTextureUnlocked(textureUnlocked);
           
            Save();
            Load();
        }
        else
        {
           
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            hiScore = data.getHiScore();
            points = data.getPoints();
            textureStyle = data.getTexture();
            textureUnlocked = data.getTextureUnlocked();
            
        }
    }

  
    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameInfo.dat");
            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setHiScore(hiScore);
                data.setPoints(points);
                data.setTexture(textureStyle);
                data.setTextureUnlocked(textureUnlocked);
                data.setIsMusicOn(isMusicOn);
             
                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameInfo.dat", FileMode.Open);//here we get saved file
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e) { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private int hiScore, points, textureStyle;
    private bool[] textureUnlocked;


    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }

    
    //music
    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getIsMusicOn()
    {
        return isMusicOn;
    }

    //high score 
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return hiScore;
    }

    //points 
    public void setPoints(int points)
    {
        this.points = points;
    }

    public int getPoints()
    {
        return points;
    }

    //textureStyle 
    public void setTexture(int textureStyle)
    {
        this.textureStyle = textureStyle;
    }

    public int getTexture()
    {
        return textureStyle;
    }

    //texture unlocked
    public void setTextureUnlocked(bool[] textureUnlocked)
    {
        this.textureUnlocked = textureUnlocked;
    }

    public bool[] getTextureUnlocked()
    {
        return textureUnlocked;
    }
}
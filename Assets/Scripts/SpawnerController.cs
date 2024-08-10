using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour {

    public static SpawnerController instance;

    [SerializeField]
    private GameObject[] spawnPoints; 
    private AudioSource sound;         
    public AudioClip[] swingClips;     
    public float timeReduce;             
    public float timeDecreaseMileStone;  
    private float timeMileStoneCount;    

    public float minTime;               
    public float time;
    int lastI = 0;                    

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

   
    void Start ()
    {
        sound = GetComponent<AudioSource>();
        timeMileStoneCount = timeDecreaseMileStone;

       
        if (GameManager.instance.isGameOver == false)
        {
            SelectAnimal();
        }

        StartCoroutine(WaitForNextSpawn());
    }
	
	
	void Update ()
    {
        IncreaseDiff();
    }

    IEnumerator WaitForNextSpawn()
    {
        float timeVal = time;

        if (GameManager.instance.currentScore <= 10)
        {
            timeVal = time;
        }
        else if (GameManager.instance.currentScore > 10 /*&& GameManager.instance.currentScore <= 15*/)
        {
            int i = Random.Range(0, 3);

            if (i >= 0 && i < 2)
            {
                timeVal = time;
            }
            else
            {
                timeVal = 0.8f;
            }
        }

        yield return new WaitForSeconds(timeVal);
       
        if (GameManager.instance.isGameOver == false)
        {
            SelectAnimal();
        }
        else
        {
            
        }

        StartCoroutine(WaitForNextSpawn());
    }

   
    void SelectAnimal()
    {
        int i = Random.Range(0, spawnPoints.Length);
        while (i == lastI)
        {
            i = Random.Range(0, spawnPoints.Length);
        }
        

        if (i == 0)
        {
           
            GameObject newRed = ObjectPooling.instance.GetRed();
          
            newRed.transform.position = spawnPoints[i].transform.position;
         
            newRed.transform.rotation = this.transform.rotation;
           
            AnimalController code = newRed.GetComponent<AnimalController>();
            
            newRed.SetActive(true);
        
            code.applyForce = true;
           
            sound.PlayOneShot(swingClips[0]);
        }
        else if (i == 1)
        {
            GameObject newBlue = ObjectPooling.instance.GetBlue();
            newBlue.transform.position = spawnPoints[i].transform.position;
            newBlue.transform.rotation = this.transform.rotation;
            AnimalController code = newBlue.GetComponent<AnimalController>();
            newBlue.SetActive(true);
            code.applyForce = true;
            sound.PlayOneShot(swingClips[1]);
        }
        else if (i == 2)
        {
            GameObject newGreen = ObjectPooling.instance.GetGreen();
            newGreen.transform.position = spawnPoints[i].transform.position;
            newGreen.transform.rotation = this.transform.rotation;
            AnimalController code = newGreen.GetComponent<AnimalController>();
            newGreen.SetActive(true);
            code.applyForce = true;
            sound.PlayOneShot(swingClips[2]);
        }
        else if (i == 3)
        {
            GameObject newYellow = ObjectPooling.instance.GetYellow();
            newYellow.transform.position = spawnPoints[i].transform.position;
            newYellow.transform.rotation = this.transform.rotation;
            AnimalController code = newYellow.GetComponent<AnimalController>();
            newYellow.SetActive(true);
            code.applyForce = true;
            sound.PlayOneShot(swingClips[3]);
        }

        lastI = i;

    }

    void IncreaseDiff()
    {
        if (GameManager.instance.currentScore > timeMileStoneCount)
        {
            timeMileStoneCount += timeDecreaseMileStone;
            timeDecreaseMileStone += 5f;
            time -= timeReduce;

            if (time < minTime)
            {
                time = minTime;
            }

        }
    }


}

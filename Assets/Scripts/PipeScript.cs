using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PipeScript : MonoBehaviour {

    [SerializeField]
    private string animalTag;      
    private bool moving = false;   

    private AudioSource sound;  
    public AudioClip[] clips; 
   
    void Start ()
    {
       
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        if (transform.position.x > 5.25f)
        {
            transform.position = new Vector3(-5.25f, transform.position.y);
        }

        if (transform.position.x < -5.25f)
        {
            transform.position = new Vector3(5.25f, transform.position.y);
        }
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(animalTag))
        {
            
            sound.PlayOneShot(clips[0]);
            other.gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<PipeAnim>().playAnim = true;
            GameManager.instance.currentScore++;
        }
        else
        {
           
            sound.PlayOneShot(clips[1]);
            GameManager.instance.isGameOver = true;
          
            CameraShake.instance.ShakeCamera(0.05f, 1f);
        }
    }

    
    public void MoveRight()
    {
        Vector3 lastPos = transform.position;
        Vector3 newPos = new Vector3(lastPos.x + 1.5f, lastPos.y);

        StartCoroutine(MoveFromTo(lastPos, newPos, 0.2f));
    }

    public void MoveLeft()
    {
        Vector3 lastPos = transform.position;
        Vector3 newPos = new Vector3(lastPos.x - 1.5f, lastPos.y);

        StartCoroutine(MoveFromTo(lastPos, newPos, 0.2f));
    }

   
    IEnumerator MoveFromTo(Vector3 pointA, Vector3 pointB, float time)
    {
        if (!moving)
        {                     // Do nothing if already moving
            moving = true;                 // Set flag to true
            float t = 0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
                transform.position = Vector3.Lerp(pointA, pointB, t); // Set position proportional to t
                yield return 0;         // Leave the routine and return here in the next frame
            }
            moving = false;             // Finished moving
        }
    }
}

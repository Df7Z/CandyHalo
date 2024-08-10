using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour {

    public static AnimalController instance;

    [SerializeField]
    private int maxUpForce;            
    [SerializeField]
    private int minUpForce;            
    private Rigidbody2D myBody;        
    private Collider2D objectCollider;  
    [HideInInspector]
    public bool applyForce = false;     

    [SerializeField]
    private Sprite[] img; 

    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }


	void Start ()
    {
        
        myBody = GetComponent<Rigidbody2D>();
        
        objectCollider = GetComponent<Collider2D>();
        
        SpriteRenderer image = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        //image.sprite = img[GameManager.instance.textureStyle];

    }
	
	
	void FixedUpdate ()
    {
       
        if (GameManager.instance.isGameOver)
        {
            myBody.isKinematic = true;
            return;
        }

       
        if (applyForce)
        {
           
            int upForce = Random.Range(minUpForce, maxUpForce);
         
            myBody.AddForce(Vector3.up * upForce );
         
            applyForce = false;
        }
       
        if (myBody.velocity.y > 0)
        {
            objectCollider.enabled = false;
        }
        else 
        {
            objectCollider.enabled = true;
        }
       
        if (transform.position.y < -9f)
        {
            gameObject.SetActive(false);
        }
	}
}

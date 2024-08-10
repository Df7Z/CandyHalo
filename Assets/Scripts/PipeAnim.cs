using UnityEngine;
using System.Collections;

public class PipeAnim : MonoBehaviour {

    [HideInInspector]
    public bool playAnim;    

    private Animator anim; 

   
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	

	void Update ()
    {
        
        if (playAnim)
        {
            StartCoroutine(PlayAnim());
        }
            
    }

    IEnumerator PlayAnim()
    {
        anim.Play("PipeAnim");
        yield return new WaitForSeconds(0.33f);
        anim.Play("PipeIdle");
        playAnim = false;
    }


}

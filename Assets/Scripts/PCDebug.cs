using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PCDebug : MonoBehaviour
{
    public UnityEngine.UI.Button Left;
    public UnityEngine.UI.Button Right;
#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Left.onClick.Invoke();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            Right.onClick.Invoke();
        }
    }
#endif
}

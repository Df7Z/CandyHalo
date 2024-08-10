using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    public Button Button;
    public string gameScene;
    
    private void Awake()
    {
        Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(gameScene);
        });
    }
}
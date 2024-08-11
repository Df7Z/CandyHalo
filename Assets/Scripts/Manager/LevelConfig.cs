using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int ScoreToWin = 1;
    public float Difficultly = 1f;
    public int ScoreX2Speed = 10;
}
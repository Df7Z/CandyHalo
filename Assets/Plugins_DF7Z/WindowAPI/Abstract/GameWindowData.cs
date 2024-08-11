using UnityEngine;

namespace Core.Game.GamePopWindow
{
    public abstract class GameWindowData<V> : ScriptableObject where V : GameWindowView
    {
        [SerializeField] protected V _viewPrefab;

        public GameWindowView ViewPrefab => _viewPrefab;
    }
}
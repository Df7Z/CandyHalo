using PoolSystem;
using UnityEngine;

namespace Core.Game.GamePopWindow
{
    public static class WindowFabric
    {
        private static Transform _windowsParent;
        
        public static void SetSceneParent(Transform parent)
        {
            _windowsParent = parent;
        }

        public static GameWindowView Create(GameWindowView prefab)
        {
            var view = SystemPool.Spawn(prefab, _windowsParent);

            view.transform.parent = _windowsParent;

            return view;
        }
    }
}
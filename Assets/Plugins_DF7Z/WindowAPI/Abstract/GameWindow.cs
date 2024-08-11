using System;

namespace Core.Game.GamePopWindow
{
    public abstract class GameWindow<V, D> where V : GameWindowView where D : GameWindowData<V>
    {
        protected V _view;
        protected D _data;
        
        public GameWindow(GameWindowData<V> windowData)
        {
            _view = (V) WindowFabric.Create(windowData.ViewPrefab);
            
            Init();
        }
        
        protected virtual void Init() {}
        
        public abstract void Open();
        public virtual void InstantOpen() => Open();
        public abstract void Close();
        public virtual void InstantClose() => Close();
    }
}
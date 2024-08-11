using MonoSystemCache.Interfaces;
using UnityEngine;

namespace Core.Game.StateMachineCustom
{
    public interface ISateMachine
    {
        
    }
    
    public abstract class StateMachine<MS, D> : ISateMachine, IRunSystem where D : IStateData where MS : IMachineStates<D>
    {
        protected IState _current;
        protected MS _ms;
        protected D _data;
        
        public StateMachine(MS ms, D stateData)
        {
            _ms = ms;
            _data = stateData;
            
            InitStates();
        }

        protected abstract void InitStates();
        
        public IState CurrentState => _current;
        
        public void Change(IState targetState)
        {
            if (targetState == _current)
            {
#if UNITY_EDITOR
                Debug.LogWarning("States одинаковы!");
#endif
                return;
            }
                
            _current?.Leave(targetState);
            targetState.Cross(_current);
            _current = targetState;
        }
        
        public void OnRun()
        {
            _current.OnRun();
        }
    }
}
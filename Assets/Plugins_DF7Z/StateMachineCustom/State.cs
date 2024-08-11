using System;
using MonoSystemCache.Interfaces;

namespace Core.Game.StateMachineCustom
{
    public interface IStateData { //Общая для всех состояний 
    
    }
    
    public interface IMachineStates<D> where D : IStateData //Все состояния машины
    {
    }

    public interface IState : IRunSystem
    {
        public void Cross(IState fromState = null);
        public void Leave(IState toState = null);
    }
    
    public abstract class State<SM, MS, D> : IState where D : IStateData where MS : IMachineStates<D> where SM : StateMachine<MS, D>
    {
        protected D _d;
        protected MS _ms;
        protected SM _machine;
        
        protected State(SM m,MS ms, D d)
        {
            _machine = m;
            _ms = ms;
            _d = d;
        }
        
        
       // public abstract void Cross(State<SM, MS, D> fromState = null);
        //public virtual void Leave(State<SM, MS, D> toState = null) { }
        public virtual void OnRun() { }
        public virtual void Cross(IState fromState = null) { } //При переходе в стэйт
        public virtual void Leave(IState toState = null) { } //При выходе из стэйта
    }
}
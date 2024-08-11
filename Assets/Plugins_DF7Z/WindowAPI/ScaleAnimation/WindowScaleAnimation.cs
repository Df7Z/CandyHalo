using System;
using Core.Game.StateMachineCustom;
using Core.Time;
using MonoSystemCache;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Game.GamePopWindow
{
    internal class DefaultState : State<WindowScaleAnimationStateMachine, WindowScaleAnimationStates,
        WindowScaleAnimationSettings>
    {
     

        public override void Cross(IState fromState = null)
        {
            Vector3 _scale;
            
            _scale.x = _d.RaiseCurve.Evaluate(0f);
            _scale.y = _d.RaiseCurve.Evaluate(0f);
            _scale.z = _d.RaiseCurve.Evaluate(0f);

            _d.Transform.localScale = _scale;

            _ms.Timer = 0f;
            
            _d.Transform.gameObject.SetActive(false);
        }

        public DefaultState(WindowScaleAnimationStateMachine m, WindowScaleAnimationStates ms, WindowScaleAnimationSettings d) : base(m, ms, d)
        {
        }
    }
    
    internal class IdleState : State<WindowScaleAnimationStateMachine, WindowScaleAnimationStates,
        WindowScaleAnimationSettings>
    {
        public IdleState(WindowScaleAnimationStateMachine m, WindowScaleAnimationStates ms, WindowScaleAnimationSettings d) : base(m, ms, d)
        {
        }

        public override void Cross(IState fromState = null)
        {
           
        }
    }

    internal class LowerState : State<WindowScaleAnimationStateMachine, WindowScaleAnimationStates,
        WindowScaleAnimationSettings>
    {
        private Timer _timer;
        private Vector3 _scale;
        
        private void Complete()
        {
            _d.Transform.gameObject.SetActive(false);
            _machine.Change(_ms.Default);
        }

        private void OnTimerFinish() => Complete();
        
        public override void OnRun()
        {
           
            _timer.TakeTimeUnsafe(ref TimeService.DeltaTime);
            
            if (!_timer.IsActive) return;
            
            _scale.x = _d.LoverCurve.Evaluate(_timer.TimePassed);
            _scale.y = _d.LoverCurve.Evaluate(_timer.TimePassed);
            _scale.z = _d.LoverCurve.Evaluate(_timer.TimePassed);

            _d.Transform.localScale = _scale;
        }
        
        public override void Cross(IState fromState = null)
        {
            _timer.StartFrom(_d.TimeAnimation - _ms.Timer);

            GlobalUpdate.Instance.AddRunSystem(this);
        }

        public override void Leave(IState toState = null)
        {
            _ms.Timer = _timer.TimeRemain;
            
            GlobalUpdate.Instance.RemoveRunSystem(this);
        }


        public LowerState(WindowScaleAnimationStateMachine m, WindowScaleAnimationStates ms, WindowScaleAnimationSettings d) : base(m, ms, d)
        {
            _timer = new Timer(_d.TimeAnimation);
            _timer.OnTimerFinish += OnTimerFinish;
        }
    }
    
    internal class RaiseState : State<WindowScaleAnimationStateMachine ,WindowScaleAnimationStates, WindowScaleAnimationSettings>
    {
        private Timer _timer;
        private Vector3 _scale;
        
        private void Complete()
        {
            _machine.Change(_ms.Idle);
        }

        private void OnTimerFinish() => Complete();
        
        public override void OnRun()
        {
            _timer.TakeTimeUnsafe(ref TimeService.DeltaTime);

            if (!_timer.IsActive) return;
            
            _scale.x = _d.RaiseCurve.Evaluate(_timer.TimePassed);
            _scale.y = _d.RaiseCurve.Evaluate(_timer.TimePassed);
            _scale.z = _d.RaiseCurve.Evaluate(_timer.TimePassed);

         //  if (_scale == Vector3.zero) Debug.Log("Zero");
            
            _d.Transform.localScale = _scale;
        }
        
        public override void Cross(IState fromState = null)
        { 
            _d.Transform.gameObject.SetActive(true);
            
            _timer.StartFrom(_ms.Timer);
           
            GlobalUpdate.Instance.AddRunSystem(this);
        }

        public override void Leave(IState toState = null)
        {
            _ms.Timer = _timer.TimePassed;
            
            GlobalUpdate.Instance.RemoveRunSystem(this);
        }


        public RaiseState(WindowScaleAnimationStateMachine m, WindowScaleAnimationStates ms, WindowScaleAnimationSettings d) : base(m, ms, d)
        {
            _timer = new Timer(_d.TimeAnimation);
            _timer.OnTimerFinish += OnTimerFinish;
        }
    }

    [Serializable]
    public class WindowScaleAnimationSettings : IStateData
    {
        [FormerlySerializedAs("RectTransform")] public Transform Transform;
        //public float StartSize = 0.01f;
        //public float EndSize = 1f;
        [FormerlySerializedAs("TimeOpen")] public float TimeAnimation = 0.3f;
        //public float TimeClose = 0.1f;
        public AnimationCurve RaiseCurve;
        public AnimationCurve LoverCurve;
        
    }

    public class WindowScaleAnimation
    {
        private WindowScaleAnimationSettings _settings; //Настройки окна анимации
        private WindowScaleAnimationStates _statesStates;
        private WindowScaleAnimationStateMachine _stateMachine;

        public WindowScaleAnimation(WindowScaleAnimationSettings settings)
        {
            _settings = settings;
            _statesStates = new WindowScaleAnimationStates();
            _stateMachine = new WindowScaleAnimationStateMachine(_statesStates, _settings);
            _stateMachine.Change(_statesStates.Default);
        }

        public void Raise()
        {
            _stateMachine.Change(_statesStates.Raise); //открыть анимация}
        }
        
        public void Lower()
        {
            _stateMachine.Change(_statesStates.Lower); //закрыть анимация
        }

        public void Reset()
        {
            _stateMachine.Change(_statesStates.Idle); //сброс анимации
        }
    }
}
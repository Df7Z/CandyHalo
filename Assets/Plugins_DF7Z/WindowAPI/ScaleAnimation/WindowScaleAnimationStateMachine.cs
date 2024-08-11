using Core.Game.StateMachineCustom;

namespace Core.Game.GamePopWindow
{
    internal class WindowScaleAnimationStateMachine : StateMachine<WindowScaleAnimationStates, WindowScaleAnimationSettings>
    {
        protected override void InitStates()
        {
            _ms.Idle = new IdleState(this, _ms, _data);
            _ms.Raise = new RaiseState(this, _ms, _data);
            _ms.Lower = new LowerState(this, _ms, _data);
            _ms.Default = new DefaultState(this, _ms, _data);
        }
        
        public WindowScaleAnimationStateMachine(WindowScaleAnimationStates ms, WindowScaleAnimationSettings stateData) : base(ms, stateData)
        {
        }
    }
}
using Core.Game.StateMachineCustom;

namespace Core.Game.GamePopWindow
{
    internal class WindowScaleAnimationStates : IMachineStates<WindowScaleAnimationSettings>
    {
        public IdleState Idle; //окно открыто
        public DefaultState Default; //окно не открыто
        public RaiseState Raise; //Открывается
        public LowerState Lower; //Закрывается
        
        //Runtime
        public float Timer;
    }
}
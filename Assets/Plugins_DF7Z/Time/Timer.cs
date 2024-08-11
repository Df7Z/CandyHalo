using System;
using Core.Time;
using MonoSystemCache;
using MonoSystemCache.Interfaces;

namespace Core.Time
{
    public sealed class Timer : IRunSystem
    {
        public Action OnTimerFinish;
        private float _timer;
        private float _timeRemain;
        private float _timePassed;

        public float TimeRemain => _timeRemain;
        public float TimePassed => _timePassed;

        private bool _isActive;

        public bool IsActive => _isActive;

        public Timer(float time) {
            _timer = time;
            _timeRemain = _timer;
            _isActive = false;  
        }

        public void StartFrom(float timeStart) //Старт таймера от этого значения
        {
            if (timeStart >= _timer)
            {
                Finish();
                return;
            }

            _isActive = true;
            _timeRemain = _timer - timeStart;
            _timePassed = timeStart;
        }
        
        public void TakeTime(ref float time) {
            if (!_isActive) return;
            
            _timeRemain -= time;
            _timePassed += time;

            //if (_timePassed > _timer) _timePassed = _timer;

            if (_timeRemain <= 0) {
                Finish();
            }
        }
        
        public void TakeTimeUnsafe(ref float time) {
            _timeRemain -= time;
            _timePassed += time;
            
            if (_timeRemain <= 0) {
                Finish();
            }
        }
        
        public void TakeTimeUnsafe(float time) {
            _timeRemain -= time;
            _timePassed += time;
            
            if (_timeRemain <= 0) {
                Finish();
            }
        }

        public void Start() {
            Reset();
            _isActive = true;
        }
        
        
        public void Reset() {
            _isActive = false;
            _timePassed = 0f;
            _timeRemain = _timer;
        }
        
        private void Finish()
        {
            if (_timePassed > _timer) _timePassed = _timer;
            
            OnTimerFinish?.Invoke();
            Reset();
        }

        public void SetNewTime(float time) {
            _timer = time;
        }

        public void RegisterGlobalUpdate() {
            GlobalUpdate.Instance.AddRunSystem(this);
        }
        
        public void UnregisterGlobalUpdate() {
            GlobalUpdate.Instance.AddRunSystem(this);
        }
        
        
        public void OnRun() {
            TakeTime(ref TimeService.DeltaTime);
        }
    }
}
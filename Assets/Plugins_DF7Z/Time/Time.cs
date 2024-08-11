using System;
using MonoSystemCache;
using MonoSystemCache.Interfaces;

namespace Core.Time
{
    public sealed class TimeService : IRunSystem, IFixedRunSystem
    {
        public static float DeltaTime;
        public static float FixedTime;
        
        public static Action<bool> OnTimeChangeFreeze; //Время остановилось
        private static bool _freezeTime;
        public static bool FreezeTime => _freezeTime;

        public static void Init() {
            _freezeTime = false;
        }
        
        public static void ChangeFreezeTime(bool state) {
            _freezeTime = state;
            DeltaTime = 0f;
            FixedTime = 0f;
            OnTimeChangeFreeze?.Invoke(_freezeTime);
        }

        public void OnRun() {
            if (_freezeTime) return;
            
            DeltaTime = UnityEngine.Time.deltaTime;
        }

        public void OnFixedRun() {
            if (_freezeTime) return;
            
            FixedTime = UnityEngine.Time.deltaTime;
        }
        
    }
}
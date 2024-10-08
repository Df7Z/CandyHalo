
using System;
using MonoSystemCache.Interfaces;
using MonoSystemCache;
using UnityEngine.Device;

namespace MonoSystemCache
{
    public abstract class MonoCache : MonoShortСuts, IRunSystem, IFixedRunSystem, ILateRunSystem
    {
        private GlobalUpdate _globalUpdate;
        private bool _isSetup;
        
        private void OnEnable()
        {
            OnEnabled();

            if (_isSetup == false)
            {
                TrySetup();
            }

            if (_isSetup)
            {
                SubscribeToGlobalUpdate();
            }
        }

        private void OnDisable()
        {
            if (_isSetup)
            {
                UnsubscribeFromGlobalUpdate();
            }

            OnDisabled();
        }

        private void TrySetup()
        {
            if (Application.isPlaying)
            {
                _globalUpdate = Singleton<GlobalUpdate>.Instance;
                _isSetup = true;
            }
            else
            {
                throw new Exception(
                    $"You tries to get {nameof(GlobalUpdate)} instance when application is not playing!");
            }
        }
        
        private void SubscribeToGlobalUpdate()
        {
            _globalUpdate.AddRunSystem(this);
            _globalUpdate.AddFixedRunSystem(this);
            _globalUpdate.AddLateRunSystem(this);
        }

        private void UnsubscribeFromGlobalUpdate()
        {
            _globalUpdate.RemoveRunSystem(this);
            _globalUpdate.RemoveFixedRunSystem(this);
            _globalUpdate.RemoveLateRunSystem(this);
        }

        void IRunSystem.OnRun() => Run();
        void IFixedRunSystem.OnFixedRun() => FixedRun();
        void ILateRunSystem.OnLateRun() => LateRun();

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        
        protected virtual void Run() { }
        protected virtual void FixedRun() { }
        protected virtual void LateRun() { }
    }
}

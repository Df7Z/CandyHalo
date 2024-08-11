using UnityEngine;

namespace Core.Time
{
    public sealed class TimeEntry : MonoBehaviour
    {
        private void Update()
        {
            TimeService.DeltaTime = UnityEngine.Time.deltaTime;
        }

        private void FixedUpdate()
        {
            TimeService.FixedTime = UnityEngine.Time.fixedDeltaTime;
        }
    }
}
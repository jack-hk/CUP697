using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Handles compatibility functions for the build runtime platform.
    /// </summary>
    public class RuntimePlatformCompatibility : MonoBehaviourSingletonPersistent<RuntimePlatformCompatibility>
    {

#if UNITY_EDITOR
        public bool IsInUnityEditor { get; private set; } = true;
#else
        public bool IsInUnityEditor { get; private set; } = false;
#endif

        public bool IsVirtualGamepadAllowed { get; private set; } = true;

        private void MobileMode()
        {
            IsVirtualGamepadAllowed = false;
        }

        public override void Awake()
        {
            base.Awake();

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    MobileMode();
                    break;

                case RuntimePlatform.IPhonePlayer:
                    MobileMode();
                    break;
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            MobileMode(); // Back-up approach to detecting OS
#endif
        }
    }
}

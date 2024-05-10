using UnityEngine;

namespace RoboStruct.Debugging
{
    /// <summary>
    /// WARNING: Debugging scripts are not made to be efficient or optimised. They are used for Unity Editor debugging purposes and are not included in the release build.
    /// </summary>
    public class Debug : MonoBehaviour
    {
        public bool IsDebugModeEnabled 
        {
            get 
            {
                if (!RuntimePlatformCompatibility.Instance.IsInUnityEditor) return false;
                else return _isDebugModeEnabled;
            }
            set
            {
                _isDebugModeEnabled = value;
            }
        }

        private bool _isDebugModeEnabled;

        public void StartShop()
        {
            if (UnityEngine.Debug.isDebugBuild) SceneLoader.Instance.Load(SceneLoader.Scene.ShopMenu);
        }

        public void StartMainMenu()
        {
            if (UnityEngine.Debug.isDebugBuild) SceneLoader.Instance.Load(SceneLoader.Scene.MainMenu);
        }
    }
}



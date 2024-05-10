
namespace RoboStruct.Scene
{
    /// <summary>
    /// Singleton that has common functions shared across scene managers.
    /// "Scene managers" are scripts that handle with specific functions that only appear in that single scene.
    /// </summary>
    public class SceneCommon : MonoBehaviourSingleton<SceneCommon>
    {
        public void LoadSurvival()
        {
            GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.GameplayStateCallback);
            SceneLoader.Instance.Load(SceneLoader.Scene.Survival);
        }

        public void LoadMainMenu()
        {
            GameManager.Instance.GameStateFSM.TransitionTo(GameManager.Instance.MenuStateCallback);
            SceneLoader.Instance.Load(SceneLoader.Scene.MainMenu);
        }
    }
}



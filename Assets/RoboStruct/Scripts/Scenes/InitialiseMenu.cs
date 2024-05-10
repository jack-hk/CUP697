
namespace RoboStruct.Scene
{
    /// <summary>
    /// Singletone scene manager that deals with the InitaliseMenu scene functions.
    /// "Scene managers" are scripts that handle with specific functions that only appear in that single scene.
    /// 
    /// One of the InitialiseMenu's purposes is to create (usually) persistent GameObjects that exist throughout the entire life of the gameloop.
    /// </summary>
    public class InitialiseMenu : MonoBehaviourSingleton<GameManager>
    {
        private void Start()
        {
            SceneLoader.Instance.Load(SceneLoader.Scene.MainMenu);
        }
    }
}

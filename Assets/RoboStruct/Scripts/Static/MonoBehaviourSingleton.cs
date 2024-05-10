using UnityEngine;

namespace RoboStruct
{
    /// <summary>
    /// Class which only allows one instance of itself to be created.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Debug.LogWarning("Multiple instances of singleton class " + typeof(T).Name + " detected. Destroying this instance.");
                Destroy(this);
            }
        }
    }

    /// <summary>
    /// DontDestroyOnLoad, Persistent class which only allows one instance of itself to be created.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
        where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("Multiple instances of singleton class " + typeof(T).Name + " detected. Destroying this instance.");
                Destroy(gameObject);
            }
        }
    }
}

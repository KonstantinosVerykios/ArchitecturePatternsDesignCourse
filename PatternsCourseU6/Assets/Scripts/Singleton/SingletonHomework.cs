using UnityEngine;

// ALL OF THE COMMENTS WERE WRITTEN WITH GEMINI PRO ARGON MODEL ON THE HIGH SETTING
/// <summary>
/// A generic, thread-safe, persistent Singleton base class for MonoBehaviours.
/// Inherit from this class using the Curiously Recurring Template Pattern (CRTP):
/// e.g. public class AudioManager : SingletonHomework<AudioManager>
/// </summary>
public class SingletonHomework<T> : MonoBehaviour where T : MonoBehaviour
{
    // The single cached reference to the instance of type T.
    private static T _instance;

    // A dummy object used to synchronize threads and prevent race conditions.
    private static object _lock = new object();

    /// <summary>
    /// Global access point to retrieve the single instance of type T.
    /// </summary>
    public static T Instance
    {
        get
        {
            // 1. QUIT PROTECTION:
            // When Unity stops playing, GameObjects are destroyed in random order.
            // If another script's OnDestroy/OnDisable calls Instance after this is destroyed,
            // we return null to prevent creating a "ghost" GameObject in the closing scene.
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            // 2. THREAD SAFETY:
            // Ensures only one thread at a time can execute the initialization logic.
            lock (_lock)
            {
                // Lazy initialization: only find or create the instance when first requested.
                if (_instance == null)
                {
                    // Look through the currently loaded scene for an existing component of type T.
                    _instance = (T)FindFirstObjectByType(typeof(T));

                    // Sanity check: verify that there aren't multiple copies placed in the scene.
                    if (FindObjectsByType(typeof(T), FindObjectsSortMode.None).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    // 3. AUTO-CREATION:
                    // If no instance exists in the scene, create one programmatically.
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        // Preserve this GameObject across scene loads.
                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    // Flag tracking whether the application/play-mode session is shutting down.
    private static bool applicationIsQuitting = false;

    /// <summary>
    /// When this object is destroyed, flag that shutdown has occurred.
    /// Marked 'protected virtual' so subclasses can override it if needed (must call base.OnDestroy()).
    /// </summary>
    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
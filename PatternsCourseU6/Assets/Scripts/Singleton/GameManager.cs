using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        MAIN_MENU,
        PLAYING,
        PAUSED,
        GAME_OVER
    }

    [SerializeField]
    private static GameState _state;
    
    private int Highscore = 0;

    public static GameManager instance { get; private set; }

    public static void StartGame()
    {
        // Set the game state
        _state = GameState.PLAYING;

        Debug.Log("User entered!");
        // Open the singleton game scene
        SceneManager.LoadScene("GameScene");
    }

    public static void ExitGame()
    {
        Debug.Log("User quit!");
        // Exit the game scene
        Application.Quit();
    }

    private void Awake()
    {
        // Allows for only one instance of a game maager to exist at a time
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _state = GameState.MAIN_MENU;   
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case GameState.PLAYING:
                if(Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    _state = GameState.PAUSED;
                    Time.timeScale = 0f;
                }
                break;
            case GameState.PAUSED:
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    _state = GameState.PLAYING;
                    Time.timeScale = 1f;
                }
                break;

        }
    }

    public bool IsPlaying()
    {
        return _state == GameState.PLAYING;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MAIN_MENU,
        PLAYING,
        PAUSED,
        GAME_OVER
    }

    private static GameState _state;
    private int Highscore = 0;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Automatically set state based on which scene you hit Play in
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                _state = GameState.PLAYING;
            }
            else
            {
                _state = GameState.MAIN_MENU;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StartGame()
    {
        _state = GameState.PLAYING;
        Time.timeScale = 1f;
        Debug.Log("User entered!");
        SceneManager.LoadScene("GameScene");
    }

    public static void ExitGame()
    {
        Debug.Log("User quit!");
        Application.Quit();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        switch (_state)
        {
            case GameState.PLAYING:
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
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

                if (Keyboard.current.qKey.wasPressedThisFrame)
                {
                    _state = GameState.MAIN_MENU;
                    SceneManager.LoadScene("MenuScene");
                    ExitGame();
                }
                break;
        }
    }

    public bool IsPlaying()
    {
        return _state == GameState.PLAYING;
    }
}
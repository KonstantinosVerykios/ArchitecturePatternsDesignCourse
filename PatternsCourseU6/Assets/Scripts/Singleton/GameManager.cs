using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int Highscore = 0;

    public static GameManager instance { get; private set; }

    public static void StartGame()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

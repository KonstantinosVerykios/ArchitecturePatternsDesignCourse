using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioCLips")]
    public AudioClip CactusClip;
    public AudioClip BGMusic;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource BGSource;
    [SerializeField] private AudioSource SFXSource;
    private float _pitch = 0f;


    public static AudioManager Instance { get; private set; }

    public void PlayCactusSound()
    {
        SFXSource.clip = CactusClip;
        SFXSource.pitch = _pitch * Random.Range(0.98f, 1.02f);
        SFXSource.Play();
    }

    public void PlayBackgroundMusic()
    {
        BGSource.clip = BGMusic;
        BGSource.Play();
    }

    private void Awake()
    {
        // Allows for only one instance of a game maager to exist at a time
        if (Instance == null)
        {
            Instance = this;
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
        _pitch = SFXSource.pitch;
        PlayBackgroundMusic();
    }

}

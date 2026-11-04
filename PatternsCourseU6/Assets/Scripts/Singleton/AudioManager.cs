using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioCLips")]
    public AudioClip CactusClip;
    public AudioClip BGMusic;

    [Header("AudioSource")]
    private AudioSource MainSource;
    private AudioSource SFXSource;
    private float _pitch = 0f;


    public static AudioManager Instance { get; private set; }

    public void PlayCactusSound()
    {
        SFXSource.Stop();
        SFXSource.clip = CactusClip;
        SFXSource.pitch = _pitch * Random.Range(0.98f, 1.02f);
        SFXSource.Play();
    }

    public void StartMusic()
    {
        SFXSource.Stop();
        SFXSource.clip = CactusClip;
        SFXSource.pitch = _pitch * Random.Range(0.98f, 1.02f);
        SFXSource.Play();
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
        MainSource = GetComponent<AudioSource>();
        SFXSource = GetComponent<AudioSource>();

        _pitch = SFXSource.pitch;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

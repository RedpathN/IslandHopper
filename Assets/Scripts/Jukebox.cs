using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jukebox : MonoBehaviour
{
    public static Jukebox Instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] soundClips = null;
    [SerializeField]
    private string[] soundNames = null;
    private Dictionary<string, AudioClip> soundNameToClip;

    [SerializeField]
    private string[] islandSceneNames = null;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            InitializeJukebox();
        }
    }

    private void InitializeJukebox()
    {
        audioSource.loop = true;
        soundNameToClip = new Dictionary<string, AudioClip>();
        for (int i = 0; i < soundNames.Length; ++i)
            soundNameToClip[soundNames[i]] = soundClips[i];
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If we want music to keep playing between scenes, we should rework this
        audioSource.Stop();

        if (scene.name == "MainMenu")
        {
            audioSource.clip = soundNameToClip["Title Theme"];
            audioSource.Play();
        }
        else if (islandSceneNames.Contains(scene.name))
        {
            audioSource.clip = soundNameToClip["Island Theme"];
            audioSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (name == "Victory" || name == "Defeat")
            audioSource.Stop();

        // Arbitrary volume scaling here because the music was a lot
        // louder than the sound effects. Also fixes footstep loudness.
        // Music is played in OnSceneLoaded, not here.
        float volumeScale = 1;
        if (!name.StartsWith("Footstep"))
            volumeScale = 2;

        audioSource.PlayOneShot(soundNameToClip[name], volumeScale);
    }

    // If multiple sound names are given, play one at random.
    // Useful for adding variation.
    public void PlaySFX(string[] names)
    {
        PlaySFX(names[UnityEngine.Random.Range(0, names.Length)]);
    }

    // Just for testing!
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            audioSource.Stop();
        }
    }
}

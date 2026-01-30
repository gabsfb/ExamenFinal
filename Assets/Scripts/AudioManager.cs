using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusic;

    [Header("Audio Clips")]
    public AudioClip background;

    private void Start()
    {
        backgroundMusic.clip = background;
        backgroundMusic.Play();
    }

}

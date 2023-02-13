using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public bool IsBattle = false;

    [SerializeField] private AudioSource _normalAudioSource;
    [SerializeField] private AudioSource _battleAudioSource;
    [SerializeField] private AudioSource _audioSource;

    private bool _isSoundOn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        ChangeMusic();

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) IsBattle = false;
    }

    public void ChangeMusic()
    {
        if (IsBattle)
        {
            _normalAudioSource.volume = 0f;
            _battleAudioSource.volume = 1f;
        }
        else
        {
            _normalAudioSource.volume = 1f;
            _battleAudioSource.volume = 0f;
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        _audioSource.PlayOneShot(_sound);
    }  
}
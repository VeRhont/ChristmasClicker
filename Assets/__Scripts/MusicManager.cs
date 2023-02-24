using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private bool _isBattle = false;

    [SerializeField] private AudioSource _normalAudioSource;
    [SerializeField] private AudioSource _battleAudioSource;
    [SerializeField] private AudioSource _audioSource;

    private bool _isSoundOn = false;
    private float _currentVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _currentVolume = PlayerPrefs.GetFloat("volume", 0.5f);

        _audioSource.volume = _currentVolume;
    }

    private void Update()
    {
        ChangeMusic();

        _isBattle = EnemyWaves.Instance.IsBattle;
    }

    public void ChangeMusic()
    {
        if (_isBattle)
        {
            _normalAudioSource.volume = 0f;
            _battleAudioSource.volume = _currentVolume;
        }
        else
        {
            _normalAudioSource.volume = _currentVolume;
            _battleAudioSource.volume = 0f;
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        _audioSource.PlayOneShot(_sound);
    } 
}
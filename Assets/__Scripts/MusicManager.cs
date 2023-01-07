using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public bool IsBattle = false;

    [SerializeField] private AudioSource _normalAudioSource;
    [SerializeField] private AudioSource _battleAudioSource;
    [SerializeField] private AudioSource _audioSource;

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
            _normalAudioSource.enabled = false;
            _battleAudioSource.enabled = true;
        }
        else
        {
            _normalAudioSource.enabled = true;
            _battleAudioSource.enabled = false;
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        _audioSource.PlayOneShot(_sound);
    }
}
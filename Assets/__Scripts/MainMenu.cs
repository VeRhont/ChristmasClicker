using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;

    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _mainMenu;

    [Header("Volume")]
    [SerializeField] private float _currentVolume;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _volumeToggle;
    [SerializeField] private AudioSource _audioSource;

    public void Play()
    {
        PlayClickSound();

        SceneManager.LoadScene("MainRoom");
    }

    public void ShowMainMenu()
    {
        _mainMenu.SetActive(true);
    }

    public void HideMainMenu()
    {
        _mainMenu.SetActive(false);
    }

    public void ShowSettings()
    {
        PlayClickSound();

        _settings.SetActive(true);
    }

    public void HideSettings()
    {
        PlayClickSound();

        _settings.SetActive(false);
    }

    public void ShowTutorial()
    {
        PlayClickSound();

        _tutorial.SetActive(true);
    }

    public void HideTutorial()
    {
        PlayClickSound();

        _tutorial.SetActive(false);
    }

    public void Exit()
    {
        PlayClickSound();

        Application.Quit();
    }

    private void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    public void ChangeVolume()
    {
        _currentVolume = _volumeSlider.value;
    }

    public void LoadVolumeValue()
    {
        _currentVolume = PlayerPrefs.GetFloat("volume", 0.5f);
    }

    public void SaveVolumeValue()
    {
        PlayerPrefs.SetFloat("volume", _currentVolume);
    }
}

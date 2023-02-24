using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;

    public void PauseGame()
    {
        MusicManager.Instance.PlaySound(_clickSound);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        MusicManager.Instance.PlaySound(_clickSound);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        MusicManager.Instance.PlaySound(_clickSound);

        PlayerPrefs.DeleteAll();
        GameManager.Instance.ResetValues();

        Time.timeScale = 1f;

        SceneManager.LoadScene("MainRoom");
    }
    
    public void ReturnToMenu()
    {
        MusicManager.Instance.PlaySound(_clickSound);

        Time.timeScale = 1f;

        GameManager.Instance.SaveValues();
        EnemyWaves.Instance.SaveValues();

        SceneManager.LoadScene("Menu");
    }
}
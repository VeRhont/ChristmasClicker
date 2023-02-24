using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score { get { return _score; } }
    public int CoinsPerSecond { get { return _coinsPerSecond; } }

    private int _score = 0;
    private int _coinsForClick = 1;
    private int _coinsPerSecond = 0;

    [SerializeField] private float _scoreToWin = 500000f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _roomScoreText;
    [SerializeField] private TextMeshProUGUI _outsideScoreText;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;

    [SerializeField] private AudioClip _clickSound;

    private float _timer = 1f;

    private void Awake()
    {
        Instance = this;

        LoadValues();
        UpdateScore();
    }

    private void Update()
    {
        if (_score >= _scoreToWin)
        {
            WinGame();
        }

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _score += _coinsPerSecond;
            UpdateScore();

            _timer = 1f;
        }
    }

    public void Click()
    {
        MusicManager.Instance.PlaySound(_clickSound);
        
        _score += _coinsForClick;
        UpdateScore();
    }

    public void UpdateScore()
    {
        _roomScoreText.SetText($"Score: {_score}");
        _outsideScoreText.SetText($"Score: {_score}");
    }

    public void DecreaseScore(int value)
    {
        _score -= value;
    }

    public void IncreaseCoinsPerSecond(int value)
    {
        _coinsPerSecond += value;
    }

    private void WinGame()
    {
        _scoreToWin *= 2;

        Time.timeScale = 0f;

        _winUI.SetActive(true);
    }

    private void LoseGame()
    {
        Time.timeScale = 0f;

        PlayerPrefs.DeleteAll();
        ResetValues();

        _loseUI.SetActive(true);
    }

    public void ResetValues()
    {
        _score = 0;
        _coinsForClick = 1;
        _coinsPerSecond = 0;
        _timer = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LoseGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveValues();
    }

    public void SaveValues()
    {
        PlayerPrefs.SetInt("score", _score);
        PlayerPrefs.SetInt("coinsPerSecond", _coinsPerSecond);
        PlayerPrefs.SetFloat("scoreToWin", _scoreToWin);
    }

    private void LoadValues()
    {
        _score = PlayerPrefs.GetInt("score", 0);
        _coinsPerSecond = PlayerPrefs.GetInt("coinsPerSecond", 0);
        _scoreToWin = PlayerPrefs.GetFloat("scoreToWin", 500000);
    }
}
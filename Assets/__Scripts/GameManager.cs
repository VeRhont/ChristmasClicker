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

    [SerializeField] private float _scoreToWin = 1000000f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _roomScoreText;
    [SerializeField] private TextMeshProUGUI _outsideScoreText;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;

    private float _timer = 1f;

    private void Awake()
    {
        Instance = this;
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
        Time.timeScale = 0f;

        _winUI.SetActive(true);
    }

    private void LoseGame()
    {
        Time.timeScale = 0f;

        _loseUI.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LoseGame();
        }
    }
}
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

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _timer = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
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
        _scoreText.SetText($"Score: {_score}");
    }

    public void DecreaseScore(int value)
    {
        _score -= value;
    }

    public void IncreaseCoinsPerSecond(int value)
    {
        _coinsPerSecond += value;
    }
}
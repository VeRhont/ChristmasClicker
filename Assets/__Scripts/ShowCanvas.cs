using UnityEngine;
using UnityEngine.UI;

public class ShowCanvas : MonoBehaviour
{
    public static ShowCanvas Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Toggle _showShop;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector2 _normalPosition;
    [SerializeField] private Vector2 _hiddenPosition;

    [SerializeField] private Sprite _arrowUp;
    [SerializeField] private Sprite _arrowDown;
    [SerializeField] private Image _togglerImage;

    [SerializeField] private AudioClip _showUpSound;

    public void ShowShop()
    {
        MusicManager.Instance.PlaySound(_showUpSound);

        if (_showShop.isOn)
        {
            _togglerImage.sprite = _arrowDown;
            _rectTransform.transform.localPosition = _normalPosition;
        }
        else
        {
            _togglerImage.sprite = _arrowUp;
            _rectTransform.transform.localPosition = _hiddenPosition;
        }
    }

    public void HideShop()
    {
        _showShop.isOn = false;
        ShowShop();
    }
}

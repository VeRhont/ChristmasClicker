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

    public void ShowShop()
    {
        if (_showShop.isOn)
        {
            _rectTransform.transform.localPosition = _normalPosition;
        }
        else
        {
            _rectTransform.transform.localPosition = _hiddenPosition;
        }
    }

    public void HideShop()
    {
        _showShop.isOn = false;
        ShowShop();
    }
}

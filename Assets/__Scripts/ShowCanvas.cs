using UnityEngine;
using UnityEngine.UI;

public class ShowCanvas : MonoBehaviour
{
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
}

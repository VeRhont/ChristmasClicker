using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Slot : MonoBehaviour
{
    [SerializeField] private ItemInfo _item;

    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _slotPrice;
    [SerializeField] private TextMeshProUGUI _slotEfficiency;
    [SerializeField] private GameObject _roomChanges;

    private Sprite _sprite;
    private int _initialPrice;
    private int _price;

    private int _coinsPerSecond;
    private int _count = 0;

    private float _time = 60f;

    private void Awake()
    {
        _sprite = _item.Sprite;
        _initialPrice = _item.price;
        _coinsPerSecond = _item.efficiency;

        _price = _initialPrice;

        SetValues();
    }

    public void BuyItem()
    {
        if (GameManager.Instance.Score >= _price)
        {
            GameManager.Instance.DecreaseScore(_price);
            GameManager.Instance.UpdateScore();
            GameManager.Instance.IncreaseCoinsPerSecond(_coinsPerSecond);

            _count += 1;
            _price = (int)(_price * 1.5f);

            SetValues();
            UpdateRoom();
        }
    }

    private void UpdateRoom()
    {
        if (_count == 0)
        {
            _roomChanges.SetActive(false);
        }
        else
        {
            _roomChanges.SetActive(true);
        }
    }

    private void SetValues()
    {
        _slotImage.sprite = _sprite;
        _slotPrice.SetText(_price.ToString());
        _slotEfficiency.SetText(_coinsPerSecond.ToString());
    }
}
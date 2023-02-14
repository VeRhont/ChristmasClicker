using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private ItemInfo _item;

    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _slotPrice;
    [SerializeField] private TextMeshProUGUI _slotEfficiency;
    [SerializeField] private GameObject _roomChanges;

    [SerializeField] private AudioClip _buySound;

    private Sprite _sprite;
    private Sprite _defaultSprite;
    private int _initialPrice;
    private int _price;
    private string _name;

    private int _coinsPerSecond;
    private int _count = 0;

    private float _time = 60f;

    private void Awake()
    {
        _sprite = _item.Sprite;
        _defaultSprite = _item.DefaultSprite;
        _initialPrice = _item.Price;
        _coinsPerSecond = _item.Efficiency;
        _name = _item.Name;

        _price = PlayerPrefs.GetInt($"{_name}Price", _initialPrice);
        _count = PlayerPrefs.GetInt($"{_name}Count", 0);

        SetValues();
        UpdateRoom();
    }

    public void BuyItem()
    {
        if (GameManager.Instance.Score >= _price)
        {
            GameManager.Instance.DecreaseScore(_price);
            GameManager.Instance.UpdateScore();
            GameManager.Instance.IncreaseCoinsPerSecond(_coinsPerSecond);

            MusicManager.Instance.PlaySound(_buySound);

            _count += 1;
            _price = (int)(_price * 1.5f);

            SetValues();
            UpdateRoom();
            SaveValues();
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
        if (_count == 0)
        {
            _slotImage.sprite = _defaultSprite;
        }
        else
        {
            _slotImage.sprite = _sprite;
        }

        if (_price >= 1000f)
        {
            if (_price % 10 != 0) _price = _price / 10 * 10;
            var priceText = (_price / 1000f).ToString();

            _slotPrice.SetText(priceText + "K");
        }
        else
        {
            _slotPrice.SetText(_price.ToString());
        }

        if (_coinsPerSecond >= 1000f)
        {
            var efficiencyText = (_coinsPerSecond / 1000f).ToString();
            _slotEfficiency.SetText(efficiencyText + "K");
        }
        else
        {
            _slotEfficiency.SetText(_coinsPerSecond.ToString());
        }
    }

    private void SaveValues()
    {
        PlayerPrefs.SetInt($"{_name}Price", _price);
        PlayerPrefs.SetInt($"{_name}Count", _count);
    }
}
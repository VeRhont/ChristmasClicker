using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _initialPrice;

    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private GameObject _flyingObject;
    [SerializeField] private Sprite _objectSprite;

    private Vector2 _mousePosition;

    private int _price = 0;
    private bool _isBuying = false;

    private void Awake()
    {
        _price = _initialPrice;
        _priceText.SetText(_price.ToString());
    }

    public void BuyObject()
    {
        if (GameManager.Instance.Score < _price) return;

        _isBuying = true;
        _flyingObject.GetComponent<SpriteRenderer>().sprite = _objectSprite;
        _flyingObject.SetActive(true);
    }

    private void Update()
    {
        if (_isBuying == false) return;

        _flyingObject.transform.position = GetMousePosition();

        if (Input.GetMouseButtonDown(1))
        {
            HideFlyingObject();
        }
        if (Input.GetMouseButtonDown(0))
        {
            HideFlyingObject();
            var spawnPosition = AdjustPosition(GetMousePosition());

            Instantiate(_objectPrefab, spawnPosition, _objectPrefab.transform.rotation);

            UpdatePrice();
        }
    }

    private Vector3 GetMousePosition()
    {
        _mousePosition = Input.mousePosition;
        var position = Camera.main.ScreenToWorldPoint(_mousePosition);
        position.z = 0;

        return position;
    }

    private Vector3 AdjustPosition(Vector3 position)
    {
        if (_objectPrefab.CompareTag("Barricade"))
        {
            position.y = 0.2f;
        }
        else if (_objectPrefab.CompareTag("Cannon"))
        {
            if (position.y > -0.2f) position.y = -0.2f;
            else if (position.y < -4.23f) position.y = -4.23f;
        }

        if (position.x < 25f) position.x = 25f;
        else if (position.x > 33f) position.x = 33f;

        return position;
    }

    private void UpdatePrice()
    {
        GameManager.Instance.DecreaseScore(_price);

        _price = (int)(_price * 1.3f);
        _priceText.SetText(_price.ToString());
    }

    private void HideFlyingObject()
    {
        _flyingObject.GetComponent<SpriteRenderer>().sprite = null;
        _flyingObject.SetActive(false);
        _isBuying = false;
    }
}
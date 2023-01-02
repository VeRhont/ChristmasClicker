using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private int _initialPrice;

    private int _price;

    private void Awake()
    {
        _price = _initialPrice;
    }

    public void BuyObject()
    {
        print("Ok");
        _price = (int)(_price * 1.5f);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Clicker/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private Sprite _defaultSprite;
    public Sprite DefaultSprite => _defaultSprite;

    [SerializeField] private int _price;
    public int Price => _price;

    [SerializeField] private int _efficiency;
    public int Efficiency => _efficiency;

    [SerializeField] private string _name;
    public string Name => _name;
}

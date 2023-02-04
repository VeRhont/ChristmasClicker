using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _healthBarFill;   

    [SerializeField] private Vector3 _offset;

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _health;

        _healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + _offset);

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateHealthBar()
    {
        _healthBar.value = _health / _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        var isOutside = ChangeRoom.IsOutside;

        _healthBar.gameObject.SetActive(isOutside);
    }
}

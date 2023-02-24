using UnityEngine;
using System.Collections;

public class Barricade : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Color _damagedColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _health;

        _spriteRenderer.color = _defaultColor;
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(ChangeColor());

        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator ChangeColor()
    {
        _spriteRenderer.color = _damagedColor;

        yield return new WaitForSeconds(0.5f);

        _spriteRenderer.color = _defaultColor;
    }
}

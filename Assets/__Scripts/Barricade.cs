using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField] private float _health;

    public void TakeDamage(int damage)
    {
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
}

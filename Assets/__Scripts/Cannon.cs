using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _lifeTime;

    [SerializeField] private float _shootingRate;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootingPoint;

    private bool _shooting = false;
    private float _lastShootingTime = 0;

    private void Update()
    {
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        if (_shooting == true && _lastShootingTime <= 0)
        {
            Shoot();
            _lastShootingTime = _shootingRate;
        }

        _lastShootingTime -= Time.deltaTime;
        _lifeTime -= Time.deltaTime;
    }

    private void Shoot()
    {
        var snowball = Instantiate(_projectile, _shootingPoint.position, Quaternion.identity);
        snowball.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _projectileSpeed, ForceMode2D.Impulse);
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _shooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _shooting = false;
        }
    }
}

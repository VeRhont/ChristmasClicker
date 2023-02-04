using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _lifeTime;
    [SerializeField] private Image _timerImage;
    [SerializeField] private Image _timerBackgroundImage;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _shootingRate;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootingPoint;

    private bool _shooting = false;
    private float _lastShootingTime = 0;
    private float _maxLifeTime;

    private void Start()
    {
        _maxLifeTime = _lifeTime;
        _timerImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + _offset);
        _timerBackgroundImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + _offset);
    }

    private void Update()
    {
        UpdateUI();

        _timerImage.fillAmount = _lifeTime / _maxLifeTime;

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

    private void UpdateUI()
    {
        var isOutside = ChangeRoom.IsOutside;
 
        _timerImage.enabled = isOutside;
        _timerBackgroundImage.enabled = isOutside;
    }
}

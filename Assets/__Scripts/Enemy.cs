using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _speed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private int _damageOnClick;
    [SerializeField] private int _damageFromSnowball;
    private int _health;

    private Rigidbody2D _enemyRb;
    private Animator _enemyAnimator;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponentInChildren<Animator>();

        _health = _maxHealth;
    }

    private void Update()
    {
        _enemyRb.MovePosition(transform.position + Vector3.left * _speed * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TakeDamage(_damageOnClick);
    }

    private void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        if (_health == 0)
        {
            Die();
        }
    } 

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            TakeDamage(_damageFromSnowball);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Barricade"))
        {
            _enemyAnimator.SetTrigger("Attack");
            collision.gameObject.GetComponent<Barricade>().TakeDamage(_enemyDamage);
        }
        if (collision.gameObject.CompareTag("Cannon"))
        {
            _enemyAnimator.SetTrigger("Attack");
            collision.gameObject.GetComponent<Cannon>().TakeDamage(_enemyDamage);
        }
    }
}
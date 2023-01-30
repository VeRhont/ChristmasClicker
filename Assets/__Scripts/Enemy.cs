using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [Header("EnemyStats")]
    [SerializeField] private float _speed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _enemyDamage;
    [SerializeField] private int _damageOnClick;
    [SerializeField] private int _damageFromSnowball;
    private int _health = 0;

    [Header("EnemyAttack")]
    [SerializeField] private float _timeBetweenAttack = 1f;
    private float _timeFromLastAttack = 0;
    private bool _isAttacking = false;
    private GameObject _collisionObject;

    private Rigidbody2D _enemyRb;
    private Animator _enemyAnimator;

    [SerializeField] private float _deathAnimationTime = 0.5f;
    [SerializeField] private ParticleSystem _deathParticles;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponentInChildren<Animator>();

        _health = _maxHealth;
    }

    private void Update()
    {
        _enemyRb.MovePosition(transform.position + Vector3.left * _speed * Time.deltaTime);

        if (_isAttacking && (_timeFromLastAttack <= 0))
        {
            Attack();
            _timeFromLastAttack = _timeBetweenAttack;
        }
        _timeFromLastAttack -= Time.deltaTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TakeDamage(_damageOnClick);
    }

    private void TakeDamage(int damage)
    {
        _enemyAnimator.SetTrigger("TakeDamage");

        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        if (_health == 0)
        {
            Die();
        }
    } 

    private void Die()
    {
        _enemyAnimator.SetBool("IsAlive", false);
        enabled = false;

        _deathParticles.transform.position = transform.position;
        _deathParticles.Play();

        Destroy(gameObject, _deathAnimationTime);
    }

    private void Attack()
    {
        var obj = _collisionObject;

        _enemyAnimator.SetTrigger("Attack");

        if (obj.CompareTag("Barricade"))
        {
            obj.GetComponent<Barricade>().TakeDamage(_enemyDamage);
        }
        else if (obj.CompareTag("Cannon"))
        {
            obj.GetComponent<Cannon>().TakeDamage(_enemyDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            TakeDamage(_damageFromSnowball);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Cannon") || collision.gameObject.CompareTag("Barricade"))
        {
            _isAttacking = true;
            _collisionObject = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cannon") || collision.gameObject.CompareTag("Barricade"))
        {
            _isAttacking = false;
            _collisionObject = null;
        }
    }
}
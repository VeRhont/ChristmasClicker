using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _speed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damageOnClick;
    private int _health;

    private Rigidbody2D _enemyRb;
    private Animator _enemyAnimator;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        //_enemyAnimator = GetComponent<Animator>();

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
}
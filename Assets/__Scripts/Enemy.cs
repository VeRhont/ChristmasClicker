using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;

    private Rigidbody2D _enemyRb;
    private Animator _enemyAnimator;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();

        //_enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        _enemyRb.MovePosition(transform.position + Vector3.left * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
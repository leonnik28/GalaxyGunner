using UnityEngine;

public class FireTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private bool _isDestroyed;

    private Animator _animator;
    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void GetDamage(float damage)
    {
        _animator = GetComponent<Animator>();
        if (_animator != null)
        {
            _animator.SetTrigger("GetDamage");
        }
        
        _health -= damage;
        if( _health <= 0)
        {
            if (_isDestroyed)
            {
                if (transform.TryGetComponent(out ILargeSphere largeSphere))
                {
                    largeSphere.KillLargeSphere();
                }
                gameObject.SetActive(false);
                UpdateHealth();
            }
            else
            {
                _animator.SetTrigger("DeathObject");
                UpdateHealth();
            }
        }
    }

    public void UpdateHealth()
    {
        _health = _maxHealth;
    }
}
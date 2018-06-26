using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DamagingObject : MonoBehaviour, IDamaging
    {
		[Header("Damaging")]
        [SerializeField] protected float maxHealth = 100;

        public float Health { get; private set; }
        public float HealthFraction => Health / maxHealth;
        public bool IsDead => Health <= 0;

        public event System.Action onDeath;

		protected virtual void Awake()
		{
            Health = maxHealth;
		}

        public virtual void DealDamage(float damage)
        {
            if (Health <= 0)
            {
                return;
            }

            Health = Mathf.Max(Health - damage, 0);
            if (Health == 0)
            {
                Death();
            }
        }

        public virtual void Death()
        {
            GetComponent<Collider2D>().enabled = false;
            Destroy(this);
            onDeath?.Invoke();
        }
    }
}

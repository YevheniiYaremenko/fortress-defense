using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour, IDamaging
    {
        [SerializeField] float maxHealth = 50;
		[SerializeField] float damage = 100;
		[SerializeField] int killBonus = 10;
        
        float health;

        public float Health => health;
		public int KillBonus => killBonus;

        public event System.Action onDeath;

        protected void Awake()
        {
            health = maxHealth;
        }

        public void DealDamage(float damage)
        {

        }

        public void Death()
        {
            onDeath?.Invoke();
        }
    }
}

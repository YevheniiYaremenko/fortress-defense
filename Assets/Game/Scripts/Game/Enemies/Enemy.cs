using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Enemy : DamagingObject, IDamaging
    {
        [Header("Enemy")]
		[SerializeField] float damage = 100;
		[SerializeField] int killBonus = 10;

        [Header("UI")]
        [SerializeField] GameObject ui;
        [SerializeField] Image healthBar;
        
		public int KillBonus => killBonus;

        public override void DealDamage(float damage)
        {
            base.DealDamage(damage);

            if (!IsDead && healthBar != null)
            {
                healthBar.fillAmount = Health / maxHealth;
            }
        }

        public override void Death()
        {
            base.Death();

            ui?.SetActive(false);
        }
    }
}

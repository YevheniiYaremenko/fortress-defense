using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Fortress : DamagingObject, IDamaging
    {
        [Header("View")]
        [SerializeField] Sprite[] sprites;
        [SerializeField] Sprite deathSprite;
        SpriteRenderer spriteRenderer;

		public float HealthFraction => Health / maxHealth;

		protected override void Awake()
		{
			base.Awake();
            spriteRenderer = GetComponent<SpriteRenderer>();
		}

        public override void DealDamage(float damage)
        {
            base.DealDamage(damage);

            if (!IsDead && sprites.Length > 0)
            {
                spriteRenderer.sprite = sprites[Mathf.Min(sprites.Length - 1, (int)(HealthFraction / maxHealth * sprites.Length))];
            }
        }
    }
}

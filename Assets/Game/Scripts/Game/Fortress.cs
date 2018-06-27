using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public class Fortress : DamagingObject, IDamaging
    {
        [Header("View")]
        [SerializeField] Sprite[] sprites;
        [SerializeField] Sprite deathSprite;
        SpriteRenderer spriteRenderer;

		[Header("Unit placements")]
		[SerializeField] List<UnitPlacement> unitPlacements;

		public bool CanPlaceUnit => unitPlacements.Any(p => !p.IsBusy);

		protected override void Awake()
		{
			base.Awake();
            spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void Init(System.Action<Unit> onRemove, System.Action onDestroy)
		{
			onDeath += onDestroy;
		}

        public override void DealDamage(float damage)
        {
            base.DealDamage(damage);
            if (!IsDead && sprites.Length > 0)
            {
                spriteRenderer.sprite = sprites[
					Mathf.Min(sprites.Length - 1, 
					(int)((1 - HealthFraction) * sprites.Length))];
            }
        }

		public override void Death()
		{
			base.Death();
			Destroy(this);
			spriteRenderer.sprite = deathSprite;
		}

        public void HighlightUnitPlacements(System.Action<UnitPlacement> onSelect) => unitPlacements.ForEach(p => p.Highlight(onSelect));
        public void UnhighlightUnitPlacements() => unitPlacements.ForEach(p => p.Unhighlight());
    }
}

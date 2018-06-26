using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UnitPlacement : MonoBehaviour
    {
		[SerializeField] GameObject selectButton;
		[SerializeField] GameObject removeUnitButton;

		Unit unit;
		bool highlight;

		public Unit PlacedUnit => unit;
		public bool IsBusy => unit != null;

		void Update()
		{
			selectButton.SetActive(highlight && !IsBusy);
			removeUnitButton.SetActive(!highlight && IsBusy);
		}

		public void SetUnit(Unit unit)
		{
			if (IsBusy)
			{
				Debug.LogError("This placement is already busy");
			}

			this.unit = unit;
			unit.transform.SetParent(transform);
			unit.transform.localPosition = Vector3.zero;

            removeUnitButton.SetActive(true);
		}

		public void Highlight(System.Action<UnitPlacement> onSelect)
		{
			this.onSelect = onSelect;
			highlight = true;
		}

		public void Unhighlight()
		{
			highlight = false;
		}

        System.Action<UnitPlacement> onSelect;
		public void Select()
		{
			onSelect?.Invoke(this);
		}

		public void RemoveUnit()
		{
			if (IsBusy)
			{
				Destroy(unit.gameObject);
			}
		}
    }
}

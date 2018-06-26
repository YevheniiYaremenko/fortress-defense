using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ShopItem : MonoBehaviour
    {
		[SerializeField] Image iconView;
		[SerializeField] Text priceText;
        [SerializeField] Button button;
		[SerializeField] Image buttonView;
		[SerializeField] Color disableColor = new Color(.5f, .5f, .5f, .5f);
		int price;

		void Awake()
		{
			button = GetComponent<Button>();
		}

		public void Init(Sprite icon, int price, System.Action onBuy)
		{
			this.price = price;
			priceText.text = price.ToString();
			iconView.sprite = icon;
			this.onBuy = onBuy;
		}

		public void SetData(int coins, bool canPlaceUnit)
		{
			bool canBuy = price <= coins && canPlaceUnit;
			button.interactable = canBuy;
            buttonView.color = canBuy ? Color.white : disableColor;
            iconView.color = canBuy ? Color.white : disableColor;
		}

		System.Action onBuy;
		public void Buy() => onBuy?.Invoke();
    }
}

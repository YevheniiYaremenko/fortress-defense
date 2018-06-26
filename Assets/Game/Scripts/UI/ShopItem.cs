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

		public void SetData(int coins)
		{
			button.interactable = price <= coins;
		}

		System.Action onBuy;
		public void Buy() => onBuy?.Invoke();
    }
}

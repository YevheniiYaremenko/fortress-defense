using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UnitsShopScreen : Screen
    {
        [SerializeField] Transform itemsContainer;
        [SerializeField] ShopItem itemSample;
        List<ShopItem> items = new List<ShopItem>();

        public void Init(Unit[] units, System.Action<Unit> onBuy, System.Action onClose)
        {
            this.onClose = onClose;

            foreach(var unit in units)
            {
                var u = unit;
                var item = Instantiate(itemSample, itemsContainer);
                item.Init(u.Icon, u.Price, () => onBuy(u));
                item.gameObject.SetActive(true);
                items.Add(item);                        
            }
        }

        public void SetData(int coins, bool canPlaceUnit)
        {
            foreach(var item in items)
            {
                item.SetData(coins, canPlaceUnit);
            }
        }

        System.Action onClose;
        public void Close() => onClose?.Invoke();
    }
}
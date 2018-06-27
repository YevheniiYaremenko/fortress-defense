using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Unit : MonoBehaviour
    {
		[Header("View")]
		[SerializeField] Sprite icon;
		[SerializeField] int price;

		public Sprite Icon => icon;
		public int Price => price;

        protected List<Enemy> enemies = new List<Enemy>();

        public void SetEnemies(List<Enemy> enemies)
		{
			this.enemies = enemies;
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Unit : MonoBehaviour
    {
		[Header("View")]
		[SerializeField] Sprite icon;
		[SerializeField] int price;

		public Sprite Icon => icon;
		public int Price => price;
    }
}

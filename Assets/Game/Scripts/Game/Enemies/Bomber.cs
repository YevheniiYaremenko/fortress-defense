using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Bomber : Enemy
    {
        [SerializeField] GameObject bombPrefab;

        public override void Attack()
        {
            if (CanAttack)
            {
                //TODO
                base.Attack();
            }
        }
    }
}

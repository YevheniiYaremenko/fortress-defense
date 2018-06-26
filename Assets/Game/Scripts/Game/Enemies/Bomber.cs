using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Game
{
    public class Bomber : Enemy
    {
        [SerializeField] GameObject bombPrefab;
        [SerializeField] Transform throwPoint;
        Vector2 bombTargetPosition;

        protected override void Awake()
        {
            base.Awake();
            bombTargetPosition = Physics2D.Raycast(transform.position, moveDirection).point + new Vector2(2,1);
        }

        public override void Attack()
        {
            if (CanAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        IEnumerator AttackCoroutine()
        {
            var bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
            var endPosition = bombTargetPosition + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            var points = new Vector3[]
            {
                throwPoint.position,
                ((Vector2)throwPoint.position + endPosition) / 2f + Vector2.up,
                endPosition
            };
            yield return bomb.transform.DOPath(points, .5f).SetEase(Ease.Linear).WaitForCompletion();

            Destroy(bomb);

            base.Attack();
        }
    }
}

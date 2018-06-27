using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace Game
{
    public class Archer : Unit
    {
		[Header("Archer")]
		[SerializeField] float shootingRange = 10;
		[SerializeField] float damage = 20;
		[SerializeField] Transform shotPoint;
		[SerializeField] GameObject arrowPrefab;
		[SerializeField] float arrowSpeed = 10;

		Animator animator;
		Enemy selectedEnemy;

		void Awake()
		{
            animator = GetComponent<Animator>();
		}

		void Update()
		{
			if (selectedEnemy != null || enemies.Count == 0)
			{
				return;
			}

			var enemy = enemies.Where(e => Vector2.Distance(e.AimPoint.position, shotPoint.position) <= shootingRange)
				.OrderBy(e => Vector2.Distance(e.AimPoint.position, shotPoint.position))
				.FirstOrDefault();
			selectedEnemy = enemy;

			animator.SetBool("Attack", selectedEnemy != null);
		}

		public void Attack()
		{
			if (selectedEnemy != null)
			{
                StartCoroutine(AttackCoroutine());
			}
		}

        IEnumerator AttackCoroutine()
        {
            var arrow = Instantiate(arrowPrefab, shotPoint.position, shotPoint.rotation);
			var center = (Vector2)(selectedEnemy.AimPoint.position + shotPoint.position) / 2f;
            var points = new Vector3[]
            {
                shotPoint.position,
                shotPoint.position + shotPoint.up * Mathf.Abs((center.x - shotPoint.position.x) / shotPoint.up.x) ,
                selectedEnemy.AimPoint.position
            };
            yield return arrow.transform.DOPath(points, (selectedEnemy.AimPoint.position - shotPoint.position).magnitude / arrowSpeed, PathType.CatmullRom)
				.SetEase(Ease.Linear).WaitForCompletion();
            
			Destroy(arrow);

            selectedEnemy?.DealDamage(damage);
        }
    }
}

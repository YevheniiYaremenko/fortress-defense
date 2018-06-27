using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Enemy : DamagingObject, IDamaging
    {
        [Header("Enemy")]
		[SerializeField] float damage = 100;
        [SerializeField] float attackDistance = 1;
		[SerializeField] int killBonus = 10;
        [SerializeField] Transform aimPoint;
        Animator animator;
        Fortress fortress;
        Vector3 attackPosition;

        [Header("Movement")]
        [SerializeField] protected Vector3 moveDirection = Vector3.right;
        [SerializeField] float moveSpeed = 1;

        [Header("UI")]
        [SerializeField] GameObject ui;
        [SerializeField] Image healthBar;
        
        public Transform AimPoint => aimPoint;
		public int KillBonus => killBonus;
        protected bool CanAttack => Vector2.Distance(attackPosition, transform.position) <= attackDistance;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
            fortress = FindObjectOfType<Fortress>();
            attackPosition = Physics2D.Raycast(transform.position, moveDirection).point;
        }

        void Update()
        {
            if (IsDead)
            {
                return;
            }
            
            if (Vector2.Distance(attackPosition, transform.position) > attackDistance)
            {
                animator.SetBool("Move", true);
                var newPosition = Vector2.MoveTowards(transform.position, attackPosition, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            }
            else
            {
                animator.SetBool("Move", false);
            }
        }

        public virtual void Attack()
        {
            if (CanAttack)
            {
                fortress.DealDamage(damage);
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public override void DealDamage(float damage)
        {
            base.DealDamage(damage);

            if (!IsDead && healthBar != null)
            {
                healthBar.fillAmount = HealthFraction;
            }
        }

        public override void Death()
        {
            base.Death();

            animator.SetTrigger("Death");
            ui?.SetActive(false);
        }
    }
}

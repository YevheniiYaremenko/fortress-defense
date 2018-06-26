public interface IDamaging
{
	float Health { get; }
	bool IsDead { get; }
	void DealDamage(float damage);
	void Death();
    event System.Action onDeath;
}

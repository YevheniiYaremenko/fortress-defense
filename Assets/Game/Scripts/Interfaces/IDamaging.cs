public interface IDamaging
{
	float Health { get; }
	bool IsDead { get; }
	float HealthFraction { get; }
	void DealDamage(float damage);
	void Death();
    event System.Action onDeath;
}

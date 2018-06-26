using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Game.Spawner
{
    ///<summary>
    /// Class for instantiation objects whith some conditions
    ///</summary>
    public abstract class AbstractSpawner<T> : Singleton<AbstractSpawner<T>> where T : MonoBehaviour, IDamaging
    {
        [SerializeField] protected int minSpawnInstancesCount = 1;
        [SerializeField] protected int maxSpawnInstancesCount = 5;
        [SerializeField] protected float minSpawnPeriod = 1;
        [SerializeField] protected float maxSpawnPeriod = 10;
		[SerializeField] bool spawning;
		public bool Spawning 
		{
			get { return spawning; }
			set { spawning = value; }
		}

        System.Action<T> onSpawn;
        System.Action<T> onDeath;
		List<T> spawnPool;
		float spawnTimer;

		void Awake()
		{
			spawnPool = new List<T>();
		}

		void Update()
		{
			if (!Spawning)
			{
				return;
			}

			spawnPool = spawnPool.Where(x => x != null).ToList();

			if (spawnPool.Count < minSpawnInstancesCount)
			{
				Spawn();
				return;
			}

			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0 && spawnPool.Count < maxSpawnInstancesCount)
			{
				Spawn();
			}
		}

        ///<summary>
        ///<param name="onSpawn">On Spawn callback<param>
        ///<param name="onSpawn">On Destroy callback<param>
        ///</summary>
        public void SetData(System.Action<T> onSpawn, System.Action<T> onDeath)
        {
            this.onDeath = onDeath;
            this.onSpawn = onSpawn;
        }

        ///<summary>
        /// Destroy all spawned objects
        ///</summary>
        public void Reset()
        {
            spawnPool.ForEach(x => Destroy(x.gameObject));
			spawning = false;
        }

        ///<summary>
        /// Spawn new instance
        ///</summary>
        protected virtual void Spawn()
        {
            T item = Factory.Factory<T>.Instance.GetRandom(GetSpawnPoint());

            item.transform.SetParent(transform);
			item.onDeath += () => onDeath?.Invoke(item);
            spawnPool.Add(item);
            onSpawn?.Invoke(item);

            spawnTimer = Random.Range(minSpawnPeriod, maxSpawnPeriod);
        }

        ///<summary>
        /// Get spawn point
        ///</summary>
        protected abstract Vector3 GetSpawnPoint();
    }
}

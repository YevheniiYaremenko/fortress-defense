using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Game.Spawner
{
    public class EnemyZoneSpawner : AbstractSpawner<Enemy>
    {
        [SerializeField] BoxCollider2D spawnZone;
        
        protected override Vector3 GetSpawnPoint()
        {
            float zPos = Random.Range(-1f, 1f);
            float yPos = zPos * spawnZone.bounds.extents.y;

            return spawnZone.bounds.center +
                new Vector3(
                    Random.Range(-1f, 1f) * spawnZone.bounds.extents.x,
                    yPos,
                    -1 + zPos);
        }
    }
}

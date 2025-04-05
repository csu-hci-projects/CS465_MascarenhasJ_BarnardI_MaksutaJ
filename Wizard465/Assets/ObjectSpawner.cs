using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class ObjectSpawner : MonoBehaviour
    {
        Transform spawnPoint;

        GameObject objectToSpawn;
        
        public void SpawnObject()
        {
            if (objectToSpawn != null && spawnPoint != null)
            {
                Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Debug.LogError("Fireball prefab or spawn point not assigned.");
            }
        }

    }
}

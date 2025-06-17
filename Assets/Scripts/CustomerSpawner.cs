using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject[] customerPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Transform freeSpot = GetFreeSpawnPoint();
            if (freeSpot != null)
            {
                GameObject prefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];
                GameObject customer = Instantiate(prefab, freeSpot.position, Quaternion.identity);
                customer.transform.SetParent(freeSpot); // ✅ Attach to spot
            }
            else
            {
                // ⏳ Wait a bit and retry sooner
                yield return new WaitForSeconds(1f);
            }
        }
    }

    Transform GetFreeSpawnPoint()
    {
        foreach (Transform point in spawnPoints)
        {
            bool occupied = false;

            // Check all root-level objects to see if something is sitting at/near this point
            foreach (GameObject customer in GameObject.FindGameObjectsWithTag("Customer"))
            {
                if (Vector2.Distance(customer.transform.position, point.position) < 0.1f)
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
                return point;
        }

        return null;
    }

}

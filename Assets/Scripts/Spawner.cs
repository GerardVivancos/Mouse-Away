using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField]
	public GameObject enemyPrefab;

	[SerializeField]
	public GameObject target;

	[SerializeField]
	public float spawnInterval = 10f;

	private float elapsedTime = 0f;

	// Update is called once per frame
	void Update()
    {
		elapsedTime += Time.deltaTime;

        if (target != null && elapsedTime > spawnInterval)    {
			GameObject enemyInstance = Instantiate(enemyPrefab,
				Vector3.zero + (Random.insideUnitSphere * 10),
				Quaternion.identity);
			Enemy enemy = enemyInstance.GetComponent<Enemy>();
			enemy.target = target;
			elapsedTime = 0;
		}
    }
}

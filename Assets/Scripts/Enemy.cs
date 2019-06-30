using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	public GameObject target;

	[SerializeField]
	public float speed = 2f;

	[SerializeField]
	public float redirectInterval = 3f;

	private Vector3 rand = Vector3.one;
	private bool alive = true;
	private float elapsedTime = 0f;


	// Start is called before the first frame update
	void Start()
    {
        rand = Random.insideUnitSphere;
	}

    // Update is called once per frame
    void Update() {
		if (alive) {
			FollowPlayer();
		}
	}

	private void FollowPlayer() {
		elapsedTime += Time.deltaTime;

		if (elapsedTime > redirectInterval) {
			Debug.Log(rand);
			rand = Random.insideUnitSphere;
			elapsedTime = 0f;
			Debug.Log(rand);

		}

		Vector3 distance = transform.position - target.transform.position;

		Vector3 l = new Vector3(Mathf.Lerp(target.transform.position.x, rand.x * 200 + target.transform.position.x, distance.x / 100),
						Mathf.Lerp(target.transform.position.y, rand.y * 200 + target.transform.position.y, distance.y / 100),
						Mathf.Lerp(target.transform.position.z, rand.z * 100 + target.transform.position.z, Mathf.Max(distance.x, distance.y) / 100));
		Vector3 p = (transform.position - l) * speed * Time.deltaTime;
		transform.position -= p;
	}

	private void OnTriggerEnter(Collider other) {
		BeginDieSequence();
	}

	private void BeginDieSequence() {
		alive = false;
		MeshRenderer sphere = GetComponentInChildren<MeshRenderer>();
		sphere.enabled = false;
		GetComponent<SphereCollider>().enabled = false;
		ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
		particles.Play();
		IEnumerator dieRoutine = Die();
		StartCoroutine(dieRoutine);
	}

	private IEnumerator Die() {
		yield return new WaitForSeconds(2);
		Destroy(transform.gameObject);
	}

}

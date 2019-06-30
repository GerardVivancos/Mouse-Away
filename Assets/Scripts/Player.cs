using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	public float speed = 4f;

	bool alive = true;
	// Start is called before the first frame update
	void Start() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}

	// Update is called once per frame
	void Update() {
		if (alive) {
			transform.position -= (transform.position - MousePositionWorld()) * speed * Time.deltaTime;
		}

	}

	private Vector3 MousePositionWorld() {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -1 * (Camera.main.transform.position.z - transform.position.z);
		Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePosition);
		return mousePositionWorld;
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

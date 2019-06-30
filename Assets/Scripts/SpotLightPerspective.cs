using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpotLightPerspective : MonoBehaviour
{

	private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
		parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
		transform.eulerAngles = new Vector3(parent.position.y * -3.5f, parent.position.x * 3.5f, 0);
    }
}

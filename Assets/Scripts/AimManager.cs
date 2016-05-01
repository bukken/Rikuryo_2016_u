using UnityEngine;
using System.Collections;

public class AimManager : MonoBehaviour {
	void Update () {
		Vector3 mouse_pos = Input.mousePosition;
		gameObject.transform.position = mouse_pos;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour {

	public GameObject player;
	bool hook_flag;
	Vector3 obj_point;

	// Use this for initialization
	void Start () {
		hook_flag = false;
		obj_point = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
        if (!hook_flag && Physics.Raycast (ray, out hit, 300) && Input.GetMouseButton(0)) {
 			obj_point = hit.point;
			hook_flag = true;
		}
		if (hook_flag) {
			player.SendMessage ("HookShot",obj_point);
			if(Input.GetMouseButtonUp(0)){
				hook_flag = false;
				player.SendMessage("HookUp");
			}
		}
	}
}

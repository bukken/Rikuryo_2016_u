using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	void Update () {
        Vector3 m_pos = transform.localPosition;
	    if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localPosition = new Vector3(m_pos.x*-1, m_pos.y, m_pos.z*-1);
            transform.Rotate(new Vector3(0f, 180f, 0f));
        }
	}
}

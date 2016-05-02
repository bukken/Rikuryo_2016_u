using UnityEngine;
using System.Collections;

public class objectPoper : MonoBehaviour {

    public GameObject cube;
    
	void Start () {
        for (int i = 0; i < 400; i++){
            float x = Random.Range(0, 100);
            float y = Random.Range(0, 100);
            float z = Random.Range(0, 1000);
            float size = Random.Range(1, 10);
            cube.transform.localScale = new Vector3(size, size, size);
            Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    void Update()
    {
		if (Input.GetKey(KeyCode.F))
        {
            print("f key was pressed");
			transform.Translate(Vector3.forward * Time.deltaTime);
        }
        // Move the object forward along its z axis 1 unit/second.
        //transform.Translate(Vector3.forward * Time.deltaTime);
		Physics.SyncTransforms();
    }
}
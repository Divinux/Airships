using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {
public float time = 1f;
	// Use this for initialization
	void Start () {
	Invoke("Die", time);
	}
	
	// Update is called once per frame
	void Die () {
	Destroy(gameObject);
	}
}

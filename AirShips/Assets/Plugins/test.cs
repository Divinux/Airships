using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	public GameObject hand;
    // Start is called before the first frame update
	 [ContextMenu ("find")]
    void asd()
    {
         hand = GameObject.Find("ClockPlace(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

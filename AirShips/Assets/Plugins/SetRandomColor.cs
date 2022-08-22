using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomColor : MonoBehaviour
{
	public Color cColor;
	public vColor vCol;
	public GameObject man;
	public xkcdColorReader xk;
    // Start is called before the first frame update
    void Start()
    {
	Invoke("clr", 1);
	
		
	}
	public void clr()
	{
		if(vCol.sName == "")
	{
		Debug.Log("No Name, generating new colro");
		man = GameObject.FindWithTag("GameManager");
		xk = man.GetComponent<xkcdColorReader>();
		xk.GenerateRandomColor();
		vCol.sName = xk.color.sName;
		vCol.sValue = xk.color.sValue;
		vCol.cColor = xk.color.cColor;
		
		
        // Get the Renderer component from the new cube
		var cRenderer = GetComponent<Renderer>();
		
		// Call SetColor using the shader property name "_Color" and setting the color to red
		cRenderer.material.SetColor("_Color", vCol.cColor);
		}
		else
		{
			Debug.Log("Color already set! " + vCol.sName);
			}
		
		}
	
   
}

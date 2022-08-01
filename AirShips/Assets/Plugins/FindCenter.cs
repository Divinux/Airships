using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCenter : MonoBehaviour
{
    [ContextMenu ("CalcCenter")]
    void CalculateCenter()
    {
        calculateCentroid();
    }

    
	
	void calculateCentroid()
	{
		if ( transform.root.gameObject == transform.gameObject )
		{
			Vector3 center = Vector3.zero;
 
			if ( transform.childCount > 0 )
			{
				List<Transform> allChildren = new List<Transform>();
				 GetComponentsInChildren<Transform>(false, allChildren);
				foreach (Transform child in allChildren)
				{
					center += child.transform.position;
				}
				center /= (allChildren.Count);
			}
			GameObject.Find("Center").transform.position = center;
		}
	}
}

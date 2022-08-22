using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDs : MonoBehaviour
{
	public string UID;
	public string PUID;
   
	[ContextMenu ("GetNewUid")]
	public void GenerateUID()
	{
		if(UID == "")
		{
			UID = System.Guid.NewGuid().ToString();
		}
	}
}

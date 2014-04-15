using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveTokenToSpace : MonoBehaviour {

	public Transform spaceObject;

	public Transform cornerGo;
	public Transform cornerJail;
	public Transform cornerFP;
	public Transform cornerToJail;

	public void TriggerMove()
	{
		Transform parent = spaceObject.parent.transform;
			
		int playerIDMoving = GameManager.instance.currentTurnPlayerID;

		GameObject go = GameObject.Find("player" + playerIDMoving + "Token").gameObject;

		go.transform.localRotation = new Quaternion(parent.localRotation.x, parent.localRotation.y, parent.localRotation.z,
		                                            parent.localRotation.w);

		Vector3 nextLocal = new Vector3(parent.localPosition.x+spaceObject.localPosition.x
		                                , 0.0f, parent.localPosition.z+spaceObject.localPosition.z+0.75f);

		/*List<Vector3> path = new List<Vector3>();

		if(spaceObject.localRotation.y < 0.1f && spaceObject.transform.localRotation.y != 0.0f)
		{
			path.Add(cornerGo.localPosition);
		}
		else if(spaceObject.localRotation.y == 0.5f)
		{
			path.Add(cornerJail.localPosition);
		}
		else if(spaceObject.localRotation.y == 1.0f)
		{
			path.Add(cornerFP.localPosition);
		}
		else if(spaceObject.localRotation.y == 1.5f)
		{
			path.Add(cornerToJail.localPosition);
		}

		if(path.Count > 0)
		{
			path.Add(nextLocal);

			Hashtable args = iTween.Hash("path", path.ToArray(), "time", 3.0f); 

			iTween.MoveTo(go, args);
		}
		else
		{*/
			iTween.MoveTo(go, nextLocal, 3.0f);
		//}
	}
}

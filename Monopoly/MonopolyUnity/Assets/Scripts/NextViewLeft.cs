using UnityEngine;
using System.Collections;

public class NextViewLeft : MonoBehaviour {

	public Camera mCamera;
	public NextViewRight rightArrow;

	private readonly Vector3 stage1 = new Vector3(-3.0f, 3.5f, 1.0f);
	private readonly Vector3 stage2 = new Vector3(-1.0f, 3.5f, 2.5f);
	private readonly Vector3 stage3 = new Vector3(-2.5f, 3.5f, 4.5f);
	private readonly Vector3 stage4 = new Vector3(-4.5f, 3.5f, 3.0f);

	private Quaternion rotation;
	[HideInInspector]
	public int currentStage = 0;

	void Start()
	{
		mCamera.transform.position = stage1;
		rotation = mCamera.transform.rotation;
	}

	void OnClick()
	{
		switch(currentStage)
		{
		case 0:
			mCamera.transform.position = stage2;
			mCamera.transform.rotation = new Quaternion(rotation.x, (rotation.y+90)%360, rotation.z, rotation.w);
			currentStage = 3;
			rightArrow.currentStage = currentStage;
			break;
		case 1:
			mCamera.transform.position = stage3;
			mCamera.transform.rotation = new Quaternion(rotation.x, (rotation.y+90)%360, rotation.z, rotation.w);
			currentStage = currentStage-1;
			rightArrow.currentStage = currentStage;
			break;
		case 2:
			mCamera.transform.position = stage4;
			mCamera.transform.rotation = new Quaternion(rotation.x, (rotation.y+90)%360, rotation.z, rotation.w);
			currentStage = currentStage-1;
			rightArrow.currentStage = currentStage;
			break;
		case 3:
			mCamera.transform.position = stage1;
			mCamera.transform.rotation = new Quaternion(rotation.x, (rotation.y+90)%360, rotation.z, rotation.w);
			currentStage = currentStage-1;
			rightArrow.currentStage = currentStage;
			break;
		default:
			Debug.LogError("Camera buttons shouldnt be at this index: " + currentStage++);
			break;
			
		}
	}
}

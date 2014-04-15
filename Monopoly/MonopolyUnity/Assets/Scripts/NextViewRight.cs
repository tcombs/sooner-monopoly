using UnityEngine;
using System.Collections;

public class NextViewRight : MonoBehaviour {

	public Camera mCamera;
	public NextViewLeft leftArrow;

	private readonly Vector3 stage1 = new Vector3(-3.0f, 3.5f, 1.0f);
	private readonly Vector3 stage2 = new Vector3(-1.0f, 3.5f, 2.5f);
	private readonly Vector3 stage3 = new Vector3(-2.5f, 3.5f, 4.5f);
	private readonly Vector3 stage4 = new Vector3(-4.5f, 3.5f, 3.0f);
	
	private readonly Quaternion stage1Rot = new Quaternion(75.0f, 0.0f, 0.0f, 0.0f);
	private readonly Quaternion stage2Rot = new Quaternion(75.0f, -90.0f, 0.0f, 0.0f);
	private readonly Quaternion stage3Rot = new Quaternion(75.0f, -180.0f, 0.0f, 0.0f);
	private readonly Quaternion stage4Rot = new Quaternion(75.0f, -270.0f, 0.0f, 0.0f);

	[HideInInspector]
	public int currentStage = 0;

	void OnClick()
	{
		switch(currentStage)
		{
		case 0:
			mCamera.transform.localPosition = stage2;
			mCamera.transform.localRotation = stage2Rot;
			currentStage = (currentStage+1)%4;
			leftArrow.currentStage = currentStage;
			break;
		case 1:
			mCamera.transform.localPosition = stage3;
			mCamera.transform.localRotation = stage3Rot;
			currentStage = (currentStage+1)%4;
			leftArrow.currentStage = currentStage;
			break;
		case 2:
			mCamera.transform.localPosition = stage4;
			mCamera.transform.localRotation = stage4Rot;
			currentStage = (currentStage+1)%4;
			leftArrow.currentStage = currentStage;
			break;
		case 3:
			mCamera.transform.localPosition = stage1;
			mCamera.transform.localRotation = stage1Rot;
			currentStage = (currentStage+1)%4;
			leftArrow.currentStage = currentStage;
			break;
		default:
			Debug.LogError("Camera buttons shouldnt be at this index: " + currentStage++);
			break;

		}
	}
}

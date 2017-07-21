using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    CameraRaycaster cameraRayCaster;

	void Start () {
        cameraRayCaster = GetComponent<CameraRaycaster>();
	}

	void Update () {
		//print (cameraRayCaster.layerHit);
	}
}

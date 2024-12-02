using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform player;

	void LateUpdate () {
		transform.position = player.position + (-player.forward * 4.0f) + (player.up * 1.5f);
		transform.LookAt (player.position+Vector3.up);
	}
}

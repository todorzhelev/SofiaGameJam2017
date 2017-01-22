using UnityEngine;
using System.Collections;
//http://stackoverflow.com/questions/22015697/how-to-keep-2-objects-in-view-at-all-time-by-scaling-the-field-of-view-or-zy
public class CameraFollow : MonoBehaviour 
{
	public Vector3 offset;
	public Vector3 rotation;

	public Transform player1;
	public Transform player2;

	private const float DISTANCE_MARGIN = 1.0f;

	private Vector3 middlePoint;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;
	void Awake ()
	{
		aspectRatio = Screen.width / Screen.height;
		tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

	}

	void Start() {
		Debug.Assert (player1 != null, "Player 1 is not set in CameraFollow Script");
		Debug.Assert (player2 != null, "Player 2 is not set in CameraFollow Script");
	}

	void Update () {
		// Position the camera in the center.
		Vector3 newCameraPos = Camera.main.transform.position;
		newCameraPos.x = middlePoint.x;
		Camera.main.transform.position = newCameraPos;

		// Find the middle point between players.
		Vector3 vectorBetweenPlayers = player2.position - player1.position;
		middlePoint = player1.position + 0.5f * vectorBetweenPlayers;

		// Calculate the new distance.
		distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
		cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

		// Set camera to new position.
		Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
		Camera.main.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
		Camera.main.transform.position += offset;
		Camera.main.transform.localEulerAngles = rotation;
	}
}

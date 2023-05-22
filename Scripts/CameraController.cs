using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Transform player;
	private Vector3 initialPos;
	private float deltaY;
	private float startYPlayer;

	void Start()
	{
		initialPos = transform.position;
		startYPlayer = player.position.y;
		deltaY = startYPlayer - initialPos.y;
	}

	void LateUpdate()
	{
		float playerY = player.position.y;

		if (transform.position.y + deltaY > playerY || playerY == startYPlayer)
		{
			initialPos.y = playerY - deltaY;
			transform.position = initialPos;
		}
	}
}



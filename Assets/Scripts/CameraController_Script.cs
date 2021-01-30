using UnityEngine;

public class CameraController_Script : MonoBehaviour
{
    GameObject playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        gameObject.transform.position = new Vector3(0, playerPos.transform.position.y, -10);
    }
}
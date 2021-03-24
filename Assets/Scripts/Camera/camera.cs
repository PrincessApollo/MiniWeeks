using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    [Space(25)]
    [SerializeField] float cameraOffsetY = -1;

    Transform player1, player2;
    private Transform cameraObject;

    float distance;
    void Update()
    {
        transform.position = new Vector3(player.position.x, cameraOffsetY, -1);
        //distance = Vector2.Distance(player1.position, player2.position);
    }
}
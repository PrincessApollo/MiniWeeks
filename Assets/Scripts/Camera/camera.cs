using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        transform.position = new Vector3(player.position.x, 3, -1);
    }
}

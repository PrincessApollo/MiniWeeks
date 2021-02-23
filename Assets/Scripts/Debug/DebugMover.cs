using UnityEngine;
using static UnityEngine.Input;

/*
    Written in whole or in part by F
    Licensed in whole or in part according to license https://static.princessapollo.se/licenses/mit-t.txt
    ***
    Not meant to make it into any part of the final product
*/

namespace EDebug
{
    public class DebugMover : MonoBehaviour
    {
        [SerializeField]
        float movementScale = 1f;
        [SerializeField]
        KeyCode keyLeft = KeyCode.LeftArrow;
        [SerializeField]
        KeyCode keyRight = KeyCode.RightArrow;
        [SerializeField]
        KeyCode keyUp = KeyCode.UpArrow;
        [SerializeField]
        KeyCode keyDown = KeyCode.DownArrow;
        Rigidbody2D rb = null;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        // Update is called once per frame
        void Update()
        {
            Vector2 newPos = rb.position;
            if (GetKey(keyLeft))
            {
                newPos += new Vector2(-1 * Time.deltaTime * movementScale, 0);
            }
            if (GetKey(keyRight))
            {
                newPos += new Vector2(1 * Time.deltaTime * movementScale, 0);
            }
            if (GetKey(keyUp))
            {
                newPos += new Vector2(0, 1 * Time.deltaTime * movementScale);
            }
            if (GetKey(keyDown))
            {
                newPos += new Vector2(0, -1 * Time.deltaTime * movementScale);
            }
            rb.MovePosition(newPos);

        }
    }
}
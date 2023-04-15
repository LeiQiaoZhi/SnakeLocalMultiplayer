using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Snake
{
    [RequireComponent(typeof(SnakeMovement))]
    public class SnakeInput : MonoBehaviour
    {
        public KeyCode leftKey;
        public KeyCode rightKey;
        public KeyCode topKey;
        public KeyCode downKey;

        public float tickInterval = 0.3f;

        private SnakeMovement snakeMovement;
        private Direction direction = Direction.Top();

        // Start is called before the first frame update
        private void Awake()
        {
            snakeMovement = GetComponent<SnakeMovement>();
        }

        private void Update()
        {
            direction = GetInputDirection();
        }

        public IEnumerator SnakeInputCoroutine()
        {
            while (true)
            {
                XLogger.Log(direction.changeOfCoord);
                snakeMovement.Move(direction);
                yield return new WaitForSeconds(tickInterval);
            }
        }

        private Direction GetInputDirection()
        {
            if (Input.GetKey(leftKey))
            {
                return Direction.Left();
            }

            if (Input.GetKey(rightKey))
            {
                return Direction.Right();
            }

            if (Input.GetKey(topKey))
            {
                return Direction.Top();
            }

            if (Input.GetKey(downKey))
            {
                return Direction.Bottom();
            }

            return direction;
        }
    }
}
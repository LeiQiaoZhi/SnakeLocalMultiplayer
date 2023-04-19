using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Snake
{
    [RequireComponent(typeof(SnakeMovement))]
    public class SnakeInput : MonoBehaviour
    {
        private KeyCode leftKey;
        private KeyCode rightKey;
        private KeyCode topKey;
        private KeyCode downKey;

        public float tickInterval = 0.3f;
        public float tickMultiplier = 0.95f;
        public float accelerateInterval = 5f;

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
            StartCoroutine(Accelerate());
            while (true)
            {
                XLogger.Log(direction.changeOfCoord);
                snakeMovement.Move(direction);
                yield return new WaitForSeconds(tickInterval);
            }
        }
        
        IEnumerator Accelerate()
        {
            while (true)
            {
                yield return new WaitForSeconds(accelerateInterval);
                tickInterval *= tickMultiplier;
            }
        }

        public void SetControls(ControlOption controlOption)
        {
            leftKey = controlOption.left;
            rightKey = controlOption.right;
            topKey = controlOption.up;
            downKey = controlOption.down;
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
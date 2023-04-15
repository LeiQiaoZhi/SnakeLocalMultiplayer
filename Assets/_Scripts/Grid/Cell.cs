using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Grid
{
    public class Cell : MonoBehaviour
    {
        public int x;
        public int y;
        [Header("Appearance")]
        public SpriteRenderer spriteRenderer;
        public Color color1;
        public Color color2;

        private GridSystem gridSystem;

        public void Init(int _x, int _y, GridSystem grid)
        {
            x = _x;
            y = _y;
            gridSystem = grid;

            // alternate colors
            if ((x+y) % 2 == 0)
            {
                spriteRenderer.color = color1;
            }
            else
            {
                spriteRenderer.color = color2;
            }
        }

        public List<Cell> GetNeighbours()
        {
            List<Cell> neighbours = new List<Cell>();
            var top = gridSystem.GetCell(x, y + 1);
            if (top)
            {
                neighbours.Add(top);
            }

            var down = gridSystem.GetCell(x, y - 1);
            if (down)
            {
                neighbours.Add(down);
            }

            var left = gridSystem.GetCell(x - 1, y);
            if (left)
            {
                neighbours.Add(left);
            }

            var right = gridSystem.GetCell(x + 1, y);
            if (right)
            {
                neighbours.Add(right);
            }

            return neighbours;
        }

    
    }
}
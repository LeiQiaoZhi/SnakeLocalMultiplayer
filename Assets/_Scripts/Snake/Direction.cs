using UnityEngine;

public class Direction
{
    public Vector2Int changeOfCoord;
    public Direction(Vector2Int changeOfCoord)
    {
        this.changeOfCoord = changeOfCoord;
    }

    public Direction Opposite()
    {
        return new Direction(-changeOfCoord);
    }

    public override bool Equals(object other)
    {
        if (other is not Direction)
            return false;
        Direction otherDirection = (Direction)other;
        return otherDirection.changeOfCoord == changeOfCoord;
    }

    public static Direction Top()
    {
        return new Direction(new Vector2Int(0, 1));
    }

    public static Direction Bottom()
    {
        return new Direction(new Vector2Int(0, -1));
    }

    public static Direction Left()
    {
        return new Direction(new Vector2Int(-1, 0));
    }

    public static Direction Right()
    {
        return new Direction(new Vector2Int(1, 0));
    }
}
using UnityEngine;

public class SnakeInitInfo
{
    public Color color;
    public ControlOption control;
    public string name;

    public SnakeInitInfo(Color _color, ControlOption _control, string name)
    {
        color = _color;
        control = _control;
        this.name = name;
    }

    public override string ToString()
    {
        return $"SnakeInitInfo: {name} {color} {control}";
    }
}
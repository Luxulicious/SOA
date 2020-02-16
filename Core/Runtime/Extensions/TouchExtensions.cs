using UnityEngine;

//TODO Move to standard library
public static class TouchExtensions
{
    public static Vector2 GetWorldPosition2D(this Touch touch)
    {
        return Camera.main.ScreenToWorldPoint(touch.position);
    }

    public static Vector3 GetWorldPosition3D(this Touch touch)
    {
        return Camera.main.ScreenToWorldPoint(touch.position);
    }
}
using UnityEngine;

public class IsInBetweenScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public bool IsBetween(Transform obj)
    {
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);
        float minY = Mathf.Min(pointA.position.y, pointB.position.y);
        float maxY = Mathf.Max(pointA.position.y, pointB.position.y);

        Vector3 p = obj.position;
        return p.x >= minX && p.x <= maxX && p.y >= minY && p.y <= maxY;
    }
}

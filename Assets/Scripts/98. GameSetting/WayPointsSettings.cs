using UnityEngine;

public class WayPointsSettings : MonoBehaviour
{
    public Transform[] wayPoints;

    private void Awake()
    {
        wayPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}

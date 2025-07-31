using UnityEngine;

[ExecuteAlways] // Allows the gizmo to run in Edit mode
public class CameraCenterLineGizmo : MonoBehaviour
{
    public float lineLength = 5f; // Length of the forward line
    public Color lineColor = Color.green;

    private void OnDrawGizmos()
    {


        Gizmos.color = lineColor;
        Vector3 start = Camera.main.transform.position;
        Vector3 end = start + Camera.main.transform.forward * lineLength;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.1f); 
    }
}

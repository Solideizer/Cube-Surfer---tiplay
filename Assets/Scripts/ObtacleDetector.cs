using Managers;
using UnityEngine;
public class ObtacleDetector : MonoBehaviour
{
    private void Update ()
    {
        CheckForObstacles ();
    }

    private void CheckForObstacles ()
    {
        Vector3 fwd = transform.TransformDirection (Vector3.forward);
        RaycastHit hitInfo;
        Debug.DrawRay (transform.position, fwd * 5f, Color.red);
        if (Physics.Raycast (transform.position, fwd, out hitInfo, 0.5f) && hitInfo.transform.CompareTag ("ObstacleCube"))
        {
            Destroy (this.gameObject);
            AudioManager.PlaySound ("obstacle");
        }
    }
}
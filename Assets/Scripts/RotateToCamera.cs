using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private void Rotate(Camera camera)
    {
        transform.LookAt(new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z));
        transform.rotation *= Quaternion.Euler(new Vector3(0, 180, 0));
    }
    
    private void OnEnable()
    {
        Camera.onPreRender += Rotate;
    }

    private void OnDisable()
    {
        Camera.onPreRender -= Rotate;
    }
}

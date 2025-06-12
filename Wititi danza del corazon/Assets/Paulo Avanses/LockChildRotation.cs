using UnityEngine;

public class LockChildRotation : MonoBehaviour
{
 private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = originalRotation;
    }
}

using UnityEngine;

public class LockChildRotation : MonoBehaviour
{
    private Quaternion rotacionOriginal;
    private Vector3 escalaOriginal;

    void Start()
    {
        rotacionOriginal = transform.rotation;
        escalaOriginal = transform.lossyScale; 
    }

    void LateUpdate()
    {
        transform.rotation = rotacionOriginal;
        Vector3 parentScale = transform.parent != null ? transform.parent.lossyScale : Vector3.one;

        transform.localScale = new Vector3(
            escalaOriginal.x / parentScale.x,
            escalaOriginal.y / parentScale.y,
            escalaOriginal.z / parentScale.z
        );
    }
}

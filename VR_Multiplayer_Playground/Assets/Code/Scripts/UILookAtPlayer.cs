using UnityEngine;

public class UILookAtPlayer : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}

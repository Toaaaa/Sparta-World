using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}

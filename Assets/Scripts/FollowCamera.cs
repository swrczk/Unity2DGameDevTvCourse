using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject cameraTarget;
    public float smothTime;

    Vector3 currVelocity;
    // float timeCount = 0.0f;

    void LateUpdate()
    {
        var newPosition = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, transform.position.z);
        var time = smothTime * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref currVelocity, time);

        //rotation
        // transform.rotation = Quaternion.Slerp(transform.rotation, cameraTarget.transform.rotation, timeCount);
        // timeCount += time;
    }
}

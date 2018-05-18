using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RecordAndPlay;

//record mouse position as world coordinates
public class MouseRecorder : StringRecorder
{

    private Vector3 mouseWorldPos;

    protected new void Update()
    {
        base.Update();

        Vector3 mouse = Input.mousePosition;
        mouse.z = 10;

        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouse);

        if (isRecording)
        {
            string json = JsonUtility.ToJson(mouseWorldPos);
            Debug.Log(json);
            RecordData(json);
        }
    }

    void OnDrawGizmos()
    {
        if (isRecording)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.grey;
        }
        Gizmos.DrawSphere(mouseWorldPos, 1);
    }
}

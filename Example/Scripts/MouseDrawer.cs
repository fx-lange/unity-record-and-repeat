using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RecordAndPlay;

public class MouseDrawer : DataListener
{

    private MouseRecorder.MouseData mouseData = new MouseRecorder.MouseData();

    public override void ProcessData(DataFrame results)
    {
        StringData stringData = results as StringData;
        mouseData = JsonUtility.FromJson<MouseRecorder.MouseData>(stringData.data);
    }

    void OnDrawGizmos()
    {
        float radius;
        if (mouseData.pressed)
        {
            radius = 1.2f;
            Gizmos.color = Color.green;
        }
        else
        {
            radius = 1;
            Gizmos.color = Color.grey;
        }

        Gizmos.DrawWireSphere(mouseData.worldPos, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RecordAndPlay;

//record mouse position as world coordinates
public class MouseRecorder : StringRecorder
{
    
    [System.Serializable]
    public class MouseData
    {
        public Vector3 worldPos;
        public bool pressed;
    }
    
    private MouseData mouseData = new MouseData();

    protected new void Update()
    {
        base.Update();

        Vector3 mouse = Input.mousePosition;
        mouse.z = 10;

        mouseData.worldPos = Camera.main.ScreenToWorldPoint(mouse);
        mouseData.pressed = Input.GetMouseButton(0);

        if (isRecording)
        {
            string json = JsonUtility.ToJson(mouseData);
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
        Gizmos.DrawWireSphere(mouseData.worldPos, mouseData.pressed ? 1.2f : 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RecordAndPlay;

public class MouseDrawer : DataListener
{

    private Vector3 mouseWorldPos;
	
	//TODO activate draw gizmos in game view!

    public override void ProcessData(DataFrame results){
		StringData json = results as StringData;
		mouseWorldPos = JsonUtility.FromJson<Vector3>(json.data);
		Debug.Log(json);
	}

    void OnDrawGizmos()
    {
		Gizmos.color = Color.green;
        Gizmos.DrawSphere(mouseWorldPos, 1);
    }
}

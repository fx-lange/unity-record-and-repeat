using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RecordAndPlay;

public class MouseDrawer : DataListener
{

    private Vector3 mouseWorldPos;
	
	//TODO activate draw gizmos in game view!

    public override void ProcessData(DataFrame results){
		StringData stringData = results as StringData;
		mouseWorldPos = JsonUtility.FromJson<Vector3>(stringData.data);
	}

    void OnDrawGizmos()
    {
		Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(mouseWorldPos, 1);
    }
}

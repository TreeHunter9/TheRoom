using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Test : MonoBehaviour
{
    private Quaternion last = Quaternion.identity;
    private Quaternion now = Quaternion.identity;
    private void Update()
    {
        now = transform.rotation;
        //print(Quaternion.Angle(now, last));
        //print(now.eulerAngles - last.eulerAngles);
        last = transform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DashInfo", menuName = "Physics/DashInfo", order = 1)]
public class DashInfo : ScriptableObject {
	public float tapTimeThreshold  = 0.3f;
    public float dashTime          = 1.5f;
    public float dashSpeed         = 1.0f;
    public string buttonName       = "Dash";
}

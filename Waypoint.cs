using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;
    
    // Add other properties and methods relevant to the Waypoint class here.
}

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Waypoint/Waypoints Editor Tools")]
    public static void ShowWindow()
    {
        GetWindow<WaypointManagerWindow>("Mypoints Editor Tools");
    }

    public Transform waypointOrigin;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        if (waypointOrigin == null)
        {
            EditorGUILayout.HelpBox("Please assign a waypoint origin transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            CreateButtons();  // Corrected the method name to match the declaration
            EditorGUILayout.EndVertical();
        }
    }

    void CreateButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }

    void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(Waypoint));  // Changed to childCount for unique naming
        waypointObject.transform.SetParent(waypointOrigin, false);  // Corrected the transform assignment
        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        if (waypointOrigin.childCount > 1)
        {
            waypoint.previousWaypoint = waypointOrigin.GetChild(waypointOrigin.childCount - 2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;  // Corrected the property
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;  // Corrected "actice" to "active"
    }
}

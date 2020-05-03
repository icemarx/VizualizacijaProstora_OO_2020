using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    // ATTRIBUTES
    public GameObject model;
    public GameObject room_go;
    public Vector3 location;
    public float rot_angle;
    public bool isActive;

    protected Vector3[] exit_dir; // directional vectors pointing to the next room
    protected Room[] Neighbors;

    // METHODS
    /// <summary>
    /// Shows all neighbors and hides non-neighboring rooms
    /// </summary>
    protected void ShowNeighbors() {
        foreach(Room r in Neighbors) {
            if (r == null) {
                UnityEngine.Random rnd = new UnityEngine.Random();
                // generate new room and display it
            } else if(!r.isActive) {
                r.HideNeighborsExcept(this);
            } else {
                r.Display();
            }
        }
    }

    /// <summary>
    /// Hides all neighboring rooms except for the one provided 
    /// </summary>
    /// <param name="room"></param>
    private void HideNeighborsExcept(Room room) {
        foreach (Room r in Neighbors) {
            if (r.isActive && r != room) r.Hide();
        }
    }

    /// <summary>
    /// Hides the room, making it inactive
    /// </summary>
    private void Hide() {
        model.SetActive(false);
        room_go.SetActive(false);
        isActive = false;
    }

    /// <summary>
    /// Displays the room, making it active
    /// </summary>
    private void Display() {
        room_go.SetActive(true);
        model.SetActive(true);
        isActive = true;
    }
}

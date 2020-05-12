using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room
{
    // CONSTANTS
    public readonly float EDGE_SIZE = 6;
    public float angle;

    // ATTRIBUTES
    public PlayerController pc = GameObject.Find("Main Camera").GetComponent<PlayerController>();
    public GameObject model;
    public GameObject room_go;
    public Vector3 location;
    public float rot_angle;
    public bool isActive;

    protected Vector3[] exit_dir; // directional vectors pointing to the next room
    public Room[] Neighbors;

    // METHODS
    /// <summary>
    /// Shows all neighbors and hides non-neighboring rooms
    /// </summary>
    public void ShowNeighbors() {
        for(int i = 0; i < Neighbors.Length; i++) {

            if (Neighbors[i] == null) {
                // generate new room and display it
                if (UnityEngine.Random.Range(0, 2) == 0) {       // triangle room
                    // calculate positon
                    Vector3 spawnLocation = location + exit_dir[i] *
                        (CalculateHalfDistanceMagnitude() + (new TriangleRoom()).CalculateHalfDistanceMagnitude());

                    Neighbors[i] = new TriangleRoom(this, spawnLocation, rot_angle+i*angle);    // TODO: fix starting rotation

                } else {                                        // square room
                    // calculate positon
                    Vector3 spawnLocation = location + exit_dir[i] *
                        (CalculateHalfDistanceMagnitude() + (new SquareRoom()).CalculateHalfDistanceMagnitude());

                    Neighbors[i] = new SquareRoom(this, spawnLocation, rot_angle);

                }
            }

            if(!Neighbors[i].isActive) {
                Neighbors[i].Display();
            }
        }
    }

    /// <summary>
    /// Hides all neighboring rooms except for the one provided 
    /// </summary>
    /// <param name="room"></param>
    public void HideNeighborsExcept(Room room) {
        foreach (Room r in Neighbors) {
            if (r != null && r.isActive && r != room) r.Hide();
        }
    }

    /// <summary>
    /// Hides the room, making it inactive
    /// </summary>
    private void Hide() {
        room_go.SetActive(false);
        if (model != null) model.SetActive(false);

        isActive = false;
    }

    /// <summary>
    /// Displays the room, making it active
    /// </summary>
    public void Display() {
        room_go.SetActive(true);
        if(model != null) model.SetActive(true);

        isActive = true;
    }

    /// <summary>
    /// Calculates the length of distance from the center of the room to the nearest edge.
    /// </summary>
    /// <returns>Distance based on the room type</returns>
    protected abstract float CalculateHalfDistanceMagnitude();

    /// <summary>
    /// Assigns a random object from the list to spawn in the room, as well as it's transform.
    /// </summary>
    protected void GenerateObject() {
        // generate position
        // Vector3 pos = room_go.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 pos = room_go.transform.position;

        // Apply random rotation on y
        float temp = UnityEngine.Random.rotation.eulerAngles.y;
        Quaternion rot = Quaternion.Euler(new Vector3(0, temp, 0));

        // chose random object
        GameObject go = pc.objects[UnityEngine.Random.Range(0, pc.objects.Length)];

        // display
        this.model = GameObject.Instantiate(go, pos, rot);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRoom : Room {
    private readonly static int EDGE_NUMBER = 3;

    public TriangleRoom() {
        angle = 360 / EDGE_NUMBER;
        location = new Vector3(0, 0, 0);
        rot_angle = 0f;

        // exit_dir = new Vector3[] {
        // };
        Neighbors = new Room[EDGE_NUMBER];

        isActive = false;
    }

    public TriangleRoom(Room neighbor, Vector3 location, float rot_angle) {
        angle = 360 / EDGE_NUMBER;
        Neighbors = new Room[EDGE_NUMBER];
        Neighbors[0] = neighbor;

        this.location = location;
        this.rot_angle = rot_angle;

        // exit_dir[0] = neighbor.room_go.transform.position - 
        room_go = GameObject.Instantiate(pc.rooms[0], location, Quaternion.Euler(-90, rot_angle, 0));

        isActive = false;
    }

    /// <summary>
    /// Calculates the length of distance from the center of the room to the nearest edge.
    /// </summary>
    /// <returns>Third of the height of a triangle</returns>
    protected override float CalculateHalfDistanceMagnitude() {
        return EDGE_SIZE * Mathf.Sqrt(3) / 6f;
    }
}

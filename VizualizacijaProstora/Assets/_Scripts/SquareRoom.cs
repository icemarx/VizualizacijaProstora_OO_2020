using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRoom : Room {
    private readonly static int EDGE_NUMBER = 4;

    public SquareRoom() {

        location = new Vector3(0,0,0);
        rot_angle = 0f;
        
        exit_dir = new Vector3[] {
            new Vector3(1,0,0),
            new Vector3(0,0,-1),
            new Vector3(-1,0,0),
            new Vector3(0,0,1)
        };
        Neighbors = new Room[EDGE_NUMBER];

        isActive = false;
    }

    public SquareRoom(Room neighbor, Vector3 location, float rot_angle) {
        Neighbors = new Room[EDGE_NUMBER];
        Neighbors[0] = neighbor;

        this.location = location;
        this.rot_angle = rot_angle;

        // exit_dir[0] = neighbor.room_go.transform.position - 
        room_go = GameObject.Instantiate(pc.rooms[1], location, Quaternion.Euler(-90, rot_angle, 0));

        isActive = false;
    }

    /// <summary>
    /// Calculates the length of distance from the center of the room to the nearest edge.
    /// </summary>
    /// <returns>Half of the length of the edge</returns>
    protected override float CalculateHalfDistanceMagnitude() {
        return EDGE_SIZE*0.5f;
    }
}

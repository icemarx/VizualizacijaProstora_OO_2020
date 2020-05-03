using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRoom : Room
{
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
    }

    public SquareRoom(Room neighbor, Vector3 location, float rot_angle) {
        Neighbors[0] = neighbor;
        this.location = location;
        this.rot_angle = rot_angle;

        // exit_dir[0] = neighbor.room_go.transform.position - 
    }
}

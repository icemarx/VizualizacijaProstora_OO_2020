/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRoom : Room {
    private readonly static int EDGE_NUMBER = 4;

    public SquareRoom() {
        angle = 360 / EDGE_NUMBER;

        location = new Vector3(0,0,0);
        rot_angle = 0f;
        
        exit_dir = new Vector3[] {
            new Vector3(1,0,0),
            new Vector3(0,0,-1),
            new Vector3(-1,0,0),
            new Vector3(0,0,1)
        };
        Neighbors = new Room[EDGE_NUMBER];

        isActive = true;
    }

    public SquareRoom(Room neighbor, Vector3 location, float rot_angle) {
        angle = 360 / EDGE_NUMBER;
        Neighbors = new Room[EDGE_NUMBER];
        Neighbors[0] = neighbor;

        this.location = location;
        this.rot_angle = rot_angle;

        exit_dir = new Vector3[EDGE_NUMBER];
        exit_dir[0] = Vector3.Normalize(neighbor.room_go.transform.position - location);
        for(int i = 1; i < EDGE_NUMBER; i++) {
            exit_dir[i] = Vector3.Normalize(Quaternion.Euler(0, angle, 0) * exit_dir[i - 1]);
        }

        room_go = GameObject.Instantiate(RandomRoomGO(pc.squareRooms), location, new Quaternion(0,0,0,0));
        room_go.transform.LookAt(neighbor.room_go.transform);
        room_go.transform.Rotate(new Vector3(1, 0, 0), -90);

        isActive = true;
        GenerateObject();
    }

    /// <summary>
    /// Calculates the length of distance from the center of the room to the nearest edge.
    /// </summary>
    /// <returns>Half of the length of the edge</returns>
    protected override float CalculateHalfDistanceMagnitude() {
        return EDGE_SIZE*0.5f;
    }
}
*/
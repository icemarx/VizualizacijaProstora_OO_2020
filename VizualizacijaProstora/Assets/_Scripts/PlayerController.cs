using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float rotSpeed;
    public float movSpeed;

    public CharacterController controller;
    public GameObject[] rooms;

    float xRotation = 0f;
    float yRotation = 0f;

    private Room currentRoom;
    private Room previousRoom;
    public GameObject cur_room_workaround;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        // generate first room
        currentRoom = new SquareRoom();
        currentRoom.room_go = GameObject.Instantiate(rooms[1], currentRoom.location, Quaternion.Euler(-90, 0, 0));
        currentRoom.Display();

        // TODO: impove
        cur_room_workaround = currentRoom.room_go;

        // currentRoom.ShowNeighbors();
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("New room");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
        previousRoom = currentRoom;
        foreach(Room r in currentRoom.Neighbors) {
            if(r != null && r.room_go == other.gameObject) {
                currentRoom = r;
                break;
            } 
        }
        currentRoom.ShowNeighbors();
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Trigger end");
        if (currentRoom.room_go == other.gameObject) {
            currentRoom.HideNeighborsExcept(previousRoom);
            currentRoom = previousRoom;
        } else {
            previousRoom.HideNeighborsExcept(currentRoom);
        }
    }

    private void Update() {
        // get new current room
        /*
        if(currentRoom.room_go != cur_room_workaround) {
            // find correct room
            foreach(Room r in currentRoom.Neighbors) {
                if(r.room_go == cur_room_workaround) {
                    currentRoom = r;
                    currentRoom.ShowNeighbors();
                    break;
                }
            }
        }
        */
    }

    void LateUpdate() {
        RotatePlayer();
        MovePlayer();
    }

    private void MovePlayer() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forwards   = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 rightwards = new Vector3(transform.right.x,   0, transform.right.z  );

        Vector3 move_dir = Vector3.Normalize(rightwards * x + forwards * z);

        controller.Move(move_dir * movSpeed * Time.deltaTime);
    }

    void RotatePlayer() {
        // get mouse input
        float x = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;

        // calculate rotation
        yRotation += x;
        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // apply rotation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

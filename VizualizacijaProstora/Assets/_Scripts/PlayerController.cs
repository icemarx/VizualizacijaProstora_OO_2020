﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float rotSpeed;
    public float movSpeed;

    public CharacterController controller;
    public AudioSource audio;
    public readonly int FOOTSTEP_DELAY = 42;
    private int f_delay = 0; 

    float xRotation = 0f;
    float yRotation = 0f;

    public GameObject currentRoom = null;
    public GameObject previousRoom = null;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other) {
        // Debug.Log("Trigger");

        if (other.CompareTag("Room")) {
            GameObject[] neighbors = currentRoom.GetComponent<Room2>().Neighbors;

            foreach (GameObject r in neighbors) {
                if (r != null && r == other.gameObject) {
                    previousRoom = currentRoom;
                    currentRoom = r;
                    break;
                }
            }

            ShowNeighbors(currentRoom);
        }
    }

    private void ShowNeighbors(GameObject room) {
        GameObject[] neighbors = currentRoom.GetComponent<Room2>().Neighbors;

        foreach (GameObject r in neighbors) {
            if (r != null) {
                r.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        // Debug.Log("Trigger end");

        if (other.CompareTag("Room")) {
            if (currentRoom == other.gameObject) {
                GameObject r = currentRoom;
                currentRoom = previousRoom;
                previousRoom = r;
            }
            
            HideNeighborsOfExcept(previousRoom, currentRoom);

        }
    }

    private void HideNeighborsOfExcept(GameObject room, GameObject exceptionRoom) {
        GameObject[] neighbors = room.GetComponent<Room2>().Neighbors;

        foreach (GameObject r in neighbors) {
            if (r != null && r != exceptionRoom) {
                r.SetActive(false);
            }
        }
    }


    // ================ MOVEMENT ETC. ================ //
    void Update() {
        if (controller.velocity.magnitude > 2f && !audio.isPlaying && f_delay >= FOOTSTEP_DELAY) {
            audio.volume = UnityEngine.Random.Range(.8f, 1);
            audio.pitch  = UnityEngine.Random.Range(.9f, 1.1f);

            audio.Play();
            f_delay = 0;
        }

        f_delay++;
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

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            move_dir *= 2;

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

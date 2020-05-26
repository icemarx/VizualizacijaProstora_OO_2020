using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : MonoBehaviour {

    /// <summary>
    /// How to use this:
    /// 1. Each room must have the "Room" Tag and this script attached
    /// 2. The starting room must be set to active and in the Main Camera Player Script, it should be added as currentRoom
    /// 3. The room must also have a trigger collider
    /// 4. Each room this one connects to must be listed among the neighbors.
    /// 5. Null values among neighbors are handled
    /// </summary>
    public GameObject[] Neighbors;

    public Material skybox;

    private void OnTriggerEnter(Collider other)
    {
        if (skybox != null && other.CompareTag("Player")) {
            RenderSettings.skybox = skybox;
        }
    }

}
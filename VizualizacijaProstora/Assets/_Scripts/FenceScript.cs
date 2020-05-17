using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScript : MonoBehaviour
{

    private float initH = 0f;

    // Start is called before the first frame update
    void Start()
    {
        initH = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            this.transform.position = new Vector3(this.transform.position.x, (0.00027f - 0.5f*(1f - (other.transform.position.x - this.transform.position.x) / 0.02f)), this.transform.position.z);
            //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.005f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.transform.position = new Vector3(this.transform.position.x, initH, this.transform.position.z);
        }
    }

}

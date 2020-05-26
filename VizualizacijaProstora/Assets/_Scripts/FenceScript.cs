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
            Vector3 myPos = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
            Vector3 playerPos = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            float dist = (playerPos - myPos).magnitude;
            float radius = 2f;
            float factor = dist / radius;
            factor = factor < 0f ? 0f : factor;
            factor = factor > 1f ? 1f : factor;
            this.transform.position = new Vector3(this.transform.position.x, (initH - this.transform.lossyScale.z*1.2f*(1f - factor)), this.transform.position.z);
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

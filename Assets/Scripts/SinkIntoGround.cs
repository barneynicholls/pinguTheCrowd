using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkIntoGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate((Vector3.up * Time.deltaTime) * -1);
    }
}

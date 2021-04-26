using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlapTrigger02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name + " is colliding");
    }

}

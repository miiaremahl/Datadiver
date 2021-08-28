using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headScript : MonoBehaviour
{
    public GameObject ARcamera; 
    // Start is called before the first frame update
    void Start()
    {
        ARcamera = GameObject.Find("AR Camera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(ARcamera.transform);
    }
}

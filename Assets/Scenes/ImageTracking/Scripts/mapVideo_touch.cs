using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapVideo_touch : MonoBehaviour
{

    int playVideoIndex = 0;
    public UnityEngine.Video.VideoPlayer videoIN;
    public UnityEngine.Video.VideoPlayer videoOUT;


    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount == 2)
            {

            }
            if (touch.tapCount == 1)
            {
                if (playVideoIndex <= 2) { playVideoIndex += 1; }
                else { playVideoIndex = 0; }
            }
        }
        if (playVideoIndex == 1) videoIN.Play();
        if (playVideoIndex == 2) videoOUT.Play();

    }
}

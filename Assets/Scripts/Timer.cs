using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
        int hour;
        int minute;
        int second;
        int millisecond;
        public Text text_timeSpend;
        public Text test_chenggong;
        int a = 0;

    //elapsed time
    float timeSpend = 0.0f;

    // display the text of the time zone

    // Use this for initialization
    void Start()
        {
        }

        // Update is called once per frame                                                                                              
        void Update()
        {
            if (a == 0)
            {
                timeSpend += Time.deltaTime;
                hour = (int)timeSpend / 3600;
                minute = ((int)timeSpend - hour * 3600) / 60;
                second = (int)timeSpend - hour * 3600 - minute * 60;
                millisecond = (int)((timeSpend - (int)timeSpend) * 1000);
                text_timeSpend.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
            }
        }
        void OnTriggerStay(Collider other)
        {
            a = 1;
            test_chenggong.enabled = true;
        }
}

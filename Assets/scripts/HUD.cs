using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text timetext;
    const string TimePrefix = "time: ";
    float elapsedseconds;
    bool timerrunning = true;

    // Start is called before the first frame update
    void Start()
    {
        elapsedseconds = 0;
        timetext.text = "0";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerrunning == true) {
            elapsedseconds += Time.deltaTime;
            timetext.text = TimePrefix + elapsedseconds.ToString();
              }
    }

    public void StopGameTimer()
    {
        timerrunning = false;
    }
}

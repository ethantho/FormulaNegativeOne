using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedAndTime : MonoBehaviour
{

    TextMeshProUGUI speedText;
    TextMeshProUGUI timerText;
    CarController cc;
    CarLapCounter clc;
    public float tt = 0;
    float spd;
    int dispSpd;
    // Start is called before the first frame update
    void Start()
    {
        speedText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        timerText = GetComponentsInChildren<TextMeshProUGUI>()[1];

        cc = GetComponentInParent<CarController>();
        clc = GetComponentInParent<CarLapCounter>();
        tt = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        spd = cc.totalSpeed;
        dispSpd = (int)(spd * 10);
        speedText.text = (dispSpd.ToString()) + " KPH";
        if (!clc.isRaceCompleted && cc.startedMoving)
        {
            tt += Time.deltaTime;
        }
        timerText.text = tt.ToString();


        if (cc.dead)
        {
            timerText.text = "DEAD\nPRESS R TO RESTART";
            
            this.enabled = false;
        }
    }
}

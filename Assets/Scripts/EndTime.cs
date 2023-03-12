using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTime : MonoBehaviour
{
    public CarLapCounter clc;
    public TextMeshProUGUI text;
    public SpeedAndTime sat;
    // Start is called before the first frame update
    void Start()
    {
        clc = GetComponentInParent<CarLapCounter>();
        text = GetComponent<TextMeshProUGUI>();
        sat = GetComponentInParent<SpeedAndTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!clc.isRaceCompleted)
        {
            text.enabled = false;
        }
        else
        {
            text.enabled = true;
            text.text = "FINISHED\n" + sat.tt + "\nPRESS R TO RESTART";
        }

    }
}

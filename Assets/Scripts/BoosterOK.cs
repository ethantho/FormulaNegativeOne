using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoosterOK : MonoBehaviour
{

    TextMeshProUGUI message;
    CarLapCounter clc;
    CarController cc;
    bool messageShown = false;
    float counter = 0;
    AudioSource snd;
    // Start is called before the first frame update
    void Start()
    {
        message = GetComponent<TextMeshProUGUI>();
        message.enabled = false;
        clc = GetComponentInParent<CarLapCounter>();
        cc = GetComponentInParent<CarController>();
        snd = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clc.lapsCompleted == 1 && !messageShown)
        {
            message.enabled = true;
            messageShown = true;
            counter = 2f;
            cc.gotBoostPower = true;
            snd.Play();
        }

        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            message.enabled = false;
        }
    }
}

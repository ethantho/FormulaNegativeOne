using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;
    int numberOfPassedCheckPoints = 0;

    public int lapsCompleted = 0;
    const int lapsToComplete = 3;

    public bool isRaceCompleted = false;

    int carPosition = 0;

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int GetNumberOfCheckpointsPassed()
    {
        return numberOfPassedCheckPoints;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    public event Action<CarLapCounter> OnPassCheckpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(isRaceCompleted == true)
        {
            return;
        }
        if (collision.CompareTag("CheckPoint"))
        {
            CheckPoint check = collision.GetComponent<CheckPoint>();

            if(passedCheckPointNumber + 1 == check.checkPointNumber)
            {
                passedCheckPointNumber = check.checkPointNumber;

                timeAtLastPassedCheckPoint = Time.time;

                numberOfPassedCheckPoints++;

                if (check.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if(lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                    }
                }

                OnPassCheckpoint?.Invoke(this);
            }
        }
    }


}

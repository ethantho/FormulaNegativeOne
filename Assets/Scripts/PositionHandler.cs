using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PositionHandler : MonoBehaviour
{

    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();
    // Start is called before the first frame update
    void Start()
    {
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        foreach (CarLapCounter lapCounter in carLapCounters)
            lapCounter.OnPassCheckpoint += OnPassCheckpoint;
    }


    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        //Debug.Log($"Event: Car {carLapCounter.gameObject.name} passed a checkpoint");

        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberOfCheckpointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

        int carPosition = carLapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPosition(carPosition);
    }

}

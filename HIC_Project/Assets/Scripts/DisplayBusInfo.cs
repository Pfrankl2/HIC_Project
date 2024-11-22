// Test script to display bus information in the Log.

using UnityEngine;

public class DisplayBusInfo : MonoBehaviour
{
    public BusDataLoader dataLoader;

    void Start()
    {
        dataLoader.LoadBusData(); // Ensure data is loaded first.

        // Access data
        foreach (var stop in dataLoader.busData.busStops)
        {
            Debug.Log($"Stop Name: {stop.name}, Latitude: {stop.latitude}, Longitude: {stop.longitude}");
        }
    }
}

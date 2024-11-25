// Test script to display bus information in the Log.

using UnityEngine;

public class DisplayBusInfo : MonoBehaviour
{
    public BusDataLoader dataLoader;

    void Start()
    {
        dataLoader.LoadBusData(); // Calls the "BusDataLoader" class to parse .json file and store all bus data.

        // Loops through and prints each Bus Stop.
        foreach (var stop in dataLoader.busData.busStops)
        {
            Debug.Log($"Stop Name: {stop.name}, Latitude: {stop.latitude}, Longitude: {stop.longitude}");
        }

        // Loops through and prints each Bus Route.
        foreach (var route in dataLoader.busData.busRoutes)
        {
            Debug.Log($"Route Name: {route.routeName}, Route ID: {route.routeID}");
        }
    }
}

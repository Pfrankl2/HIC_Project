// Bus Map System that places map markers for each Bus Stop of the .json file
// onto the map using X and Y positions.

using System.IO;
using UnityEngine;

public class BusStopMapper : MonoBehaviour
{
    public BusDataLoader dataLoader;
    public RectTransform mapRectTransform; // Stores the Rect Transform of the map to use for the coordinate system.
    public GameObject markerPrefab; // Stores the Prefab used for making bus stop markers.

    void Start()
    {
        dataLoader.LoadBusData(); // Calls the "BusDataLoader" class to parse .json file and store all bus data.
        PlaceMarkers();
    }

    void PlaceMarkers()
    {
        foreach (var busStop in dataLoader.busData.busStops)
        {
            GameObject marker = Instantiate(markerPrefab, mapRectTransform); // Instantiates marker with the "Map Marker" GameObject and the map's Transform.

            // Sets the Rect Transform of the Marker to place it within the map's Rect Transform.
            RectTransform markerRect = marker.GetComponent<RectTransform>();
            markerRect.anchoredPosition = new Vector2(busStop.x, busStop.y);

            marker.name = busStop.name; // Renames the marker to the name of the bus stop it is referencing.
        }
    }
}

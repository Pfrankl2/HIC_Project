// Script that implements the functionality of the "Bus Stops" page by adding all bus stops from "busData.json" into a dropdown menu
// and then updating TextMeshPro elements with the related bus stop information.

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BusRoutesPage : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Text busRouteNameText;
    public TMP_Text busRouteIDText;
    public TMP_Text busRoutePathText;

    public BusDataLoader dataLoader;
    private Dictionary<int, BusRoute> busRoutesDict = new Dictionary<int, BusRoute>(); // Creates a dictionary using "int" as each index of "BusStop" objects.
    private Dictionary<int, BusStop> busStopsDict = new Dictionary<int, BusStop>(); // Dictionary for quick lookup of bus stops by ID

    public GameObject[] routeVisualGameObjects; // Assign in the Inspector
    public Dictionary<int, GameObject> routeVisuals = new Dictionary<int, GameObject>(); // Map each route to its GameObject

    void Start()
    {
        dataLoader.LoadBusData();

        PopulateBusStopsDict();
        PopulateDropdown();

        // Populates the "routeVisuals" dictionary with the route visuals assigned in the Inspector.
        for (int i = 0; i < routeVisualGameObjects.Length; i++)
        {
            routeVisuals[i] = routeVisualGameObjects[i];
        }

        UpdateBusRouteInfo(0); // Sets the first "BusStop" object in the list as the default stop shown.

        dropdown.onValueChanged.AddListener(UpdateBusRouteInfo); // Creates a listener that looks for when the index value of
                                                                 // the dropdown menu changes to call "UpdateBusStopInfo".
    }

    // Populate the bus stops dictionary for quick lookup
    void PopulateBusStopsDict()
    {
        foreach (var stop in dataLoader.busData.busStops)
        {
            busStopsDict[stop.stopID] = stop; // Map each bus stop's ID to the BusStop object
        }
    }

    // Initializes the dropdown element with each "BusStop" object.
    void PopulateDropdown()
    {
        dropdown.ClearOptions(); // Removes default options before adding bus stops to the dropdown list.

        // Populates the dropdown options and dictionary with each bus stop by mapping them to each successive index of "busStopsDict".
        List<string> options = new List<string>(); // Creates a list of strings to hold the options of the dropdown menu.
        foreach (var route in dataLoader.busData.busRoutes)
        {
            options.Add(route.routeName); // Adds each bus stop's "name" variable to the list of strings "options" to be added to the dropdown menu.
            busRoutesDict[options.Count - 1] = route; // Maps each bus stop to successive indexes of "busStopsDict" (starting at 0).
        }
        dropdown.AddOptions(options); // Adds each bus stop as an option of the dropdown menu ("AddOptions()" takes a "List<string>" as its parameter).
    }

    // Takes the index of the selected options from the dropdown and updates all text elements.
    void UpdateBusRouteInfo(int index)
    {
        if (busRoutesDict.TryGetValue(index, out BusRoute selectedRoute)) // If the index currently selected is within "busStopsDict":
        {
            // Updates all TextMeshPro elements with bus stop information.
            busRouteNameText.text = $"Route Name: {selectedRoute.routeName}";
            busRouteIDText.text = $"Route ID: {selectedRoute.routeID}";

            // Retrieve and display the list of bus stops for the route
            List<string> stopNames = new List<string>();
            foreach (int stopID in selectedRoute.stops)
            {
                if (busStopsDict.TryGetValue(stopID, out BusStop stop)) // Find the stop by ID
                {
                    stopNames.Add(stop.name); // Add the stop name to the list
                }
            }

            // Join the names into a single string and display them
            busRoutePathText.text = $"Stops:\n{string.Join("->", stopNames)}";

            UpdateRouteVisualization(index);
        }
    }

    // Enables/Disables the bus route visuals on the map for each route.
    void UpdateRouteVisualization(int selectedIndex)
    {
        foreach (var routeVisual in routeVisuals)
        {
            if (routeVisual.Key == selectedIndex)
            {
                routeVisual.Value.SetActive(true); // Enable the selected route's GameObject
            }
            else
            {
                routeVisual.Value.SetActive(false); // Disable all other route GameObjects
            }
        }
    }
}

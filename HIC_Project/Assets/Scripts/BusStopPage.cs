// Script that implements the functionality of the "Bus Stops" page by adding all bus stops from "busData.json" into a dropdown menu
// and then updating TextMeshPro elements with the related bus stop information.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class BusStopPage : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Text busStopNameText;
    public TMP_Text busStopIDText;
    public TMP_Text busStopAddressText;

    public TMP_InputField searchBar;
    public Button searchButton;
    public GameObject homePage;
    public GameObject busStopsPage;

    public BusDataLoader dataLoader;
    private Dictionary<int, BusStop> busStopsDict = new Dictionary<int, BusStop>(); // Creates a dictionary using "int" as each index of "BusStop" objects.

    void Start()
    {
        dataLoader.LoadBusData();

        PopulateDropdown();
        UpdateBusStopInfo(0); // Sets the first "BusStop" object in the list as the default stop shown.

        dropdown.onValueChanged.AddListener(UpdateBusStopInfo); // Creates a listener that looks for when the index value of
                                                                // the dropdown menu changes to call "UpdateBusStopInfo".
        
        searchButton.onClick.AddListener(SearchAndSelectBusStop); // Creates a listener corresponding to the search button of
                                                                  // Home Page that updates the Bus Stops Page based on the inputted
                                                                  // bus stop of the user.
    }

    // Initializes the dropdown element with each "BusStop" object.
    void PopulateDropdown()
    {
        dropdown.ClearOptions(); // Removes default options before adding bus stops to the dropdown list.

        // Populates the dropdown options and dictionary with each bus stop by mapping them to each successive index of "busStopsDict".
        List<string> options = new List<string>(); // Creates a list of strings to hold the options of the dropdown menu.
        foreach (var stop in dataLoader.busData.busStops)
        {
            options.Add(stop.name); // Adds each bus stop's "name" variable to the list of strings "options" to be added to the dropdown menu.
            busStopsDict[options.Count - 1] = stop; // Maps each bus stop to successive indexes of "busStopsDict" (starting at 0).
        }
        dropdown.AddOptions(options); // Adds each bus stop as an option of the dropdown menu ("AddOptions()" takes a "List<string>" as its parameter).
    }

    // Takes the index of the selected options from the dropdown and updates all text elements.
    void UpdateBusStopInfo(int index)
    {
        if (busStopsDict.TryGetValue(index, out BusStop selectedStop)) // If the index currently selected is within "busStopsDict":
        {
            // Updates all TextMeshPro elements with bus stop information.
            busStopNameText.text = $"Stop Name: {selectedStop.name}";
            busStopIDText.text = $"Stop ID: {selectedStop.stopID}";
            busStopAddressText.text = $"Address: {selectedStop.address}";
        }
    }

    // Takes the inputted string bus stop name from the search bar and matches it with an index in "busStopsDict".
    void SearchAndSelectBusStop()
    {
        string searchQuery = searchBar.text.Trim().ToLower();

        // Searches for the index of the inputted bus stop.
        int index = -1;
        foreach (var stop in busStopsDict)
        {
            if (stop.Value.name.ToLower() == searchQuery) // If the name of a bus stop in "busStopsDict"
                                                          // matches the inputted string name in the search bar:
            {
                index = stop.Key; // Sets "index" equal to the key (index) of the matching stop name.
                break;
            }
        }

        if (index != -1) // If a matching bus stop was found from the inputted name:
        {
            dropdown.value = index; // Sets the dropdown option to the index of the found bus stop.
            UpdateBusStopInfo(index); // Updates the text elements to the found bus stop.
            homePage.SetActive(false); busStopsPage.SetActive(true); // Switches to the Bus Stops page after search is complete.
        }
    }
}

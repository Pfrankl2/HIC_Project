// Script for implementing general bus destination search bar that takes the user to the bus stop
// they search for from other pages.

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BusStopSearch : MonoBehaviour
{
    public BusDataLoader dataLoader;
    public RectTransform mapRectTransform;
    public RectTransform viewportRectTransform;
    public TMP_InputField searchInputField;
    public Button searchButton;

    void Start()
    {
        searchButton.onClick.AddListener(SearchAndNavigate); // Assigns "SearchAndNavigate()" as a listener when "searchButton" is clicked.
    }

    // Searches for the bus stop input as a string of text by the user.
    void SearchAndNavigate()
    {
        string searchQuery = searchInputField.text.Trim().ToLower(); // Creates a string based on the input of the user in the search bar element (Turns to lower case).

        BusStop foundStop = null; // Initializes a "BusStop" object to store the inputted stop.
        foreach (var stop in dataLoader.busData.busStops)
        {
            if (stop.name.ToLower() == searchQuery)
            {
                foundStop = stop; // Sets "foundStop" BusStop object to bus stop with the inputted string name.
                break;
            }
        }

        if (foundStop != null)
        {
            Debug.Log($"Bus Stop {foundStop.name} located!");
        }
        else
        {
            Debug.Log("Bus stop not found!");
        }
    }
}

// Script for searching a Bus Stop by name and showing its location
// on the map!

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusStopSearch : MonoBehaviour
{
    public BusDataLoader dataLoader;
    public RectTransform mapRectTransform;
    public RectTransform viewportRectTransform;
    public TMP_InputField searchInputField;
    public Button searchButton;

    void Start()
    {
        searchButton.onClick.AddListener(SearchAndNavigate); // Calls "SearchAndNAvigate" when the search button is pressed.
    }

    // Searches "busData.json" for the inputted name of a bus stop a user has entered.
    void SearchAndNavigate()
    {
        string searchQuery = searchInputField.text.Trim().ToLower();

        // Searches each bus stop in "busData.json" to see if any of their names match "seachQuery".
        BusStop foundStop = null;
        foreach (var stop in dataLoader.busData.busStops)
        {
            if (stop.name.ToLower() == searchQuery) // If the inputted name matches a bus stop's name:
            {
                foundStop = stop;
                break;
            }
        }

        if (foundStop != null)
        {
            //NavigateToBusStop(foundStop);
            Debug.Log($"Stop Name: {foundStop.name} | Stop Address: {foundStop.address}");
        }
        else
        {
            Debug.Log("Bus stop not found!");
        }
    }

    /*
    void NavigateToBusStop(BusStop stop)
    {
        // Move the map to center the found bus stop in the viewport
        Vector2 stopPosition = new Vector2(stop.x, stop.y);

        // Calculate the new map position to center the stop in the viewport
        Vector2 viewportSize = viewportRectTransform.rect.size;
        Vector2 mapSize = mapRectTransform.rect.size;

        // Clamp the map position so it doesn't go out of bounds
        float clampedX = Mathf.Clamp(-stopPosition.x + viewportSize.x / 2, -(mapSize.x - viewportSize.x), 0);
        float clampedY = Mathf.Clamp(-stopPosition.y + viewportSize.y / 2, -(mapSize.y - viewportSize.y), 0);

        mapRectTransform.anchoredPosition = new Vector2(clampedX, clampedY);

        Debug.Log($"Navigated to bus stop: {stop.name}");
    }
    */
}

// Script for laoding JSON data and parsing it into the approproate classes
// to be used in other scripts.
//
// Attach this script (BusDataLoader.cs) to a GameObject to then use it for
// obtaining bus information from a JSON file to then use in other scripts!

using System.IO;
using UnityEngine;

// Loads and Parses through JSON files.
public class BusDataLoader : MonoBehaviour
{
    public BusData busData; // Stores "BusStop" and "BusRoute" objects to hold all general bus information.

    public void LoadBusData(string fileName = "busData.json")
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName); // Looks for "busData.Json" in Assets/StreamingAssets.

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            busData = JsonUtility.FromJson<BusData>(jsonData); // Predefined Json function in Unity to parse busData.Json.
            Debug.Log("Bus data loaded successfully.");
        }
        else
        {
            Debug.LogError("Bus data file not found at " + filePath);
        }
    }
}

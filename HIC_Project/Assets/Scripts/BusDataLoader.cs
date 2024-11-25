// Script for loading .json data and parsing it into the approproate classes
// to be used in other scripts.
//
// Attach this script (BusDataLoader.cs) to an Empty GameObject to use it for
// obtaining bus information from a .json file. This data can then be referenced
// and used in other scripts!

using System.IO;
using UnityEngine;

public class BusDataLoader : MonoBehaviour
{
    public BusData busData; // Stores arrays of "BusStop" and "BusRoute" objects to hold all general bus information.

    public void LoadBusData()
    {
        string fileName = "busData.json"; // Defines the .json file to look for.
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName); // Looks for "busData.json" in Assets/StreamingAssets.

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            busData = JsonUtility.FromJson<BusData>(jsonData); // Predefined .json function in Unity to parse busData.json.
            Debug.Log("Bus data loaded successfully.");
        }
        else
        {
            Debug.LogError("Bus data file not found at " + filePath);
        }
    }
}

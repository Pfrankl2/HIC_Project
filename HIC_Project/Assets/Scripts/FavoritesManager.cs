using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FavoritesManager : MonoBehaviour
{
    // References to UI elements
    public GameObject editModePanel;
    public GameObject viewModePanel;

    public Transform editModeContentParent;
    public Transform viewModeContentParent;

    public GameObject favoriteItemEditPrefab;
    public GameObject favoriteItemViewPrefab;

    public Button saveButton;
    public Button editButton;

    public BusDataLoader dataLoader; // Reference to BusDataLoader


    private List<int> favoriteStopIDs = new List<int>(); // List to store favorite bus stop IDs
    private Dictionary<int, Toggle> stopToggles = new Dictionary<int, Toggle>(); // For toggles in Edit Mode

    void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        editButton.onClick.AddListener(OnEditButtonClicked);
        ShowViewMode();
    }

    void PopulateEditModeList()
    {
        foreach (Transform child in editModeContentParent)
        {
            Destroy(child.gameObject);
        }

        stopToggles.Clear();

        var busStops = dataLoader.busData.busStops;

        foreach (var stop in busStops)
        {
            GameObject item = Instantiate(favoriteItemEditPrefab, editModeContentParent);

            Toggle toggle = item.transform.Find("Toggle").GetComponent<Toggle>();
            TMP_Text nameText = item.transform.Find("NameText").GetComponent<TMP_Text>();

            nameText.text = stop.name;
            toggle.isOn = favoriteStopIDs.Contains(stop.stopID);

            stopToggles[stop.stopID] = toggle;
        }
    }

    void OnSaveButtonClicked()
    {
        favoriteStopIDs.Clear();

        foreach (var kvp in stopToggles)
        {
            int stopID = kvp.Key;
            Toggle toggle = kvp.Value;

            if (toggle.isOn)
            {
                favoriteStopIDs.Add(stopID);
            }
        }

        ShowViewMode();
    }

    void OnEditButtonClicked()
    {
        ShowEditMode();
    }

    void ShowViewMode()
    {
        editModePanel.SetActive(false);
        viewModePanel.SetActive(true);
        PopulateViewModeList();
    }

    void ShowEditMode()
    {
        editModePanel.SetActive(true);
        viewModePanel.SetActive(false);
        PopulateEditModeList();
    }

    void PopulateViewModeList()
    {
        foreach (Transform child in viewModeContentParent)
        {
            Destroy(child.gameObject);
        }

        var busStops = dataLoader.busData.busStops;
        Dictionary<int, BusStop> busStopDict = new Dictionary<int, BusStop>();
        foreach (var stop in busStops)
        {
            busStopDict[stop.stopID] = stop;
        }
        foreach (int stopID in favoriteStopIDs)
        {
            if (busStopDict.TryGetValue(stopID, out BusStop stop))
            {
                GameObject item = Instantiate(favoriteItemViewPrefab, viewModeContentParent);

                TMP_Text nameText = item.transform.Find("NameText").GetComponent<TMP_Text>();
                nameText.text = stop.name;
            }
        }
    }
}

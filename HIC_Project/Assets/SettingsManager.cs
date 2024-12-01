using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    // UI Elements
    public TMP_InputField firstNameInputField;
    public TMP_InputField lastNameInputField;
    public TMP_Dropdown homeStopDropdown;
    public Toggle darkModeToggle;
    public Toggle showAccessibleRoutesToggle;
    public Button saveButton;

    // Reference to DataManager's BusDataLoader
    public BusDataLoader dataLoader;

    // Dark Mode elements to change color
    [SerializeField]
    private Graphic[] darkModeElements; // Assign UI elements in the Inspector
    [SerializeField]
    private Graphic[] darkModeBackgrounds; 
    [SerializeField]
    private TMP_Text[] darkModeTextElements;

    private UserSettings userSettings;
    private string settingsFilePath;

    void Start()
    {
        // Initialize settings file path
        settingsFilePath = Path.Combine(Application.streamingAssetsPath, "userSettings.json");

        // Load user settings from JSON
        LoadUserSettings();

        // Populate UI fields with loaded settings
        PopulateUI();

        // Add listeners
        saveButton.onClick.AddListener(SaveUserSettings);
        darkModeToggle.onValueChanged.AddListener(delegate { ApplyDarkMode(); });
    }

    void LoadUserSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            userSettings = JsonUtility.FromJson<UserSettings>(json);
            Debug.Log("User settings loaded.");
        }
        else
        {
            // Create default settings if file doesn't exist
            userSettings = new UserSettings();
            Debug.Log("User settings file not found. Using default settings.");
        }
    }

    void PopulateUI()
    {
        firstNameInputField.text = userSettings.firstName;
        lastNameInputField.text = userSettings.lastName;
        darkModeToggle.isOn = userSettings.darkMode;
        showAccessibleRoutesToggle.isOn = userSettings.showAccessibleRoutes;

        // Populate Home Stop dropdown
        PopulateHomeStopDropdown();

        // Set selected home stop
        SetHomeStopDropdownValue();

        // Apply Dark Mode settings
        ApplyDarkMode();
    }

    void PopulateHomeStopDropdown()
    {
        homeStopDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();

        foreach (var stop in dataLoader.busData.busStops)
        {
            options.Add(stop.name);
        }

        homeStopDropdown.AddOptions(options);
    }

    void SetHomeStopDropdownValue()
    {
        int index = 0;
        for (int i = 0; i < dataLoader.busData.busStops.Length; i++)
        {
            if (dataLoader.busData.busStops[i].stopID == userSettings.homeStop)
            {
                index = i;
                break;
            }
        }
        homeStopDropdown.value = index;
    }

    public void SaveUserSettings()
    {
        // Update userSettings with current UI values
        userSettings.firstName = firstNameInputField.text;
        userSettings.lastName = lastNameInputField.text;
        userSettings.darkMode = darkModeToggle.isOn;
        userSettings.showAccessibleRoutes = showAccessibleRoutesToggle.isOn;
        userSettings.homeStop = dataLoader.busData.busStops[homeStopDropdown.value].stopID;

        // Serialize to JSON and save
        string json = JsonUtility.ToJson(userSettings, true);
        File.WriteAllText(settingsFilePath, json);
        Debug.Log("User settings saved.");

        // Apply Dark Mode settings
        ApplyDarkMode();
    }

    void ApplyDarkMode()
    {
        Color color = userSettings.darkMode ? new Color32(60, 60, 60, 120) : new Color32(231, 209, 162, 255);

        // element/internal box color
        foreach (Graphic element in darkModeElements)
        {
            element.color = color;
        }
        color = userSettings.darkMode ? new Color32(0, 0, 0, 255) : new Color32(231, 209, 162, 255);
        
        // bg color
        foreach (Graphic element in darkModeBackgrounds)
        {
            element.color = color;
        }
        
        // textColor
        color = userSettings.darkMode ? new Color32(255, 255, 255, 255) : new Color32(0, 0, 0, 255);
        foreach (TMP_Text textElement in darkModeTextElements)
        {
            textElement.color = color;
        }
    }

    public void onDarkModeClick()
    {
        userSettings.darkMode = !userSettings.darkMode;
        ApplyDarkMode();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MyUI;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Data.Common;
using System.IO;
using System.Net.Http.Headers;
//using Assets.Plugins.SimpleFileBrowser;
using SimpleFileBrowser;



namespace Assets.Scripts.DrugsMod
{
    [System.Serializable]
    public class RobotData
    {
        public string name;         // The robot's name (e.g., "Robot-A")
        public string hwid;         // Hardware ID
        public string uniq;         // Unique identifier
        public string hash;         // Hash value
        public string id;           // ID
        public bool isLoggedIn;     // Login status

        public RobotData(string name, string hwid, string uniq, string hash, string id, bool isLoggedIn)
        {
            this.name = name;
            this.hwid = hwid;
            this.uniq = uniq;
            this.hash = hash;
            this.id = id;
            this.isLoggedIn = isLoggedIn;
        }
        public RobotData(){
          this.isLoggedIn = false;
          this.hwid = DMRegistryFunctionality.GenerateUniqueHwid();
        }
    }  
  public class DMRegistryFunctionality{
    public static void AddRobot(string robotName, string hwid, string uniq, string hash, string id, bool isLoggedIn)
    {
        // Ensure the name starts with "Robot-"
        string fullName = robotName.StartsWith("Robot-") ? robotName : $"Robot-{robotName}";
        
        if (string.IsNullOrEmpty(fullName))
        {
            UnityEngine.Debug.LogError("Robot name cannot be empty.");
            return;
        }

        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (robotList.Contains(fullName))
        {
            UnityEngine.Debug.LogWarning($"Robot '{fullName}' already exists. Use OverrideRobot to update.");
            return;
        }

        // Add to the robot list
        robotList = string.IsNullOrEmpty(robotList) ? fullName : $"{robotList},{fullName}";
        PlayerPrefs.SetString("RobotList", robotList);

        // Store the robot's data
        SetRobotData(fullName, hwid, uniq, hash, id, isLoggedIn);
        PlayerPrefs.Save();
    }

    // Remove a robot entry
    public static void RemoveRobot(string robotName)
    {
        string fullName = robotName.StartsWith("Robot-") ? robotName : $"Robot-{robotName}";
        
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (!robotList.Contains(fullName))
        {
            UnityEngine.Debug.LogWarning($"Robot '{fullName}' does not exist.");
            return;
        }

        // Remove from the robot list
        string[] robots = robotList.Split(',');
        List<string> newList = new List<string>(robots);
        newList.Remove(fullName);
        PlayerPrefs.SetString("RobotList", string.Join(",", newList));

        // Delete all associated data
        PlayerPrefs.DeleteKey($"{fullName}_hwid");
        PlayerPrefs.DeleteKey($"{fullName}_uniq");
        PlayerPrefs.DeleteKey($"{fullName}_hash");
        PlayerPrefs.DeleteKey($"{fullName}_id");
        PlayerPrefs.DeleteKey($"{fullName}_isLoggedIn");
        PlayerPrefs.Save();
    }

    // Override (update) an existing robot entry
    public static void OverrideRobot(string robotName, string hwid, string uniq, string hash, string id, bool isLoggedIn)
    {
        string fullName = robotName.StartsWith("Robot-") ? robotName : $"Robot-{robotName}";
        
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (!robotList.Contains(fullName))
        {
            UnityEngine.Debug.LogWarning($"Robot '{fullName}' does not exist. Use AddRobot to create.");
            return;
        }

        // Update the robot's data
        SetRobotData(fullName, hwid, uniq, hash, id, isLoggedIn);
        PlayerPrefs.Save();
    }

    // Get a single robot's data
    public static RobotData GetRobot(string robotName)
    {
        string fullName = robotName.StartsWith("Robot-") ? robotName : $"Robot-{robotName}";
        
        if (!PlayerPrefs.HasKey($"{fullName}_hwid"))
        {
            UnityEngine.Debug.LogWarning($"Robot '{fullName}' does not exist.");
            return null;
        }

        string hwid = PlayerPrefs.GetString($"{fullName}_hwid", "");
        string uniq = PlayerPrefs.GetString($"{fullName}_uniq", "");
        string hash = PlayerPrefs.GetString($"{fullName}_hash", "");
        string id = PlayerPrefs.GetString($"{fullName}_id", "");
        int isLoggedInInt = PlayerPrefs.GetInt($"{fullName}_isLoggedIn", 0);
        bool isLoggedIn = isLoggedInInt == 1;

        return new RobotData(fullName, hwid, uniq, hash, id, isLoggedIn);
    }

    // Get all robot entries
    public static List<RobotData> GetAllRobots()
    {
        List<RobotData> robots = new List<RobotData>();
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (string.IsNullOrEmpty(robotList))
        {
            return robots; // Return empty list if no robots exist
        }

        string[] robotNames = robotList.Split(',');
        foreach (string robotName in robotNames)
        {
            RobotData robot = GetRobot(robotName);
            if (robot != null)
            {
                robots.Add(robot);
            }
        }
        return robots;
    }

    // Helper method to set robot data in PlayerPrefs
    private static void SetRobotData(string robotName, string hwid, string uniq, string hash, string id, bool isLoggedIn)
    {
        PlayerPrefs.SetString($"{robotName}_hwid", hwid);
        PlayerPrefs.SetString($"{robotName}_uniq", uniq);
        PlayerPrefs.SetString($"{robotName}_hash", hash);
        PlayerPrefs.SetString($"{robotName}_id", id);
        PlayerPrefs.SetInt($"{robotName}_isLoggedIn", isLoggedIn ? 1 : 0);
    }
  

    /// <summary>
    /// Generates a unique hardware ID-like string that doesn’t repeat in PlayerPrefs.
    /// </summary>
    /// <returns>A unique hardware ID-like string.</returns>
    public static string GenerateUniqueHwid()
    {
        const int maxAttempts = 10; // Safety limit for regeneration attempts
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Generate a new GUID as the hardware ID
            string hwid = System.Guid.NewGuid().ToString();
            
            // Check if this hwid is already used in PlayerPrefs
            if (!IsHwidAlreadyUsed(hwid))
            {
                return hwid; // Return the unique hwid
            }
            // If a duplicate is found, loop continues to generate a new one
        }
        // If max attempts reached (extremely unlikely with GUIDs), throw an exception
        throw new System.Exception("Failed to generate a unique HWID after multiple attempts.");
    }

    /// <summary>
    /// Checks if a given hardware ID already exists in PlayerPrefs.
    /// </summary>
    /// <param name="hwid">The hardware ID to check.</param>
    /// <returns>True if the hwid is already used, false otherwise.</returns>
    private static bool IsHwidAlreadyUsed(string hwid)
    {
        // Get the list of all robot names from PlayerPrefs
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (string.IsNullOrEmpty(robotList))
        {
            return false; // No robots exist, so no duplicates
        }

        // Split the comma-separated list into individual robot names
        string[] robots = robotList.Split(',');
        foreach (string robotName in robots)
        {
            // Retrieve the hwid for this robot
            string existingHwid = PlayerPrefs.GetString($"{robotName}_hwid", "");
            if (existingHwid == hwid)
            {
                return true; // Found a duplicate
            }
        }
        return false; // No duplicates found
    }

    // Export a Single Robot
    public static string ExportRobot(string robotName)
    {
        // Normalize robot name
        string fullName = robotName.StartsWith("Robot-") ? robotName : $"Robot-{robotName}";

        // Check if the robot exists
        if (!PlayerPrefs.HasKey($"{fullName}_hwid"))
        {
            UnityEngine.Debug.LogError($"Robot '{fullName}' does not exist. Cannot export.");
            return null;
        }

        // Retrieve robot data
        RobotData robot = GetRobot(fullName);
        if (robot == null)
        {
            UnityEngine.Debug.LogError($"Failed to retrieve data for robot '{fullName}'. Export aborted.");
            return null;
        }

        
        // Serialize to JSON
        string json = JsonUtility.ToJson(robot, true); // Pretty print for readability

        // Write to file with error handling
        return json;
    }

    // Import One or Multiple Robots
    public static void ImportRobots(string filePath, bool overrideExisting = false)
    {
        // Validate file path and existence
        if (string.IsNullOrEmpty(filePath))
        {
            UnityEngine.Debug.LogError("Import file path cannot be empty.");
            return;
        }

        if (!File.Exists(filePath))
        {
            UnityEngine.Debug.LogError($"Import file not found at: {filePath}");
            return;
        }

        // Read and parse the file
        try
        {
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(json))
            {
                UnityEngine.Debug.LogError($"Import file at {filePath} is empty.");
                return;
            }

            // Determine if JSON is an array (multiple robots) or a single object
            json = json.TrimStart();
            if (json.StartsWith("["))
            {
                // Multiple robots
                List<RobotData> robots = JsonUtility.FromJson<List<RobotData>>(json);
                if (robots == null || robots.Count == 0)
                {
                    UnityEngine.Debug.LogError($"No valid robot data found in {filePath}.");
                    return;
                }

                UnityEngine.Debug.Log($"Importing {robots.Count} robots from {filePath}");
                foreach (var robot in robots)
                {
                    ImportSingleRobot(robot, overrideExisting);
                }
            }
            else
            {
                // Single robot
                RobotData robot = JsonUtility.FromJson<RobotData>(json);
                if (robot == null || string.IsNullOrEmpty(robot.name))
                {
                    UnityEngine.Debug.LogError($"Invalid robot data in {filePath}.");
                    return;
                }

                UnityEngine.Debug.Log($"Importing 1 robot from {filePath}");
                ImportSingleRobot(robot, overrideExisting);
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError($"Failed to import robots from {filePath}: {e.Message}");
        }
    }

    // Helper Method: Import a Single Robot
    public static void ImportSingleRobot(RobotData robot, bool overrideExisting)
    {
        // Validate robot data
        if (robot == null || string.IsNullOrEmpty(robot.name))
        {
            UnityEngine.Debug.LogError("Cannot import robot: Invalid or missing robot data.");
            return;
        }

        string fullName = robot.name;

        // Check if the robot already exists
        if (PlayerPrefs.HasKey($"{fullName}_hwid"))
        {
            if (overrideExisting)
            {
                UnityEngine.Debug.Log($"Overriding existing robot '{fullName}'");
                OverrideRobot(fullName, robot.hwid, robot.uniq, robot.hash, robot.id, robot.isLoggedIn);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Robot '{fullName}' already exists. Skipping import.");
            }
        }
        else
        {
            UnityEngine.Debug.Log($"Adding new robot '{fullName}'");
            AddRobot(fullName, robot.hwid, robot.uniq, robot.hash, robot.id, robot.isLoggedIn);
        }
    }

    public static void ImportRobotData()
    {
        FileBrowser.SetFilters(true, new string[] { ".json" });
        FileBrowser.ShowLoadDialog((paths) => {
            if (paths.Count() == 1)
            {
                string path = paths[0];
                try
                {
                    RobotData currentRobotData;
                    string jsonContent = FileBrowserHelpers.ReadTextFromFile(path);
                    currentRobotData = JsonUtility.FromJson<RobotData>(jsonContent);
                    if (currentRobotData == null)
                    {
                        Debug.LogError("Failed to deserialize RobotData from " + path);
                    }
                    else
                    {
                        //use the importsingle robot
                        DMRegistryFunctionality.ImportSingleRobot(currentRobotData,true);
                        DMPopupManager.THIS.PrepareLoginPage();
                        Debug.Log("Robot data imported from " + path);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Error importing robot data: " + e.Message);
                }
            }
        }, null, FileBrowser.PickMode.Files, false);
    }

    public static void ExportRobotData(RobotData currentRobotData)
    {
        if (currentRobotData == null)
        {
            Debug.Log("No robot data to export.");
            return;
        }
        string jsonContent = JsonUtility.ToJson(currentRobotData);
        FileBrowser.SetFilters(true, new string[] { ".json" });
        FileBrowser.ShowSaveDialog((path) => {
            if (!string.IsNullOrWhiteSpace(path[0]))
            {
                try
                {
                    FileBrowserHelpers.WriteTextToFile(path[0], jsonContent);
                    Debug.Log("Robot data exported to " + path);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error exporting robot data: " + e.Message);
                }
            }
        }, null, FileBrowser.PickMode.Files, false, null, "robot_data.json", "Save Robot Data", "Save");
    }

  }
}
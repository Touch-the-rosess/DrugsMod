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


#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX //|| UNITY_EDITOR
using System.Windows.Forms; // Requires System.Windows.Forms.dll for Windows
using System.Runtime.InteropServices; // For macOS native calls
#endif
#if UNITY_STANDALONE_LINUX || UNITY_EDITOR_LINUX || UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX || UNITY_ANDROID
using System.Diagnostics;
#endif

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

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private static void SaveJsonFileWindows(string defaultFileName, string jsonContent)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            FileName = defaultFileName,
            Filter = "JSON files (*.json)|*.json",
            Title = "Save JSON File"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(saveFileDialog.FileName, jsonContent);
            UnityEngine.Debug.Log("JSON file saved to: " + saveFileDialog.FileName);
        }
        else
        {
            UnityEngine.Debug.Log("Save file dialog canceled.");
        }
    }
#endif

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    private static void SaveJsonFileMacOS(string defaultFileName, string jsonContent)
    {
        string tempPath = Path.Combine(Application.temporaryCachePath, defaultFileName);
        File.WriteAllText(tempPath, jsonContent);

        string appleScript = $"tell app \"System Events\" to set savePath to POSIX path of (choose file name with prompt \"Save JSON File\" default name \"{defaultFileName}\")" +
                             $"\nset tempPath to POSIX path of \"{tempPath}\"" +
                             $"\ndo shell script \"mv \" & quoted form of tempPath & \" \" & quoted form of savePath";

        try
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "osascript",
                    Arguments = $"-e '{appleScript}'",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            UnityEngine.Debug.Log("JSON file saved via macOS dialog.");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Failed to save JSON file on macOS: " + e.Message);
            string fallbackPath = Path.Combine(Application.persistentDataPath, defaultFileName);
            File.WriteAllText(fallbackPath, jsonContent);
            UnityEngine.Debug.Log("Fallback JSON save to: " + fallbackPath);
        }
    }
#endif

#if UNITY_STANDALONE_LINUX || UNITY_EDITOR_LINUX
    private static void SaveJsonFileLinux(string defaultFileName, string jsonContent)
    {
        try
        {
            // Use zenity for a simple file save dialog on Linux
            string tempPath = Path.Combine(Application.temporaryCachePath, defaultFileName);
            File.WriteAllText(tempPath, jsonContent);

            System.Diagnostics.Process process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "zenity",
                    Arguments = $"--file-selection --save --filename=\"{defaultFileName}\" --file-filter=\"JSON files (*.json) | *.json\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode == 0 && !string.IsNullOrEmpty(output))
            {
                string savePath = output.Trim();
                File.Move(tempPath, savePath);
                UnityEngine.Debug.Log("JSON file saved to: " + savePath);
            }
            else
            {
                UnityEngine.Debug.Log("Save file dialog canceled on Linux.");
                File.Delete(tempPath);
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Failed to save JSON file on Linux: " + e.Message);
            // Fallback to persistentDataPath
            string fallbackPath = Path.Combine(Application.persistentDataPath, defaultFileName);
            File.WriteAllText(fallbackPath, jsonContent);
            UnityEngine.Debug.Log("Fallback JSON save to: " + fallbackPath);
        }
    }
#endif

#if UNITY_ANDROID || UNITY_IOS
    private static void SaveJsonFileMobile(string defaultFileName, string jsonContent)
    {
        string path = Path.Combine(Application.persistentDataPath, defaultFileName);
        File.WriteAllText(path, jsonContent);
        UnityEngine.Debug.Log("JSON file saved to: " + path);
    }
#endif

// Main method to call the appropriate platform-specific save function
public static void SaveJsonFile(string defaultFileName, string jsonContent)
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    SaveJsonFileWindows(defaultFileName, jsonContent);
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    SaveJsonFileMacOS(defaultFileName, jsonContent);
#elif UNITY_STANDALONE_LINUX || UNITY_EDITOR_LINUX
     SaveJsonFileLinux(defaultFileName, jsonContent);
#elif UNITY_ANDROID || UNITY_IOS
    SaveJsonFileMobile(defaultFileName, jsonContent);
#else
    // Default fallback for unsupported platforms
    string fallbackPath = Path.Combine(Application.persistentDataPath, defaultFileName);
    File.WriteAllText(fallbackPath, jsonContent);
    UnityEngine.Debug.Log("JSON file saved to fallback path: " + fallbackPath);
#endif
    UnityEngine.Debug.Log("DmRegistryFunctionality.SaveJsonFile()");
}

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private static RobotData LoadJsonFileWindows(string title = "Select Robot Data JSON File")
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json",
            Title = title
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFileDialog.FileName;
            return LoadAndValidateJson(filePath);
        }
        else
        {
            UnityEngine.Debug.Log("File open dialog canceled.");
            return null;
        }
    }
#endif

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    private static RobotData LoadJsonFileMacOS(string title = "Select Robot Data JSON File")
    {
        try
        {
            string appleScript = $"tell app \"System Events\" to set filePath to POSIX path of (choose file with prompt \"{title}\" of type {{\"json\"}})" +
                                 $"\nreturn filePath";

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "osascript",
                    Arguments = $"-e '{appleScript}'",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string filePath = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            if (process.ExitCode == 0 && !string.IsNullOrEmpty(filePath))
            {
                return LoadAndValidateJson(filePath);
            }
            else
            {
                UnityEngine.Debug.Log("File open dialog canceled on macOS.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Failed to load JSON file on macOS: " + e.Message);
            return null;
        }
    }
#endif

#if UNITY_STANDALONE_LINUX || UNITY_EDITOR_LINUX
    private static RobotData LoadJsonFileLinux(string title = "Select Robot Data JSON File")
    {
        try
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "zenity",
                    Arguments = $"--file-selection --title=\"{title}\" --file-filter=\"JSON files (*.json) | *.json\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string filePath = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            if (process.ExitCode == 0 && !string.IsNullOrEmpty(filePath))
            {
                return LoadAndValidateJson(filePath);
            }
            else
            {
                UnityEngine.Debug.Log("File open dialog canceled on Linux.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Failed to load JSON file on Linux: " + e.Message);
            return null;
        }
    }
#endif

#if UNITY_ANDROID
    private static RobotData LoadJsonFileAndroid(string title = "Select Robot Data JSON File")
    {
        string defaultPath = Path.Combine(Application.persistentDataPath, "robot_data.json");
        if (File.Exists(defaultPath))
        {
            return LoadAndValidateJson(defaultPath);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No default JSON file found at: " + defaultPath + ". Use a plugin like 'Native File Picker' for full file selection.");
            return null;
        }
    }
#endif

    // Helper method to load and validate JSON
    private static RobotData LoadAndValidateJson(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                UnityEngine.Debug.LogError("File does not exist: " + filePath);
                return null;
            }

            string jsonContent = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(jsonContent))
            {
                UnityEngine.Debug.LogError("JSON file is empty: " + filePath);
                return null;
            }

            RobotData robotData = JsonUtility.FromJson<RobotData>(jsonContent);
            if (robotData == null)
            {
                UnityEngine.Debug.LogError("Failed to deserialize JSON to RobotData: " + filePath);
                return null;
            }

            // Safety checks for required fields
            if (string.IsNullOrEmpty(robotData.name))
            {
                UnityEngine.Debug.LogWarning("RobotData 'name' is missing or empty. Setting default.");
                return null;
            }
            if (string.IsNullOrEmpty(robotData.hwid))
            {
                UnityEngine.Debug.LogWarning("RobotData 'hwid' is missing or empty. Generating new HWID.");
                return null;
            }
            if (string.IsNullOrEmpty(robotData.uniq))
            {
                UnityEngine.Debug.LogWarning("RobotData 'uniq' is missing or empty. Setting default.");
                return null;
            }
            if (string.IsNullOrEmpty(robotData.hash))
            {
                UnityEngine.Debug.LogWarning("RobotData 'hash' is missing or empty. Setting default.");
                return null;
            }
            if (string.IsNullOrEmpty(robotData.id))
            {
                UnityEngine.Debug.LogWarning("RobotData 'id' is missing or empty. Setting default.");
                return null;
            }

            UnityEngine.Debug.Log("RobotData loaded successfully from: " + filePath);
            return robotData;
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Error loading or validating JSON file: " + e.Message);
            return null;
        }
    }

    // Main method to call the appropriate platform-specific load function
    public static RobotData LoadRobotData(string title = "Select Robot Data JSON File")
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        return LoadJsonFileWindows(title);
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        return LoadJsonFileMacOS(title);
#elif UNITY_STANDALONE_LINUX || UNITY_EDITOR_LINUX
        return LoadJsonFileLinux(title);
#elif UNITY_ANDROID
        return LoadJsonFileAndroid(title);
#else
        UnityEngine.Debug.LogWarning("File loading not implemented for this platform. Using fallback.");
        string fallbackPath = Path.Combine(Application.persistentDataPath, "robot_data.json");
        if (File.Exists(fallbackPath))
        {
            return LoadAndValidateJson(fallbackPath);
        }
        return null;
#endif
    }

  }
}
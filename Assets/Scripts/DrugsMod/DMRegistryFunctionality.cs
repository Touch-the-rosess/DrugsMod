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



namespace Assets.Scripts.DrugsMod
{
 public class RobotData
 {
     public string Name { get; set; }
     public string Hwid { get; set; }
     public string Hash { get; set; }
     public string Id { get; set; }
     public bool IsLoggedIn { get; set; }
 } 
  public class DMRegistryFunctionality{
    public static void AddRobot(string robotName, string hwid, string hash, string id, bool isLoggedIn)
    {
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (robotList.Contains(robotName))
        {
            Debug.LogWarning($"Robot '{robotName}' already exists.");
            return;
        }

        // Add to robot list
        robotList = string.IsNullOrEmpty(robotList) ? robotName : $"{robotList},{robotName}";
        PlayerPrefs.SetString("RobotList", robotList);

        // Set properties
        string prefix = $"Robot-{robotName}";
        PlayerPrefs.SetString($"{prefix}_hwid", hwid);
        PlayerPrefs.SetString($"{prefix}_hash", hash);
        PlayerPrefs.SetString($"{prefix}_id", id);
        PlayerPrefs.SetInt($"{prefix}_isLoggedIn", isLoggedIn ? 1 : 0);

        PlayerPrefs.Save();
    }
    public static void RemoveRobot(string robotName)
    {
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (!robotList.Contains(robotName))
        {
            Debug.LogWarning($"Robot '{robotName}' does not exist.");
            return;
        }

        // Remove from robot list
        string[] robots = robotList.Split(',');
        robotList = string.Join(",", robots.Where(r => r != robotName));
        PlayerPrefs.SetString("RobotList", robotList);

        // Delete properties
        string prefix = $"Robot-{robotName}";
        PlayerPrefs.DeleteKey($"{prefix}_hwid");
        PlayerPrefs.DeleteKey($"{prefix}_hash");
        PlayerPrefs.DeleteKey($"{prefix}_id");
        PlayerPrefs.DeleteKey($"{prefix}_isLoggedIn");

        PlayerPrefs.Save();
    }
    public static void UpdateRobot(string robotName, string hwid, string hash, string id, bool isLoggedIn)
    {
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (!robotList.Contains(robotName))
        {
            Debug.LogWarning($"Robot '{robotName}' does not exist.");
            return;
        }

        // Update properties
        string prefix = $"Robot-{robotName}";
        PlayerPrefs.SetString($"{prefix}_hwid", hwid);
        PlayerPrefs.SetString($"{prefix}_hash", hash);
        PlayerPrefs.SetString($"{prefix}_id", id);
        PlayerPrefs.SetInt($"{prefix}_isLoggedIn", isLoggedIn ? 1 : 0);

        PlayerPrefs.Save();
    }
    public static RobotData GetRobot(string robotName)
    {
        string prefix = $"Robot-{robotName}";
        if (!PlayerPrefs.HasKey($"{prefix}_hwid"))
        {
            Debug.LogWarning($"Robot '{robotName}' does not exist.");
            return null;
        }

        return new RobotData
        {
            Name = robotName,
            Hwid = PlayerPrefs.GetString($"{prefix}_hwid"),
            Hash = PlayerPrefs.GetString($"{prefix}_hash"),
            Id = PlayerPrefs.GetString($"{prefix}_id"),
            IsLoggedIn = PlayerPrefs.GetInt($"{prefix}_isLoggedIn") == 1
        };
    }
    public static List<RobotData> GetAllRobots()
    {
        string robotList = PlayerPrefs.GetString("RobotList", "");
        if (string.IsNullOrEmpty(robotList))
        {
            return new List<RobotData>();
        }

        string[] robots = robotList.Split(',');
        List<RobotData> robotDataList = new List<RobotData>();

        foreach (string robotName in robots)
        {
            RobotData data = GetRobot(robotName);
            if (data != null)
            {
                robotDataList.Add(data);
            }
        }

        return robotDataList;
    }
  }
}
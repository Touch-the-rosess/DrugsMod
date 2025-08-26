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



namespace Assets.Scripts.DrugsMod
{
  public static class DMGlobalVariables{
    public static RobotData currentLoggedRobot;
    public static bool IsSigningInNewRobot  = false;
        public static bool GunRadius_First  = true;
        public static bool GunRadius_Second = true;
        public static bool GunRadius_Third  = true;
  } 
}
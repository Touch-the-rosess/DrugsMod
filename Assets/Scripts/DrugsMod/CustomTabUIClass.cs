using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DrugsMod
{
  class CustomTabUIClass
  {
    public string tabName;

    public bool isActive;
    public CustomTabUIClass(string name, bool active = false)
    {
      tabName = name;
      isActive = active;
    }
    public void SetTabAsClosed(){
      isActive = false;
    }
    public void SetTabAsOpened(){
      isActive = true;
    }
    
  }
}
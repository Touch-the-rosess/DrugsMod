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
    class DMPopupManager : MonoBehaviour
    {
        private void Start() {
            UnityEngine.Debug.Log("Lol");
            DMPopupManager.THIS = this;
            this.tabsArray = new CustomTabUIClass[] {
              new CustomTabUIClass("Main",true),
              new CustomTabUIClass("Жывки"),
              new CustomTabUIClass("Third"),
              new CustomTabUIClass("About")
            };
            this.ConstructTabsVisually();
            this.exitButton.onClick.AddListener(new UnityAction(this.OnExit));
        }
        public void Show(){
          if(!this.ModGUIWindow.activeSelf){
            UnityEngine.Debug.Log($"The state of this.ModGUIWindow.activeSelf = {this.ModGUIWindow.activeSelf}"); 
            this.ModGUIWindow.SetActive(true);
            ConstructTabsVisually();
          }
          
        }
        private void OnExit()
        {
          UnityEngine.Debug.Log("Exit button pressed!"); 
          this.ModGUIWindow.SetActive(false);
          GUIManager.THIS.ClearFocus();
        }

        private void CommonButtonListener(string buttonType, int tabId){
          UnityEngine.Debug.Log($"The buttontype \"{buttonType}\" with next id {tabId} was pressed!");
          if(buttonType == "tab"){
            for(int i = 0; i < tabsArray.Length; i++){
              tabsArray[i].SetTabAsClosed();
              if(i == tabId){
                tabsArray[i].SetTabAsOpened();
              }
            }
            ConstructTabsVisually();
            UpdateLayout();
          }
          
        }
        private void UpdateLayout()
        {
          //LayoutRebuilder.ForceRebuildLayoutImmediate(this.buttonRow.GetComponent<RectTransform>());
          LayoutRebuilder.ForceRebuildLayoutImmediate(this.tabsRow.GetComponent<RectTransform>());
          LayoutRebuilder.ForceRebuildLayoutImmediate(this.ModGUIWindow.GetComponent<RectTransform>());
        }
        public void ConstructTabsVisually(){
          //UnityEngine.Debug.Log($"There are {this.tabsRow.transform.childCount} childrens.");
          foreach (object obj in this.tabsRow.transform)
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
          
          for(int i = 0; i < tabsArray.Length; i++){
          //foreach (var element in tabsArray){
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(tabsArray[i].isActive?this.openedTabPrefab:this.closedTabPrefab);
            gameObject.transform.SetParent(this.tabsRow.transform, worldPositionStays: false);
            gameObject.GetComponentInChildren<Text>().text = tabsArray[i].tabName;
            if(!tabsArray[i].isActive){
              //UnityEngine.Debug.Log($"Tab id when adding listener to buttons {i}");
              int temp = i;
              gameObject.GetComponent<Button>().onClick.AddListener(delegate (){
                this.CommonButtonListener("tab", temp);
              });
            }
            //UnityEngine.Debug.Log($"Created a tab?");
          }
          
          UnityEngine.Debug.Log($"tabsArray[0] = {tabsArray[0].isActive}\ntabsArray[1] = {tabsArray[1].isActive}\ntabsArray[2] = {tabsArray[2].isActive}\ntabsArray[3] = {tabsArray[3].isActive}\n");
        }
        public CustomTabUIClass[] tabsArray;
        public GameObject ModGUIWindow;
        public GameObject tabsRow;
        public Button exitButton;
        public GameObject openedTabPrefab;
        public GameObject closedTabPrefab;
        public static DMPopupManager THIS;
    }
}

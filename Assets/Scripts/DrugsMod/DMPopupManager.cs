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
        public void PrepareLoginPage(){
          //set the scroll view?
          Vector2 sizeDelta = this.scrollView.GetComponent<RectTransform>().sizeDelta;
          sizeDelta.y = 400f;
          this.scrollView.GetComponent<RectTransform>().sizeDelta = sizeDelta;
          //GameObject gameObject24 = this.listContent;
          foreach (object obj7 in this.listContent.transform)
          {
              UnityEngine.Object.Destroy(((Transform)obj7).gameObject);
          }
          this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 5f;
          //the list content is the one holding the lines
          //get the folders from reg
          this.scrollView.SetActive(true);

          foreach(var robot in DMRegistryFunctionality.GetAllRobots())
          {
            GameObject gameObject23 = UnityEngine.Object.Instantiate<GameObject>(this.buttonLinePrefab);
            gameObject23.transform.SetParent(this.listContent.transform, false);
            gameObject23.GetComponentsInChildren<Text>()[0].text = robot.Name;
            gameObject23.GetComponentsInChildren<Text>()[1].text = "Log in";
            
            if (robot.IsLoggedIn)
            {
                gameObject23.GetComponentInChildren<Button>().gameObject.SetActive(false);
            }
            else
            {
                gameObject23.GetComponentInChildren<Button>().onClick.AddListener(delegate (){ LogInRobot(robot); });
            }
          }

        }

        public void LogInRobot(RobotData robot){
            DMRegistryFunctionality.UpdateRobot(robot.Name,robot.Hwid, robot.Hash,robot.Id, robot.IsLoggedIn);
            // now find the logic for logging in the bot
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

        private void CommonButtonListener(string buttonType, int tabId){ // remane it for tabs, like tabs button listener
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
        public GameObject scrollView;
        public GameObject buttonLinePrefab;
        public GameObject listContent; // its for stcrollviewa
        public GameObject[] pages; // there ill assign the pages that would be shown wen pressing on tabs
        public static DMPopupManager THIS;
    }
}

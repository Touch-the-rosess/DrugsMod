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
using Assets.Scripts.DrugsMod;
// using UnityEditor.Build.Player;
//using UnityEngine.UIElements;


namespace Assets.Scripts.DrugsMod
{
    class DMPopupManager : MonoBehaviour
    {
        private void Start() {
            DMPopupManager.THIS = this;
            //DMRegistryFunctionality.AddRobot("test1","hwid1","uniq1","hash1","1",false);
            //DMRegistryFunctionality.AddRobot("test2","hwid2","uniq2","hash2","1",false);
            //DMRegistryFunctionality.AddRobot("test3","hwid3","uniq3","hash3","1",false);
            //DMRegistryFunctionality.AddRobot("test4","hwid4","uniq4","hash4","1",false);
            //DMRegistryFunctionality.AddRobot("test5","hwid5","uniq5","hash5","1",false);

            this.tabsArray = new CustomTabUIClass[] {
              new CustomTabUIClass("First"),
              new CustomTabUIClass("Second"),
              new CustomTabUIClass("Third"),
              new CustomTabUIClass("About",true)
            };
            //this.ConstructTabsVisually();
            this.PrepareLoginPage();
            this.newAccountButton.onClick.AddListener(delegate () { SignInRobot(); });
            this.exitButton.onClick.AddListener(new UnityAction(this.OnExit));
            this.AddListenerToGunRadiusCheckboxes();
            //TabPagesPair = new Dictionary<GameObject, CustomTabUIClass>();

        }
        public void AddListenerToGunRadiusCheckboxes() {
            gunRadiusToggleCheckboxes[0].onValueChanged.AddListener((isOn) => { 
                ClientConfig.gunRadius = isOn; 
                Debug.Log($"ClientConfig.gunRadius: {isOn}");
                gunRadiusToggleCheckboxes[1].interactable = isOn;
                gunRadiusToggleCheckboxes[2].interactable = isOn;
                gunRadiusToggleCheckboxes[3].interactable = isOn;
            });
            gunRadiusToggleCheckboxes[1].onValueChanged.AddListener((isOn) => { DMGlobalVariables.GunRadius_First  = isOn;  Debug.Log($"DMGlobalVariables.GunRadius_First : {isOn}"); });
            gunRadiusToggleCheckboxes[2].onValueChanged.AddListener((isOn) => { DMGlobalVariables.GunRadius_Second = isOn;  Debug.Log($"DMGlobalVariables.GunRadius_Second: {isOn}"); });
            gunRadiusToggleCheckboxes[3].onValueChanged.AddListener((isOn) => { DMGlobalVariables.GunRadius_Third  = isOn;  Debug.Log($"DMGlobalVariables.GunRadius_Third : {isOn}"); });
            //gameObject.GetComponent<Button>().onClick.AddListener(delegate () {
            //    this.CommonButtonListener("tab", temp);
            //});
        }
        public void PrepareLoginPage(){
          Vector2 sizeDelta = this.scrollView.GetComponent<RectTransform>().sizeDelta;
          sizeDelta.y = 300f;
          this.scrollView.GetComponent<RectTransform>().sizeDelta = sizeDelta;
          foreach (object obj7 in this.listContent.transform){
              UnityEngine.Object.Destroy(((Transform)obj7).gameObject);
          }
          this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 30f;
          //the list content is the one holding the lines
          //get the folders from reg
          this.scrollView.SetActive(true);
          this.loginPageButtonRow.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate (){SignInRobot(); }); //new account
          this.loginPageButtonRow.GetComponentsInChildren<Button>()[1].onClick.AddListener(async delegate (){
            DMRegistryFunctionality.ImportRobotData();
            //PrepareLoginPage();
          }); //import account
          //DMRegistryFunctionality.ImportSingleRobot(import,true);
          //RobotData import =  DMRegistryFunctionality.LoadRobotData();
          foreach(var robot in DMRegistryFunctionality.GetAllRobots())
          {
            AddNewLogInButtonLine(robot);
          }

        }
        private void AddNewLogInButtonLine(RobotData robot){
            GameObject buttonLineElement = UnityEngine.Object.Instantiate<GameObject>(this.logInButtonLinePrefab);
            //UnityEngine.Debug.Log($"DMPopupManager.AddNewLogInButtonLine() = ",buttonLineElement.GetComponentsInChildren<Button>()[1]);
            //buttonLineElement.transform.SetParent(this.listContent.transform, false);
            buttonLineElement.GetComponentsInChildren<Text>()[0].text = robot.name;
            buttonLineElement.GetComponentsInChildren<Text>()[1].text = "Log in";
            
            buttonLineElement.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate (){ 
              UnityEngine.Debug.Log("DMPopupManager.AddNewLogInButtonLine() Export button pressed");
              DMRegistryFunctionality.ExportRobotData(robot);
            }); //export
            
            UnityEngine.Debug.Log("DMPopupManager.AddNewLogInButtonLine() ", buttonLineElement.GetComponentsInChildren<Button>()[1]);
            buttonLineElement.GetComponentsInChildren<Button>()[2].onClick.AddListener(delegate (){ 
              UnityEngine.Debug.Log("DMPopupManager.AddNewLogInButtonLine() Delete button pressed");
              DMRegistryFunctionality.RemoveRobot(robot.name); 
              buttonLineElement.SetActive(false);
            }); //delete
            
            if (robot.isLoggedIn)
            {
                //buttonLineElement.GetComponentInChildren<Button>().gameObject.SetActive(false); // TODO: Make the button disabled, not invisible
                buttonLineElement.GetComponentsInChildren<Button>()[1].interactable = false; 
            }
            else
            {
                buttonLineElement.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate (){ LogInRobot(robot); });
            }
            buttonLineElement.transform.SetParent(this.listContent.transform);
        }
        public void SignInRobot(){
          DMGlobalVariables.currentLoggedRobot = new RobotData();
          DMGlobalVariables.currentLoggedRobot.isLoggedIn = true;
          DMGlobalVariables.IsSigningInNewRobot = true;
          ConnectionManager.THIS.FirstConnect();
          ModGUIWindow.SetActive(false);
        }
        public void LogInRobot(RobotData robot){
          DMGlobalVariables.currentLoggedRobot = robot;
          robot.isLoggedIn = true;
          UnityEngine.Debug.Log("DMPopupManager.LogInRobot() got invoked.");
          DMRegistryFunctionality.OverrideRobot(robot.name,robot.hwid,robot.uniq, robot.hash,robot.id, robot.isLoggedIn);
          ConnectionManager.THIS.FirstConnect();
          this.ModGUIWindow.SetActive(false);
        }
        public void Show(){
          if(!this.ModGUIWindow.activeSelf){
            UnityEngine.Debug.Log($"The state of this.ModGUIWindow.activeSelf = {this.ModGUIWindow.activeSelf}"); 
            this.ConstructTabsVisually();
            this.tabsRow.SetActive(true);
            this.pages[0].SetActive(false);//hide login page
            this.ModGUIWindow.SetActive(true);
            //if(this.signInButton.activeSelf)this.signInButton.SetActive(false);
            if(this.loginPageButtonRow.activeSelf)this.loginPageButtonRow.SetActive(false);
            ShowPage(pages.Length-1);//showing the last page that would allways be about page
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
                ShowPage(i+1);
              }
            }
            ConstructTabsVisually();
            UpdateLayout();
          }
          
        }
        public void ShowPage(int index){
          for(int i = 0; i < pages.Length; i++){
            pages[i].SetActive((i==index)?true:false);
            //if(i == index) pages[i].SetActive(true);
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
          //show tabs
          this.tabsRow.SetActive(true);
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
        public GameObject loginPageButtonRow;
        public Button exitButton;
        public Button newAccountButton;
        public GameObject scrollView;
        public GameObject listContent; // its for stcrollviewa
        public GameObject[] pages; // there ill assign the pages that would be shown wen pressing on tabs
        public Toggle[] gunRadiusToggleCheckboxes; 
        public GameObject openedTabPrefab;
        public GameObject closedTabPrefab;
        public GameObject logInButtonLinePrefab;
        public static DMPopupManager THIS;

        //public Dictionary<GameObject, CustomTabUIClass> TabPagesPair;//openWith =new Dictionary<string, string>();

    }
}

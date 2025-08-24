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

            this.PrepareLoginPage();
            this.newAccountButton.onClick.AddListener(delegate () { SignInRobot(); });
            this.exitButton.onClick.AddListener(new UnityAction(this.OnExit));
            this.AddListenerToGunRadiusCheckboxes();
            this.SetupTabsToggle();
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
          foreach (object obj in this.listContent.transform){
              UnityEngine.Object.Destroy(((Transform)obj).gameObject);
          }
          this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 30f;
          //the list content is the one holding the lines
          //get the folders from reg
          this.scrollView.SetActive(true);
          this.tabsRow.SetActive(false);
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
          this.LastEnteredRobotText.text = "Last Account: " + PlayerPrefs.GetString("LastRobot");
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
          PlayerPrefs.SetString("LastRobot", robot.name);
          this.ModGUIWindow.SetActive(false);
          this.loginPage.SetActive(false);//hide login page
          this.tabsRow.SetActive(true);
          if (this.loginPageButtonRow.activeSelf) this.loginPageButtonRow.SetActive(false);

        }
        public void Show(){
          if(!this.ModGUIWindow.activeSelf){
            this.ModGUIWindow.SetActive(true);
          }
        }
        private void OnExit()
        {
          UnityEngine.Debug.Log("Exit button pressed!"); 
          this.ModGUIWindow.SetActive(false);
          GUIManager.THIS.ClearFocus();
        }
        private void SetupTabsToggle(){
            for (int i = 0; i < tabs.Length; i++)
            {
                Debug.Log($"Addind add listener on value change for tab with index {i}");
                int temp = i;
                tabs[i].onValueChanged.AddListener(isOn => {
                    Debug.Log($"The tab with index {temp} got toggled");
                    if (isOn) ShowAndHidePages(temp);
                    tabs[temp].GetComponent<Image>().sprite = (isOn? TabOpenedImage:TabClosedImage);
                });
            }
        }
        public void ShowAndHidePages(int index){
          for (int i = 0; i < pages.Length; i++){
             pages[i].SetActive((i==index));
          }
            
        }
        private void UpdateLayout()
        {
          //LayoutRebuilder.ForceRebuildLayoutImmediate(this.buttonRow.GetComponent<RectTransform>());
          LayoutRebuilder.ForceRebuildLayoutImmediate(this.tabsRow.GetComponent<RectTransform>());
          LayoutRebuilder.ForceRebuildLayoutImmediate(this.ModGUIWindow.GetComponent<RectTransform>());
        }


        public GameObject ModGUIWindow;
        public GameObject tabsRow;
        public GameObject loginPageButtonRow;
        public Button exitButton;
        public Button newAccountButton;
        public GameObject scrollView;
        public GameObject listContent; // its for stcrollviewa
        public Toggle[] tabs;  // there ill assign the tabs that would be prebaked
        public GameObject[] pages; // there ill assign the pages that would be shown wen pressing on tabs
        public GameObject loginPage;
        
        public Toggle[] gunRadiusToggleCheckboxes; 
        public GameObject openedTabPrefab;
        public GameObject closedTabPrefab;
        public GameObject logInButtonLinePrefab;
        public Text LastEnteredRobotText;
        public static DMPopupManager THIS;

        public Sprite TabOpenedImage;
        public Sprite TabClosedImage;

        //public Dictionary<GameObject, CustomTabUIClass> TabPagesPair;//openWith =new Dictionary<string, string>();

    }
}

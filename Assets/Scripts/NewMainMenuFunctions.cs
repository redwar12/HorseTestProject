using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewMainMenuFunctions : MonoBehaviour
{
    //TODO: Not needed for MVP, but this script really needs to be cleaned up and made more readable, focus was on functionality not readability.
    [Header("Menu Objects")]
    public GameObject initialMenu;
    public GameObject GuidedMode_Menu1;
    public GameObject GuidedMode_Menu2;
    public GameObject AssessmentMode_Menu1;
    public GameObject XrayDropdown;
    public GameObject BodyPartDropdown;
    public GameObject ConfirmationScreen;
    
    [Header("Training Setup Data for debugging only, *Do not set in inspector*")]
    [SerializeField] private TrainingMode trainingMode;
    [SerializeField] private TaskType taskType;
    [SerializeField] private XrayType xrayType;
    [SerializeField] private BodyPart bodyPart;

    private TrainingSetupData trainingSetupData;
    
    private List<XrayType> xrayTypes = new List<XrayType>();
    private List<BodyPart> bodyParts = new List<BodyPart>();
    private List<string> bodyPartOptions;
    private List<string> xrayTypeOptions;
    
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialMenu.SetActive(true);
        GuidedMode_Menu1.SetActive(false);
        GuidedMode_Menu2.SetActive(false);
        AssessmentMode_Menu1.SetActive(false);
        XrayDropdown.SetActive(false);
        BodyPartDropdown.SetActive(false);
        
        // Set the default training setup data
        trainingSetupData = new TrainingSetupData
        {
            trainingMode = TrainingMode.None,
            xrayType = XrayType.None,
            bodyPart = BodyPart.None
        };
        
        // Fill Lists
        bodyPartOptions = new List<string>();
        xrayTypeOptions = new List<string>();
        
        // Fill body part options
        bodyPartOptions.Add(BodyPart.Fetlock_Front_L.ToString());
        bodyPartOptions.Add(BodyPart.Fetlock_Front_R.ToString());
        bodyPartOptions.Add(BodyPart.Fetlock_Hind_L.ToString());
        bodyPartOptions.Add(BodyPart.Fetlock_Hind_R.ToString());
        
        // Fill xray type options
        xrayTypeOptions.Add(XrayType.DP_DPI.ToString());
        xrayTypeOptions.Add(XrayType.LM.ToString());
        xrayTypeOptions.Add(XrayType.DLPMO.ToString());
        xrayTypeOptions.Add(XrayType.DMPLO.ToString());
        
        // fill xray types
        xrayTypes.Add(XrayType.DP_DPI);
        xrayTypes.Add(XrayType.LM);
        xrayTypes.Add(XrayType.DLPMO);
        xrayTypes.Add(XrayType.DMPLO);
        // fill body parts
        bodyParts.Add(BodyPart.Fetlock_Front_L);
        bodyParts.Add(BodyPart.Fetlock_Front_R);
        bodyParts.Add(BodyPart.Fetlock_Hind_L);
        bodyParts.Add(BodyPart.Fetlock_Hind_R);
    }
    
    public void OnGuidedModeButtonPressed()
    {
        initialMenu.SetActive(false);
        GuidedMode_Menu1.SetActive(true);
        trainingMode = TrainingMode.GuidedMode;
        Debug.Log("Guided Mode button pressed");
    }

    public void OnAssessmentButtonPressed()
    {
        initialMenu.SetActive(false);
        AssessmentMode_Menu1.SetActive(true);
        trainingMode = TrainingMode.AssessmentMode;
        Debug.Log("Assessment Mode button pressed");
    }

    public void OnTaskSelection(int selection)
    {
        switch (selection)
        {
            case 0:
                taskType = TaskType.Emitter;
                break;
            case 1:
                taskType = TaskType.Plate;
                break;
            case 2:
                taskType = TaskType.Both;
                break;
            default:
                taskType = TaskType.None;
                break;
        }
        
        Debug.Log("Task Type selected: " + taskType);
        
        GuidedMode_Menu1.SetActive(false);
        var taskSelectedText = GuidedMode_Menu2.transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        GuidedMode_Menu2.SetActive(true);
        taskSelectedText.text = "Task Type: " + taskType;
    }
    
    public void OnXrayDropdownButtonPressed()
    {
        XrayDropdown.SetActive(!XrayDropdown.activeSelf);
        Debug.Log("Xray dropdown button pressed");
    }
    
    public void OnBodyPartDropdownButtonPressed()
    {
        BodyPartDropdown.SetActive(!BodyPartDropdown.activeSelf);
        Debug.Log("Body Part Dropdown button pressed");
    }
    
    public void OnXrayTypeSelected(int selection)
    {
        switch (selection)
        {
            case 0:
                xrayType = XrayType.DP_DPI;
                break;
            case 1:
                xrayType = XrayType.LM;
                break;
            case 2:
                xrayType = XrayType.DLPMO;
                break;
            case 3:
                xrayType = XrayType.DMPLO;
                break;
            default:
                xrayType = XrayType.None;
                break;
        }
        
        Debug.Log("X-ray Type selected: " + xrayType);
        XrayDropdown.SetActive(false);
        var xrayTypeSelectedText = XrayDropdown.transform.parent.GetChild(0).GetComponentInChildren<TMP_Text>();
        xrayTypeSelectedText.text = "X-ray Type: " + xrayType;
        // if body part has been selected display confirmation screen with data
        if (bodyPart != BodyPart.None)
        {
            SaveScenarioData();
            CheckScenarioData();
            ShowConfirmationScreen();
        }
    }
    
    public void OnBodyPartSelected(int selection)
    {
        switch (selection)
        {
            case 0:
                bodyPart = BodyPart.Fetlock_Front_L;
                break;
            case 1:
                bodyPart = BodyPart.Fetlock_Front_R;
                break;
            case 2:
                bodyPart = BodyPart.Fetlock_Hind_L;
                break;
            case 3:
                bodyPart = BodyPart.Fetlock_Hind_R;
                break;
            default:
                bodyPart = BodyPart.None;
                break;
        }
        
        BodyPartDropdown.SetActive(false);
        var bodyPartSelectedText = BodyPartDropdown.transform.parent.GetChild(0).GetComponentInChildren<TMP_Text>();
        bodyPartSelectedText.text = "Body Part: " + bodyPart;
        Debug.Log("Body Part selected: " + trainingSetupData.bodyPart);
        
        // if x-ray type has been selected display confirmation screen with data
        
        if (xrayType != XrayType.None)
        {
            SaveScenarioData();
            CheckScenarioData();
            ShowConfirmationScreen();
        }
    }

    
    private void SaveScenarioData()
    {
        trainingSetupData.trainingMode = trainingMode;
        trainingSetupData.xrayType = xrayType;
        trainingSetupData.bodyPart = bodyPart;
        
        Debug.Log("Training Setup Data saved: " + trainingSetupData.trainingMode + ", " + trainingSetupData.xrayType + ", " + trainingSetupData.bodyPart);
    }
    
    private void CheckScenarioData()
    {
        // check if any data is missing or set to default/none
        if (trainingSetupData.trainingMode == TrainingMode.None)
        {
            Debug.LogWarning("Training Mode is not set");
        }
        
        if (trainingSetupData.xrayType == XrayType.None)
        {
            Debug.LogWarning("X-ray Type is not set");
        }
        
        if (trainingSetupData.bodyPart == BodyPart.None)
        {
            Debug.LogWarning("Body Part is not set");
        }
        
        if (trainingSetupData.trainingMode != TrainingMode.None && trainingSetupData.xrayType != XrayType.None && trainingSetupData.bodyPart != BodyPart.None)
        {
            Debug.Log("All data is set");
        }
    }
    
    private void ShowConfirmationScreen()
    {
        AssessmentMode_Menu1.SetActive(false);
        GuidedMode_Menu2.SetActive(false);
        var selectRoleText = ConfirmationScreen.transform.GetChild(3).GetComponentInChildren<TMP_Text>();
        var xrayTypeText = ConfirmationScreen.transform.GetChild(4).GetComponentInChildren<TMP_Text>();
        var bodyPartText = ConfirmationScreen.transform.GetChild(5).GetComponentInChildren<TMP_Text>();
        
        bodyPartText.text = "Body Part: " + trainingSetupData.bodyPart;
        xrayTypeText.text = "X-ray Type: " + trainingSetupData.xrayType;
        selectRoleText.text = "Training Mode: " + trainingSetupData.trainingMode;
        ConfirmationScreen.SetActive(true);
    }

    public void ResetSelections()
    {
        // Set all data to none
        trainingSetupData.trainingMode = TrainingMode.None;
        trainingSetupData.xrayType = XrayType.None;
        trainingSetupData.bodyPart = BodyPart.None;
        taskType = TaskType.None;
        xrayType = XrayType.None;
        bodyPart = BodyPart.None;
        Debug.Log("All data has been reset");
        
        var bodyPartSelectedText = BodyPartDropdown.transform.parent.GetChild(0).GetComponentInChildren<TMP_Text>();
        var xrayTypeSelectedText = XrayDropdown.transform.parent.GetChild(0).GetComponentInChildren<TMP_Text>();
        bodyPartSelectedText.text = "Select Body Part";
        xrayTypeSelectedText.text = "Select Xray Type";
        var selectRoleText = ConfirmationScreen.transform.GetChild(3).GetComponentInChildren<TMP_Text>();
        var taskSelectedText = GuidedMode_Menu2.transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        taskSelectedText.text = "Select Task Type";
        selectRoleText.text = "Select Training Mode";
        // Return to starting screen
        initialMenu.SetActive(true);
        ConfirmationScreen.SetActive(false);
    }
    
    public void LoadMainScene()
    {
        // Load the main scene
        Debug.Log("Loading main scene");
        //TODO: ADD TRANSITION OF TRAINING SETUP DATA TO THE NEXT SCENE FOR SETUP
        SceneManager.LoadScene(1);
    }

    public void AssessmentModeSetup(int taskID)
    {
        switch (taskID)
        {
            case 0:
                // Emitter task
                Debug.Log("Emitter Task Assessment Setup initiated");
                taskType = TaskType.Emitter;
                RandomiseTypeAndPart();
                break;
            case 1:
                // Plate task
                Debug.Log("Plate Task Assessment Setup initiated");
                taskType = TaskType.Plate;
                RandomiseTypeAndPart();
                break;
            case 2:
                // Both task
                Debug.Log("Both Task Assessment Setup initiated");
                taskType = TaskType.Both;
                RandomiseTypeAndPart();
                break;
            default:
                Debug.LogWarning("Task ID not found");
                break;
        }
        
        // Set the training setup data
        trainingMode = TrainingMode.AssessmentMode;
        
        SaveScenarioData();
        CheckScenarioData();
        ShowConfirmationScreen();
    }

    private void RandomiseTypeAndPart()
    {
        // Randomise the x-ray type and body part
        int randomXrayType = Random.Range(0, xrayTypeOptions.Count);
        int randomBodyPart = Random.Range(0, bodyPartOptions.Count);
        
        xrayType = xrayTypes[randomXrayType];
        bodyPart = bodyParts[randomBodyPart];
    }
}

public enum TaskType
{
    None,
    Emitter,
    Plate,
    Both
}


/// Enums for the training setup
public enum TrainingMode
{
    None,
    GuidedMode,
    AssessmentMode
}

public enum XrayType
{
    None,
    DP_DPI,
    LM,
    DLPMO,
    DMPLO
}

public enum BodyPart
{
    None,
    Fetlock_Front_L,
    Fetlock_Front_R,
    Fetlock_Hind_L,
    Fetlock_Hind_R
    
}

/// Struct to hold the training setup data 
internal struct TrainingSetupData
{
    public TrainingMode trainingMode;
    public XrayType xrayType;
    public BodyPart bodyPart;
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public GameObject MainMenuScreen;
    public GameObject PlayerRoleSelectScreen;
    public GameObject ScenarioSelectScreen;
    public GameObject ScenarioConfirmationScreen;
    private ScenarioData scenarioData;
    
    public void Start()
    {
        //Screens set to active so the text can be set
        MainMenuScreen.SetActive(true);
        PlayerRoleSelectScreen.SetActive(true);
        ScenarioSelectScreen.SetActive(true);
        
        scenarioData = ScenarioData.ScenarioDataInstance;
        Debug.Log(scenarioData + " has been set as the ScenarioData instance");
        
        // Set menu texts
        var playerRoleButtonParent = PlayerRoleSelectScreen.transform.GetChild(1);
        var scenarioButtonParent = ScenarioSelectScreen.transform.GetChild(1);
        
        for (var i = 0; i < playerRoleButtonParent.childCount; i++)
        {
            // if no roles remaining, disable button gameobject
            if (i >= scenarioData.playerRoles.Count)
            {
                Debug.Log("No more player roles remaining , disabling button");
                playerRoleButtonParent.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            else
            {
                Debug.Log(scenarioData.playerRoles[i].ToString() + " is a player role");
                playerRoleButtonParent.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = scenarioData.playerRoles[i].ToString();
            }
            PlayerRoleSelectScreen.SetActive(false);
        }
        
        for (var i = 0; i < scenarioButtonParent.childCount; i++)
        {
            // if no scenarios remaining, disable button gameobject
            if (i >= scenarioData.scenarioTypes.Count)
            {
                Debug.Log("No more scenarios remaining , disabling button");
                scenarioButtonParent.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            else
            {
                Debug.Log("Scenario Type: " + scenarioData.scenarioTypes[i].ToString());
                if (scenarioData.scenarioTypes[i].ToString().Contains("Default"))
                {
                    scenarioButtonParent.GetChild(i).gameObject.SetActive(false);
                    continue;
                }
                scenarioButtonParent.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = scenarioData.scenarioTypes[i].ToString();
            }
            ScenarioSelectScreen.SetActive(false);
        }
    }
    

    public void ButtonPressed(string type)
    {
        switch (type)
        {
            // Main Menu Buttons
            case "QuickStart":
                Debug.LogWarning("Quick Start has not yet been implemented");
                break;
            case "ScenarioSelect":
                MainMenuScreen.SetActive(false);
                PlayerRoleSelectScreen.SetActive(true);
                break;
            case "Settings":
                Debug.LogWarning("Settings has not yet been implemented");
                break;
            case "Quit":
                Debug.LogWarning("Quit has not yet been implemented");
                break;
            
            // Player Role Select Buttons
            case "HorseHandler":
                Debug.LogWarning("Horse Handler has not yet been implemented");
                break;
            case "PlateHandler":
                scenarioData.SetPlayerRole(ScenarioData.PlayerRole.Plater);
                PlayerRoleSelectScreen.SetActive(false);
                ScenarioSelectScreen.SetActive(true);
                break;
            case "Observer":
                Debug.LogWarning("Observer has not yet been implemented");
                break;
            
            // Scenario Select Buttons
            case "Fetlock":
                scenarioData.SetScenarioType(ScenarioData.ScenarioType.Fetlock);
                ScenarioSelectScreen.SetActive(false);
                ScenarioConfirmationScreen.SetActive(true);
                ScenarioConfirmationInitiated();
                break;
            case "Foot":
                Debug.LogWarning("Foot has not yet been implemented");
                break;
            case "Carpus":
                Debug.LogWarning("Carpus has not yet been implemented");
                break;
            case "Tarsus":
                Debug.LogWarning("Tarsus has not yet been implemented");
                break;
            case "Stifle":
                Debug.LogWarning("Stifle has not yet been implemented");
                break;
            case "Head":
                Debug.LogWarning("Head has not yet been implemented");
                break;
            
            //Back Buttons
            case "ScenarioBack":
                ScenarioSelectScreen.SetActive(false);
                PlayerRoleSelectScreen.SetActive(true);
                break;
            case "PlayerRoleBack":
                PlayerRoleSelectScreen.SetActive(false);
                scenarioData.ResetScenarioType();
                MainMenuScreen.SetActive(true);
                break;
            
            case "default":
                Debug.LogWarning("Default case has been reached");
                break;
        }
    }

    public void ScenarioConfirmationInitiated()
    {
        Debug.Log("Scenario Confirmation Initiated");
        ScenarioConfirmationScreen.SetActive(true);
        var scenarioText = "Selected Scenario: " + scenarioData.GetScenarioType();
        var playerRoleText = "Selected Player Role: " + scenarioData.GetPlayerRole();
        ScenarioConfirmationScreen.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = scenarioText;
        ScenarioConfirmationScreen.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = playerRoleText;
        
    }
   
    
    public void ScenarioConfirmationButtonPressed(string type)
    {
        switch (type)
        {
            case "Confirm":
                // Load next scene
                SceneManager.LoadScene(1);
                Debug.Log("Scenario Data has been confirmed and the next scene has been loaded");
                break;
            case "Reset":
                Debug.Log("Scenario Data has been reset");
                ScenarioConfirmationScreen.SetActive(false);
                ScenarioSelectScreen.SetActive(true);
                scenarioData.ResetScenarioType();
                scenarioData.ResetPlayerRole();
                break;
        }
    }
}

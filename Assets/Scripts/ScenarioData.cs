using System.Collections.Generic;
using UnityEngine;

public class ScenarioData : MonoBehaviour
{
    public static ScenarioData ScenarioDataInstance;
    
    public List<ScenarioType> scenarioTypes = new List<ScenarioType>();
    public List<PlayerRole> playerRoles = new List<PlayerRole>();
    
    public ScenarioType selectedScenarioType;
    public PlayerRole selectedPlayerRole;
    
    
    private void Awake()
    {
        if (ScenarioDataInstance == null)
        {
            
            Debug.Log("This should only run once at the first start of the game");
            ScenarioDataInstance = this;


            // Scenario Types from enum to list
            scenarioTypes.Add(ScenarioType.Fetlock);
            scenarioTypes.Add(ScenarioType.Foot);
            scenarioTypes.Add(ScenarioType.Carpus);
            scenarioTypes.Add(ScenarioType.Tarsus);
            scenarioTypes.Add(ScenarioType.Stifle);
            scenarioTypes.Add(ScenarioType.Head);
            scenarioTypes.Add(ScenarioType.Default);

            // Player Roles from enum to list
            playerRoles.Add(PlayerRole.Handler);
            playerRoles.Add(PlayerRole.Plater);
            playerRoles.Add(PlayerRole.Observer);
            playerRoles.Add(PlayerRole.Default);
            Debug.Log("Scenario Types and Player Roles have been added to the lists");
            DontDestroyOnLoad(this);
        }
        
        else
        {
            Debug.LogWarning("ScenarioDataInstance already exists, destroying this instance");
            Destroy(gameObject);
        }
    }
    
    //**Scenario Getter, Setter and Resetter**//
    
    public void SetScenarioType(ScenarioType type)
    {
        Debug.Log("Scenario Type Set to: " + type);
        selectedScenarioType = type;
    }
    
    public ScenarioType GetScenarioType()
    {
        return selectedScenarioType;
    }
    
    public void ResetScenarioType() // This should be called on each game reset/return to main menu
    {
        selectedScenarioType = ScenarioType.Default; // if game is attempting to begin with this scenario type , then it should not start.
    }
    
    //**Player Role Getter, Setter and Resetter**//
    
    public void SetPlayerRole(PlayerRole role)
    {
        Debug.Log("Player Role Set to: " + role);
        selectedPlayerRole = role;
    }
    
    public PlayerRole GetPlayerRole()
    {
        return selectedPlayerRole;
    }
    
    public void ResetPlayerRole() // This should be called on each game reset/return to main menu
    {
        selectedPlayerRole = PlayerRole.Default; // if game is attempting to begin with this player role , then it should not start.
    }
    
    public enum ScenarioType
    {
        Fetlock,
        Foot,
        Carpus,
        Tarsus,
        Stifle,
        Head,
        Default
    }
    
    public enum PlayerRole
    {
        Handler,
        Plater,
        Observer,
        Default
    }
    
    
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class InteractableRotator : MonoBehaviour
{
    public XRGrabInteractable m_Interactable;
    public InputActionReference leftTurnAction;
    public InputActionReference rightTurnAction;

    private bool isSelected;
    
    void Start()
    {
        m_Interactable.selectEntered.AddListener(OnSelectEnter);
    }
    
    void OnSelectEnter(SelectEnterEventArgs arg0)
    {
        m_Interactable.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}

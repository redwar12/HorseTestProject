using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SimpleinteractableCustomSetup : MonoBehaviour
{
    [Tooltip("Called when Select Entered is triggered in XRSimpleInteractable")]
        public UnityEvent OnClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var interactable = GetComponent<XRSimpleInteractable>();
        var collider = GetComponent<Collider>();
        interactable.colliders[0] = collider;
        interactable.interactionManager = FindObjectOfType<XRInteractionManager>();
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        Debug.Log("Object clicked");
        OnClick.Invoke();
    }
}

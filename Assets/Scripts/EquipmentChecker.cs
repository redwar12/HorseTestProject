using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EquipmentChecker : MonoBehaviour
{
    [Header("Correct Rotation Thresholds (Relative to Hand)")]
    public float minXRotation;
    public float maxXRotation;
    public float minYRotation;
    public float maxYRotation;
    public float minZRotation;
    public float maxZRotation;

    [Header("XR Components")]
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    [Header("Misc.")]
    public bool DebugMode = false;

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Transform interactorTransform = args.interactorObject.transform;

        // Get object's rotation relative to the interactor (hand)
        Quaternion relativeRotation = Quaternion.Inverse(interactorTransform.rotation) * transform.rotation;
        Vector3 relativeEuler = relativeRotation.eulerAngles;

        if (DebugMode)
        {
            Debug.Log($"{gameObject.name} grabbed. Relative Rotation: {relativeEuler}");
        }

        if (IsRotationValid(relativeEuler))
        {
            Debug.Log("Correctly held!");
        }
        else
        {
            Debug.LogWarning("Incorrect grip! Object is not aligned properly.");
        }
    }

    private bool IsRotationValid(Vector3 rotation)
    {
        return rotation.x >= minXRotation && rotation.x <= maxXRotation &&
               rotation.y >= minYRotation && rotation.y <= maxYRotation &&
               rotation.z >= minZRotation && rotation.z <= maxZRotation;
    }
}
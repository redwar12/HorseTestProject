using UnityEngine;
using UnityEngine.Events;
//TODO : Clean up script to make it easier to read
// Creator : Aaron Smith
// Date : 24/3/25
// Purpose : To create a button that can be activated and deactivated by the player using an XR controller
// Usage : Attach to a game object that you want to be a button, it must have the collider set to trigger
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ButtonActivator : MonoBehaviour
{
    private bool beenPressed = false;
    public bool isActive = false;
    private bool timerActive;
    [Tooltip("Event/s to run when button is activated")] public UnityEvent onActivate;
    [Tooltip("Event/s to run when button is deactivated")]public UnityEvent onDeactivate;
    [Tooltip("Time in seconds that when activated the button will reset to isActive = false and call onDeactivate (If 0 it will not reset through the timer)")]
    public float resetTimer;
    
    private float timer = 0;
    private float pressTimer = 0;
    private float timeBetweenPresses = 2f;
    
    public Material buttonActivatedMaterial;
    public Material buttonDeactivatedMaterial;
    public MeshRenderer buttonMeshRenderer;

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        
        var col = GetComponent<Collider>();
        col.isTrigger = true;
        
        if (resetTimer <= 0)
        {
            timerActive = false;
            Debug.Log(gameObject.name + " does not have an active reset timer");
        }
        else
        {
            timerActive = true;
            Debug.Log(gameObject.name + " has an active timer");
        }
    }

    private void Update()
    {
        if (isActive && timerActive) // Only counts when the button is active and the timer is active
        {
            timer += Time.deltaTime;
            if (timer >= resetTimer)
            {
                Debug.Log(gameObject.name + " has finished it's reset timer");
                isActive = false;
                Debug.Log(gameObject.name + " has been deactivated");
                onDeactivate.Invoke();
                timer = 0;
                Debug.Log(gameObject.name + " timer has been reset to 0, ready for next activation");
                beenPressed = false;
                pressTimer = 0;
            }
        }

        if (beenPressed) // Timer for the button to reset after being pressed
        {
            pressTimer += Time.deltaTime;
            if (pressTimer >= timeBetweenPresses)
            {
                Debug.Log(gameObject.name + " has finished it's press timer");
                Debug.Log(gameObject.name + " can now be interacted with again");
                //TODO: Turn below in a function to call to make the code cleaner
                beenPressed = false;
                pressTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // Only ever called when the player's hands are in the trigger
    {
        if (other.CompareTag("PlayerHands") && !beenPressed)
        {
            Debug.Log("Player has pressed " + gameObject.name);
            isActive = !isActive;
            Debug.Log(gameObject.name + " active status is now : " + isActive);
            if (isActive)
            {
                onActivate.Invoke();
                Debug.Log(gameObject.name + " onActivate has been invoked");
                if (buttonMeshRenderer != null)
                {
                    Debug.Log(gameObject.name + " has a mesh renderer and is changing material to activated");
                    buttonMeshRenderer.material = buttonActivatedMaterial;
                }
                
                beenPressed = true;
                Debug.Log(gameObject.name + " press timer has started it can be interacted with again in " + timeBetweenPresses + " seconds");
            }
            else
            {
                onDeactivate.Invoke();
                Debug.Log(gameObject.name + " onDeactivate has been invoked");
                if (buttonMeshRenderer != null)
                {
                    Debug.Log(gameObject.name + " has a mesh renderer and is changing material to deactivated");
                    buttonMeshRenderer.material = buttonDeactivatedMaterial;
                }
                
                beenPressed = true;
                Debug.Log(gameObject.name + " press timer has started it can be interacted with again in " + timeBetweenPresses + " seconds");
            }
        }
    }
}

using UnityEngine;

public class XrayDebugs : MonoBehaviour
{
    // Public color variables for debug rays, can be set in the Inspector
    public Color dpDpiDebugColor = Color.red;
    public Color lmDebugColor = Color.green;
    public Color dlpmoDebugColor = Color.blue;
    public Color dmploDebugColor = Color.yellow;

    // Debug mode flag
    public bool debugMode = false;
    private bool debugModeActive = false;

    // Range for ray lengths
    public float range = 10f;

    // Initialize the script
    public void Start()
    {
        // Ensure colors are set, defaults provided at initialization
        AssignDefaultColorsIfNeeded();
    }

    // Update is called once per frame
    public void Update()
    {
        HandleDebugModeState();
        if (debugModeActive)
        {
            StartDebugMode(); // Draw debug rays every frame
        }
    }

    // Ensure default colors are assigned if not set
    private void AssignDefaultColorsIfNeeded()
    {
        dpDpiDebugColor = dpDpiDebugColor == default ? Color.red : dpDpiDebugColor;
        lmDebugColor = lmDebugColor == default ? Color.green : lmDebugColor;
        dlpmoDebugColor = dlpmoDebugColor == default ? Color.blue : dlpmoDebugColor;
        dmploDebugColor = dmploDebugColor == default ? Color.yellow : dmploDebugColor;
    }

    // Handle entering and exiting debug mode
    private void HandleDebugModeState()
    {
        if (debugMode && !debugModeActive)
        {
            debugModeActive = true;
            Debug.Log("Debug mode is active");
        }
        else if (!debugMode && debugModeActive)
        {
            debugModeActive = false;
            Debug.Log("Debug mode is inactive");
        }
    }

    // Draw debug rays based on current settings
    private void StartDebugMode()
    {
        Debug.Log("Drawing debug lines");

        // Draw DP/DPI front and back rays at 10 degrees
        Vector3 direction = Quaternion.AngleAxis(10, -transform.right) * transform.forward;
        Vector3 directionback = Quaternion.AngleAxis(-10, transform.right) * -transform.forward;
        Debug.DrawLine(transform.position, transform.position + direction * (range * 5), dpDpiDebugColor); // Forward 10 degrees , increased by a factor of 5 as this is for the emitter
        Debug.DrawLine(transform.position, transform.position + directionback * range, dpDpiDebugColor); // Backward -10 degrees

        // Draw LM side-to-side rays at 0 degrees
        DrawSideToSideRays(lmDebugColor);

        // Draw DLPMO front outer to back inner ray at 45 degrees
        Debug.LogWarning("DLPMO Debug not implemented yet");

        // Draw DMPLO front inner to back outer ray at -45 degrees
        Debug.LogWarning("DMPLO Debug not implemented yet");
    }

    // Draw left and right side-to-side rays
    private void DrawSideToSideRays(Color color)
    {
        Vector3 leftDirection = -transform.right;
        Vector3 rightDirection = transform.right;

        Debug.DrawLine(transform.position, transform.position + leftDirection * range, color);
        Debug.DrawLine(transform.position, transform.position + rightDirection * range, color);
    }
}

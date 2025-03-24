using System.Collections;
using UnityEngine;

public class HorseWalkDebug : MonoBehaviour
{
    private Animator animator;
    public float movementSpeed = 0.5f;
    public float animationSpeed = 0.5f;
    public GameObject horsePad;
    public Transform head;
    [SerializeField] private float timeToDoorOpen;
    private bool activated;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    

    public void StartHorseWalk()
    {
        if (activated) return;
        activated = true;
        animator.SetFloat("Speed", animationSpeed);
        animator.SetBool("isWalking", true);
        StartCoroutine(MoveHorseToDestination());
    }

    private IEnumerator MoveHorseToDestination()
    {
        var destination = new Vector3(horsePad.transform.position.x, transform.position.y,
            horsePad.transform.position.z);
        transform.LookAt(destination);
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            animator.SetFloat("Speed", animationSpeed); // <- This can be removed once we know the correct speed
            transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("isWalking", false);
    }
    
    private IEnumerator WaitAndStartHorseWalk()
    {
        yield return new WaitForSeconds(timeToDoorOpen);
        StartHorseWalk();
    }


}

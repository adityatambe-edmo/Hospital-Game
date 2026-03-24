using Unity.VisualScripting;
using UnityEngine;

// Patient Controller will change not final very bare bones

public enum LocationType
{
    None,
    Triage,
    Diagnostic,
    Ward,
    Exit
}

public class PatientController : MonoBehaviour
{
    MedicalCondition medicalCondition = null;
    // [SerializeField]
    private PatientStateMachine patientStateMachine;
    
    // [SerializeField]
    // private GameObject patientPrefab;
    [SerializeField]
    private PatientCase patientCase;
    // [SerializeField]
    // private Transform spawnPoint;
    GameManager gameManager;
    GameObject currentPatient;

    private Transform targetLocation;
    private PatientState targetState;
    private LocationType targetLocationType;
    private bool isMoving = false;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arrivalDistance = 0.1f;

    public LocationType currentLocation = LocationType.None;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        patientStateMachine = FindAnyObjectByType<PatientStateMachine>().GetComponent<PatientStateMachine>();
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();

    }

    public void Init(PatientCase caseInfo , MedicalCondition condition , GameManager gm)
    {
        patientCase = caseInfo;
        medicalCondition = condition;
        gameManager = gm;
    }

    void Update()
    {
        if (isMoving && targetLocation != null)
        {
            Vector3 direction = targetLocation.position - transform.position;
            direction.y = 0f;

            if (direction.magnitude <= arrivalDistance)
            {
                isMoving = false;
                OnReachedDestination(targetLocationType);
                return;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetLocation.position.x, transform.position.y, targetLocation.position.z),
                moveSpeed * Time.deltaTime
            );

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction.normalized);
            }
        }
    }

    public void MoveTo(Transform location, LocationType locationType, PatientState stateOnArrival)
    {
        targetLocation = location;
        targetLocationType = locationType;
        targetState = stateOnArrival;
        isMoving = true;
    }

    void OnReachedDestination(LocationType reachedLocation)
    {
        currentLocation = reachedLocation;
        Debug.Log($"Patient reached: {reachedLocation}");

        switch (reachedLocation)
        {
            case LocationType.Triage:
                break;
            case LocationType.Diagnostic:
                break;
            case LocationType.Ward:
                break;
            case LocationType.Exit:
                break;
        }

        patientStateMachine.TransitionTo(targetState);
    }

    // public void SpawnPatient()
    // {
    //     if (currentPatient != null)
    //     {
    //         Destroy(currentPatient);
    //     }
    //     currentPatient = Instantiate(patientPrefab, spawnPoint.position, spawnPoint.rotation);
    //     GotoTriage();
    // }

    // void GotoTriage()
    // {
    //     // moveToLocation(patientStateMachine.triage);
    //     currentPatient.transform.Translate(Vector3.forward * 5f);
    //     patientStateMachine.TransitionTo(PatientState.Triage);
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Triage"))
        {
            patientStateMachine.TransitionTo(PatientState.Triage);
        }
        else if (other.CompareTag("Diagnostic"))
        {
            patientStateMachine.TransitionTo(PatientState.Diagnostic);
        }
        else if (other.CompareTag("Ward"))
        {
            patientStateMachine.TransitionTo(PatientState.Ward);
        }      
    }


    // void ExitHospital()
    // {
    //     // moveToLocation(exitlocation);
    //     Destroy(currentPatient);
    // }
}

using Unity.VisualScripting;
using UnityEngine;

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
    private bool isMoving = false;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arrivalDistance = 0.1f;


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
        if (isMoving)
        {
            
        }
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

    void GotoTriage()
    {
        // moveToLocation(patientStateMachine.triage);
        currentPatient.transform.Translate(Vector3.forward * 5f);
        patientStateMachine.TransitionTo(PatientState.Triage);
    }

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

    void MoveToLocation(Transform location)
    {
        //play walking animation - movement code will change
        currentPatient.transform.Translate(location.position - currentPatient.transform.position);
    }

    void ExitHospital()
    {
        // moveToLocation(exitlocation);
        Destroy(currentPatient);
    }
}

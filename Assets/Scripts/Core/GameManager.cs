using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//game manager will handle spawning patients, and managing the state of the game. It will also handle the transitions between states, and the events that are triggered during those transitions. It will also handle the UI and the mini games.

public class GameManager : MonoBehaviour
{

    [Header("Game State Management")]
    PatientStateMachine patientStateMachine;

    [SerializeField] public UnityEvent onGameStart;
    [SerializeField] public UnityEvent onDashboardRequest;
    [SerializeField] public UnityEvent onDiagnosticRequest;
    [SerializeField] public UnityEvent onMovingToWard;
    [SerializeField] public UnityEvent onMiniGameStart;
    [SerializeField] public UnityEvent onPatientTreated;

    private static GameManager instance;

    [SerializeField] private GameObject triage;
    private GameObject ward;
    private GameObject diagnostic;
    // private GameObject patient;
    private GameObject spawnPoint;

    [Header("Patient Cases")]
    public PatientCase[] patient;
    public MedicalCondition[] medicalConditions;

    private List<MedicalCondition> availableConditions = new List<MedicalCondition>();

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patientStateMachine = GetComponent<PatientStateMachine>();
        patientStateMachine.TransitionTo(PatientState.GameStart);
    }

    public void GameStart()
    {
        Debug.Log("Game Started");
        // player spawns and walks up to the er

    }

    void fillAvailableConditionsToPatients()
    {
        availableConditions.Clear();
        availableConditions.AddRange(medicalConditions);
    }

    void assignRandomConditionToPatient(PatientCase patientCase)
    {
        if (availableConditions.Count == 0)
        {
            fillAvailableConditionsToPatients();
            Debug.Log("all conditions have been assigned, refilling available conditions");
        }
        
        int randomIndex = Random.Range(0, availableConditions.Count);

        MedicalCondition pickedCondition = availableConditions[randomIndex];
        patientCase.medicalCondition = pickedCondition;
        Debug.Log($"Assigned {pickedCondition.conditionName} to {patientCase.patientName}");

        availableConditions.RemoveAt(randomIndex); // so that each case wont be repeated
    }
}

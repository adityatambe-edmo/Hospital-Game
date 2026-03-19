using UnityEngine;

public enum PatientState
{
    GameStart,
    Triage,
    Diagnostic,
    Ward,
    MiniGame,
    Treated
}

public class PatientStateMachine : MonoBehaviour
{
    private GameManager gameManager;
    private PatientState currentState;
    private PatientCase patientCase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TransistionTo(PatientState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case PatientState.Triage:
                gameManager.onDashboardRequest.Invoke();
                break;
            case PatientState.Diagnostic:
                gameManager.onDiagnosticRequest.Invoke();
                break;
            case PatientState.Ward:
                gameManager.onMovingToWard.Invoke();
                break;
            case PatientState.MiniGame:
                gameManager.onMiniGameStart.Invoke();
                break;
            case PatientState.Treated:
                gameManager.onPatientTreated.Invoke();
                break;
        }
    }
}

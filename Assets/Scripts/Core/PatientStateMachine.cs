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
    public PatientState currentState;
    private PatientCase patientCase;
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void TransitionTo(PatientState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case PatientState.GameStart:
                gameManager.onGameStart.Invoke();
                break;
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

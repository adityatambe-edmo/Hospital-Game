using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public UnityEvent onGameStart;
    [SerializeField]
    public UnityEvent onDashboardRequest;
    [SerializeField]
    public UnityEvent onDiagnosticRequest;
    [SerializeField]
    public UnityEvent onMovingToWard;
    [SerializeField]
    public UnityEvent onMiniGameStart;
    [SerializeField]
    public UnityEvent onPatientTreated;


    private static GameManager instance;
    [SerializeField]
    private GameObject triage;
    private GameObject ward;
    private GameObject diagnostic;
    private GameObject patient;
    private GameObject spawnPoint;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnPlayer()
    {
        
        

    }
}

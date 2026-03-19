using UnityEngine;

public class PatientController : MonoBehaviour
{
    [SerializeField]
    private PatientStateMachine patientStateMachine;
    [SerializeField]
    private GameObject patientPrefab;
    [SerializeField]
    private PatientCase patientCase;
    [SerializeField]
    private Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

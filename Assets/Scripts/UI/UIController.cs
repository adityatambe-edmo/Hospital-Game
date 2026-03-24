using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject dashboardPanel;
    [SerializeField] private GameObject patientInfoPanel;
    [SerializeField] private GameObject dialoguePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDashboard()
    {
        dashboardPanel.SetActive(true);
        patientInfoPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    public void ShowPatientInfo()
    {
        dashboardPanel.SetActive(false);
        patientInfoPanel.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue()
    {
        dashboardPanel.SetActive(false);
        patientInfoPanel.SetActive(false);
        dialoguePanel.SetActive(true);
    }
}

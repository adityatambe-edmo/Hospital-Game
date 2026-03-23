using UnityEngine;

[CreateAssetMenu(fileName = "New Patient Case", menuName = "Hospital/Patient Case")]
public class PatientCase : ScriptableObject
{

    [Header("Patient Case Information")]
    public string patientName;
    public int age;
    public Sprite patientImage;

    [Header("Symptoms")]
    [TextArea(3, 10)]
    public string symptomsDescription;
    public string[] symptomsKeywords;

    // [Header("Diagnosis")]
    // public MedicalCondition correctDiagnosis; 
    // public DiagnosticTest[] relevantTests;

    [Header("Difficulty")]
    [Range(1, 5)]
    public int difficulty;

    [System.NonSerialized]
    public MedicalCondition medicalCondition;
}

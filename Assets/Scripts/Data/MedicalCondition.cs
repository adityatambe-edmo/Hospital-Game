using UnityEngine;

[CreateAssetMenu(fileName = "New Medical Condition", menuName = "Hospital/Medical Condition")]
public class MedicalCondition : ScriptableObject
{
    [Header("Medical Condition Information")]
    public string conditionName;
    [TextArea(3, 10)]
    public string conditionDescription;
    public string syllabusCode;
    public WardConfig treatedInWard;
    public string possibleSymptoms;
    public Sprite conditionImage;

    // [Header("TreatmentTests")]

}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Medical Condition", menuName = "Hospital/Medical Condition")]
public class MedicalCondition : ScriptableObject
{
    [Header("Medical Condition Information")]
    public String conditionName;
    [TextArea(3, 10)]
    public String conditionDescription;
    public String syllabusCode;
    public WardConfig treatedInWard;
    public String possibleSymptoms;
    public Sprite conditionImage;

    // [Header("TreatmentTests")]

}

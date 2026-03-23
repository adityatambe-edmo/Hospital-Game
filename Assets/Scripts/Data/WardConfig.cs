using UnityEngine;

[CreateAssetMenu(fileName = "New Ward Config", menuName = "Hospital/Ward Config")]
public class WardConfig : ScriptableObject
{
    [Header("Mini Game")]
    // Add mini game here
    public GameObject miniGamePrefab;
    [Header("Progression")]
    public int currentWardLevel = 1;
    public int cost;

    [Header("Ward Information")]

    public string wardName;
    public string floorCode;
    public int totalBeds = 4; // Example: Each ward level adds 10 beds
}

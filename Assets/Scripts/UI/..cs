using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    
    public static UI_Manager Instance;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject wardPanel;
    public GameObject miniGamePanel;


    void print(string message)
    {
        Debug.Log(message);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            print("UI_Manager instance already exists. Destroying duplicate.");
            
        }
    }

    public void showUI_Panel()
    {
        hideAll();
        
    }

    public void hideAll()
    {
        // foreach (UIPanel panel in GetComponentsInChildren<UIPanel>(true))
        // {
        //     panel.gameObject.SetActive(false);
        // }
        
    }

}

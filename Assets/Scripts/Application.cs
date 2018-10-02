using UnityEngine;

public class Application : MonoBehaviour
{
    private UIController _uiController;
    
    private void Start()
    {
        _uiController = new UIController(new Game());
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int _level;
    
    public void StartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + _level);
    }
}

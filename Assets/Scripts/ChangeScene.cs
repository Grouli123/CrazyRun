using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _loseMenu;

    [SerializeField] private Text _score;

    [SerializeField] private float _time;

    private void Start()
    {
        _winMenu.SetActive(false);
        _loseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        Timer();
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.CompareTag("NewLvl"))
        {
            _winMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void Timer()
    {
        _time -= Time.deltaTime;
        float roundTimer = Mathf.Round(_time);
        _score.text = roundTimer.ToString();
        

        if(_time <= 0)
        {
            Time.timeScale = 0;
            _loseMenu.SetActive(true);
        }
    }
}

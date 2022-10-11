using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _authorMenu;

    private void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        _mainMenu.SetActive(true);
        _authorMenu.SetActive(false);
    }

    public void AuthorMenu()
    {
        _mainMenu.SetActive(false);
        _authorMenu.SetActive(true);
    }
}

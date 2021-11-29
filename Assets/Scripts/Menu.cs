using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject Tutorial;
    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(true);
        Tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MenuBtn()
    {
        menuPanel.SetActive(true);
        Tutorial.SetActive(false);
    }
    public void TutorialBtn()
    {
        menuPanel.SetActive(false);
        Tutorial.SetActive(true);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }

}

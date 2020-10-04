using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuHandler : MonoBehaviour
{
    [SerializeField]
    protected Button quitBtn;

    [SerializeField]
    protected Button playBtn;


    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnStartClick);
        quitBtn.onClick.AddListener(OnQuitClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnQuitClick()
    {
        Application.Quit();
    }
}

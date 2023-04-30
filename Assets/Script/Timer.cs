using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime;
    public GameObject Winpanel;
    public GameObject LosePane;

    [SerializeField] Text countdownText;
    void Start()
    {
        Cursor.visible = false;
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = ("Time :")+currentTime.ToString("0");

        if (currentTime <= 0)
        {
            Cursor.visible = true;
            currentTime = 0;
            Winpanel.SetActive(true);
            Debug.Log("TIme UP");
        }
    }
    public void LosePlaye()
    {
        StartCoroutine(LoseCon());
    }
    public void LoadScene(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }
    IEnumerator LoseCon()
    {
        yield return new WaitForSeconds(5f);
        LosePane.SetActive(true);
        Cursor.visible = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TMP_Text winLose;
    public GameObject endPanel;
    public TMP_Text timerText;


    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject Blu;
    [SerializeField]
    private PlayerController playerController;
    private float laserPosY;
    public float timeLeft = 10f;

    public bool isPaused = false;

    void Start()
    {
        endPanel.SetActive(false);
        winLose.text = "YOU WON!";

        Time.timeScale = 1;
        laserPosY = laser.transform.position.y;
    }

    void Update()
    {
        if(!isPaused)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Round(timeLeft).ToString();

            if(timeLeft<=0.1)
            {
                Time.timeScale = 0;
                isPaused = true;
                winLose.text = "YOU LOST!";
                endPanel.SetActive(true);
            }

            laserPosY -= 0.02f;
            laser.transform.position = new Vector3(laser.transform.position.x, laserPosY, laser.transform.position.z);
            if(!playerController.isJumping && laserPosY - Blu.transform.position.y >= 3.1f)
            {
                laserPosY = Blu.transform.position.y + 3f;
            }
        }

    }
}

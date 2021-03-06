using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TMP_Text winLose;
    public GameObject endPanel;
    public TMP_Text timerText;
    public AudioSource audioSource;

    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject Blu;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private MovementComponent movementComponent;
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
        if (timeLeft <= 0.1)
        {
            Time.timeScale = 0;
            isPaused = true;
            movementComponent.isPaused = true;
            winLose.text = "YOU LOST!";
            endPanel.SetActive(true);
        }
        if (!isPaused)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Round(timeLeft).ToString();



            laserPosY -= 0.035f;
            laser.transform.position = new Vector3(laser.transform.position.x, laserPosY, laser.transform.position.z);
            if(!playerController.isJumping && laserPosY - Blu.transform.position.y >= 5f)
            {
                laserPosY = Blu.transform.position.y + 4.9f;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private int frame, max;
    public MovementComponent movementComponent;
    public TMP_Text message;
    public TMP_Text winLose;
    public GameObject endPanel;
    [SerializeField]
    private TMP_Text timerText;


    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject Blu;
    [SerializeField]
    private PlayerController playerController;
    private float laserPosY;
    public float timeLeft = 10f;

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        winLose.text = "YOU WON!";
        /*
        frame = 0;
        max = 120;
        panel.SetActive(false);
        message.gameObject.SetActive(true);
        message.text = "Move the boxes to match the pattern on the wall!";
        winLose.text = "YOU WON!";
         */
        Time.timeScale = 1;
        laserPosY = laser.transform.position.y;
    }

    // Update is called once per frame
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

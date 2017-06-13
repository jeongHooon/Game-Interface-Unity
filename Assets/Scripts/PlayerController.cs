using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    enum stage{ ROUND1, ROUND2, ROUND3};

    public CameraController CameraController;
    public TimerController TimerController;
    public float speed;
    public Image timerBar;
    public Text countText;
    public Text winText;
    public Text timerText;
    public Text scoreText;
    public bool bShake = false;
    public float time_count = 20;
    public int a = 10;
    public VirtualJoyStick moveJoyStick;

    private stage nowStage = stage.ROUND1;


    private Rigidbody rb;
    private Image im;
    private bool GameState = true;
    private bool hurryup = false;
    private int count;
    private int score = 0;
    private Vector3 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (time_count > 0 && GameState)
        {
            time_count -= Time.deltaTime;
                       
            if (time_count < 6)
                hurryup = true;
        }
        else if(GameState)
        {
            time_count = 0;
            if (count < 11)
            {
                winText.text = "<color=#ff0000>" + "You Lose" + "</color>";
                GameState = false;
            }
        }
        if(!hurryup)
            timerText.text = ((int)time_count).ToString();
        else
            timerText.text = "<color=#ff0000>" + ((int)time_count).ToString() + "</color>";
    }
    void FixedUpdate()
    {
        if (GameState)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            if(moveJoyStick.InputDirection != Vector3.zero)
            {
                moveHorizontal = moveJoyStick.InputDirection.x;
                moveVertical = moveJoyStick.InputDirection.z;
            }
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameState)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                count = count + 1;
                score += 10;
                SetCountText();
            }
            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (nowStage == stage.ROUND1)
                {
                    other.gameObject.SetActive(false);
                    score -= 10;
                }
                if (nowStage == stage.ROUND2)
                {
                    score -= 50;
                }
                if (nowStage == stage.ROUND3)
                {
                    score -= 100;
                }

                SetCountText();
                CameraController.ShakeCamera(1.0f);
            }
        }
    }
    public int GetStage()
    {
        if (nowStage == stage.ROUND1)
            return 1;
        else if (nowStage == stage.ROUND2)
            return 2;
        else
            return 3;
    }

    void SetCountText()
    {
        countText.text = "<color=#00ff00>" + "Count: " + count.ToString() + "</color>";
        scoreText.text = score.ToString();
        if (count >= 11)
        {
            if (nowStage == stage.ROUND1)
            {
                score += (int)time_count * 100;
                scoreText.text = score.ToString();
                count = 0;
                time_count = 20;
                nowStage = stage.ROUND2;
                transform.position = Vector3.zero;
                resetobject();
                TimerController.TimerBar.GetComponent<Image>().color = Color.green;
                hurryup = false;
                }
            else if (nowStage == stage.ROUND2)
            {
                score += (int)time_count * 100;
                scoreText.text = score.ToString();
                count = 0;
                time_count = 20;
                nowStage = stage.ROUND3;
                transform.position = Vector3.zero;
                resetobject();  
                TimerController.TimerBar.GetComponent<Image>().color = Color.green;
                hurryup = false;
            }
            else
            {
                winText.text = "<color=#0000ff>" + "You Win" + "</color>";
                
                GameState = false;
            }
        }
    }
    void resetobject()
    {
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (1)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (2)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (3)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (4)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (5)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (6)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (7)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (8)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (9)").gameObject.SetActive(true);
        GameObject.Find("Pick UPs").transform.FindChild("Pick Up (10)").gameObject.SetActive(true);

        GameObject.Find("Obstacles").transform.FindChild("Obstacle").gameObject.SetActive(true);
        GameObject.Find("Obstacles").transform.FindChild("Obstacle (1)").gameObject.SetActive(true);
        GameObject.Find("Obstacles").transform.FindChild("Obstacle (2)").gameObject.SetActive(true);
        GameObject.Find("Obstacles").transform.FindChild("Obstacle (3)").gameObject.SetActive(true);
        GameObject.Find("Obstacles").transform.FindChild("Obstacle (4)").gameObject.SetActive(true);
        GameObject.Find("Obstacles").transform.FindChild("Obstacle (5)").gameObject.SetActive(true);
    }
}
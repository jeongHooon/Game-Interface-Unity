using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CameraController CameraController;
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
                other.gameObject.SetActive(false);
                
                score -= 10;
                SetCountText();
                CameraController.ShakeCamera(1.0f);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "<color=#00ff00>" + "Count: " + count.ToString() + "</color>";
        scoreText.text = score.ToString();
        if (count >= 11)
        {   
            winText.text = "<color=#0000ff>" + "You Win" + "</color>";
            score += (int)time_count * 100;
            scoreText.text = score.ToString();
            GameState = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
    public Transform TimerBar;
    public Transform TimerText;
    public PlayerController player;
    [SerializeField] private float currentAmount;

    private bool counter = false;
    
	void Update () {
        currentAmount = player.time_count;
        if (currentAmount < 6)
        {
            TimerBar.GetComponent<Image>().color = Color.red;
            if (currentAmount - (int)currentAmount > 0.5f && !counter)
            {
                TimerText.GetComponent<Text>().fontSize = 25;
                counter = true;
            }
            else if(currentAmount - (int)currentAmount <= 0.5f && counter)
            {
                TimerText.GetComponent<Text>().fontSize = 17;
                counter = false;
            }
        }
        TimerBar.GetComponent<Image>().fillAmount = currentAmount / 20f;

	}
}

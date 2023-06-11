using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    bool canJump = true;
    bool canHoldJump = false;

    float JumpBtnTimer = 0.2f;
    float JumpBtnStopwatch = 0;

    Rigidbody2D rb;

    GameObject GameEndScreen;

    TextMeshProUGUI ScoreHolderTxt;
    float score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameEndScreen = GameObject.Find("Canvas").transform.Find("EndScreen").gameObject;
        ScoreHolderTxt = GameObject.Find("Canvas").transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        score += Time.deltaTime;
        ScoreHolderTxt.text = "Score: " + ((score * 30).ToString("f0"));

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("up arrow input");

            if(canJump)
            {
                canJump = false;
                canHoldJump = true;

                rb.velocity = new Vector3(0, 20, 0);
            }
            else if(canHoldJump)
            {
                JumpBtnStopwatch += Time.deltaTime;
                if(JumpBtnStopwatch > JumpBtnTimer)
                {
                    JumpBtnStopwatch = 0;
                    canHoldJump = false;
                }
                else
                {
                    rb.velocity = new Vector3(0, 20, 0);
                }
            }
        }
        else
        {
            canHoldJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if(collision.transform.CompareTag("Ground"))
            {
                canJump = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.transform.CompareTag("obstacle"))
            {
                GameEndScreen.SetActive(true);
                Time.timeScale = 0;

                if(PlayerPrefs.GetFloat("maxScore") < score * 30)
                {
                    PlayerPrefs.SetFloat("maxScore", score * 30);
                }
            }
        }
    }
}

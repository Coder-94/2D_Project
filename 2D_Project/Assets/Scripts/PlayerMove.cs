using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int playerSpeed = 1;
    Rigidbody2D Player2D;
    Player player = Player.GetInstance();


    private void Start()
    {
        Player2D = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        PlayerMoving();
    }
    void PlayerMoving()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Player2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
        }
    }
}

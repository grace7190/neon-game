﻿using UnityEngine;

public class PlayerJoystickInputManager : MonoBehaviour
{
    public bool Invert;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    public KeyCode PushKey;
    public KeyCode JumpKey;

    private PlayerController _controller;
    private float horizontalVal;
    private float verticalVal;
    private bool grabOn;
    private bool jump;

    void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (gameObject.name == "Player 1")
        {
            horizontalVal = Input.GetAxis("Horizontal_P1");
            verticalVal = Input.GetAxis("Vertical_P1");
            grabOn = Input.GetButton("Grab_P1");
            jump = Input.GetButton("Jump_P1");
        }
        else
        {
            horizontalVal = Input.GetAxis("Horizontal_P2");
            verticalVal = Input.GetAxis("Vertical_P2");
            grabOn = Input.GetButton("Grab_P2");
            jump = Input.GetButton("Jump_P2");
        }
        if (horizontalVal == 1)
        {

            if (Input.GetKey(PushKey) || grabOn)
            {
                bool IsFacing = _controller.IsFacing(Vector3.right);
                if (IsFacing) //if facing left, try to pull left block [ ]->p
                {
                    _controller.TryPullBlock();
                } //if facing right, try to push block to right p->[ ]
                _controller.TryPushBlock();
            }
            else
            {   //only move if didn't turn
                bool didTurn = _controller.Turn(Vector3.right);
                if (!didTurn)
                {
                    _controller.Move2(horizontalVal, verticalVal);
                }

            }
        }
        else if (horizontalVal == -1)
        {

            if (Input.GetKey(PushKey) || grabOn)
            {
                bool IsFacing = _controller.IsFacing(Vector3.left);
                if (IsFacing)
                {
                    _controller.TryPullBlock();
                }
                _controller.TryPushBlock();
            }
            else
            {
                bool didTurn = _controller.Turn(Vector3.left);
                if (!didTurn)
                {
                    _controller.Move2(horizontalVal, verticalVal);
                }

            }
        }
        else if (verticalVal == -1)
        {
            if (Input.GetKey(PushKey) || grabOn)
            {
                if (Invert)
                {
                    _controller.Turn(Vector3.back);
                    _controller.TryPushBlock();
                }
                else
                {
                    _controller.Turn(Vector3.forward);
                    _controller.TryPushBlock();
                }
            }

        }
        else if (verticalVal == 1)
        {
            if (Input.GetKey(PushKey) || grabOn)
            {
                if (Invert)
                {
                    _controller.Turn(Vector3.back);
                    _controller.TryPullBlock();
                }
                else
                {
                    _controller.Turn(Vector3.forward);
                    _controller.TryPullBlock();
                }

            }
        }
        if (Input.GetKeyDown(JumpKey) || jump)
        {
            _controller.Jump(); 
        }
    }
}
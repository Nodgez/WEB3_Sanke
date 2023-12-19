using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private Snake_Actions inputActions;
    private Vector2Int movementInput;

    private Dictionary<string, Vector2Int> directionMap = new Dictionary<string, Vector2Int>()
    {
        {"W", Vector2Int.up},
        {"S", Vector2Int.down},
        {"A", Vector2Int.left},
        {"D", Vector2Int.right},
    };

    private void Awake()
    {
        inputActions = new Snake_Actions();
        inputActions.Snake_Map.Enable();
        inputActions.Snake_Map.Movement.performed += OnMovementPerformed;

        movementInput = Vector2Int.right;
    }

    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        print(obj.ToString());
        movementInput = directionMap[obj.control.name.ToUpper()];
    }
    public Vector2Int GetDirection()
    {
        return movementInput;
    }
}

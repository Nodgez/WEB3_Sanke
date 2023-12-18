using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Vector2Int position;
    public void SetPosition(Vector2Int position)
    {
        this.position = position;
        this.transform.position = new Vector3(position.x, position.y, 0);
    }
    public Vector2Int GetPosition()
    {
        return this.position;
    }
}

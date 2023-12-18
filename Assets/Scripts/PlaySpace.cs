using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySpace : MonoBehaviour
{
    public bool renderSate = true;
    private List<BoardCell> boardState = new List<BoardCell>();
    public static PlaySpace Instance;
    public int gameWidth = 16;
    public int gameHeight = 8;

    [SerializeField] private GameObject cellStatePrefab;

    public void Awake()
    {
        Instance = this;

        for (int x = 0; x < gameWidth; x++)
        {
            for (int y = 0; y < gameHeight; y++)
            {
                var singleIndex = y + (x * gameHeight);
                var cellVisual = Instantiate(cellStatePrefab);
                cellVisual.name = "Cell: (" + x.ToString() + ", " + y.ToString() + ") : (" + singleIndex.ToString() + ")";
                var boardCell = new BoardCell(new Vector2Int(x, y), cellVisual);
                boardState.Add(boardCell);
                if (renderSate)
                    boardCell.Show();
            }
        }
    }
    public int GetMaxSpace()
    {
        return gameWidth * gameHeight;
    }

    public void RefreshBoardState(LinkedListNode<SnakeSegment> snakeHead)
    {
        for (var i = 0; i < boardState.Count; i++)
            boardState[i].isOccupied = false;

        while (snakeHead != null)
        {
            var x = snakeHead.Value.GetPosition().x;
            var y = snakeHead.Value.GetPosition().y;

            var index = y + (x * gameHeight);
            boardState[index].isOccupied = true;

            snakeHead = snakeHead.Next;
        }

        if (renderSate)
            for (var i = 0; i < boardState.Count; i++)
            {
                boardState[i].UpdateVisual();
                boardState[i].Show();
            }
        else
            for (var i = 0; i < boardState.Count; i++)
                boardState[i].Hide();


    }

    public BoardCell GetUnoccupiedCell()
    {
        var unOccupiedCells = boardState.Where(x => !x.isOccupied).ToList();
        var randomUnoccupied = unOccupiedCells[Random.Range(0, unOccupiedCells.Count)];
        return randomUnoccupied;
    }

    public bool IsInBounds(Vector2Int position)
    {
        return position.x > -1 && position.x < gameWidth && position.y > -1 && position.y < gameHeight;
    }

}
public class BoardCell
{
    public Vector2Int position;
    public bool isOccupied;

    private GameObject debug;
    private SpriteRenderer debugSprite;

    public BoardCell(Vector2Int position, GameObject debug)
    {
        this.position = position;
        this.isOccupied = false;
        this.debug = debug;
        this.debug.transform.position = new Vector3(position.x, position.y, -4f);

        debugSprite = debug.GetComponent<SpriteRenderer>();
    }

    public void Show()
    {
        this.debug.SetActive(true);
    }

    public void Hide()
    {
        this.debug.SetActive(false);
    }

    public void UpdateVisual()
    {
        debugSprite.color = isOccupied ? new Color(1, 0, 0, 0.5f) : new Color(0, 1, 0, 0.5f);
    }
}

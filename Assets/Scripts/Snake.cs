using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform snakeProtoType;
    [SerializeField] private Food food;
    [SerializeField] private AnimationCurve speedRamp;

    private float moveTransition = 0;

    private Vector2Int moveDir;

    private LinkedList<SnakeSegment> snakeSegmentChain = new LinkedList<SnakeSegment>();

    void Awake()
    {
        Time.fixedDeltaTime = speedRamp.Evaluate(0);
    }

    void Start()
    {
        var headLocation = new Vector2Int(PlaySpace.Instance.gameWidth / 2, PlaySpace.Instance.gameHeight / 2);
        snakeSegmentChain.AddFirst(new SnakeSegment(headLocation, snakeProtoType));

        moveDir = gameInput.GetDirection();
        AddToTail(headLocation - moveDir);

        RefreshFood();
    }

    void FixedUpdate()
    {
        var endOfTail = MoveTail();
        var snakeHead = snakeSegmentChain.First;
        if (food.GetPosition() == snakeHead.Value.GetPosition())
        {
            AddToTail(endOfTail);
            var speedSample = (float)snakeSegmentChain.Count / PlaySpace.Instance.GetMaxSpace();
            Time.fixedDeltaTime = speedRamp.Evaluate(speedSample);
            RefreshFood();
        }
        else
            PlaySpace.Instance.RefreshBoardState(snakeSegmentChain.First);

        var headPosition = snakeHead.Value.GetPosition();
        while (snakeHead.Next != null)
        {
            snakeHead = snakeHead.Next;
            if (headPosition == snakeHead.Value.GetPosition())
            {
                Time.timeScale = 0;
            }
        }

        moveTransition = 0;
    }

    void Update()
    {
        moveTransition += Time.deltaTime;
        var snakeHead = snakeSegmentChain.First;

        int counter = 0;
        while (snakeHead != null)
        {
            snakeHead.Value.UpdateVisual(moveTransition / Time.fixedDeltaTime, counter % 2 == 0);
            snakeHead = snakeHead.Next;
            counter++;
        }
    }

    private void RefreshFood()
    {
        PlaySpace.Instance.RefreshBoardState(snakeSegmentChain.First);
        var unoccupiedCell = PlaySpace.Instance.GetUnoccupiedCell();
        unoccupiedCell.isOccupied = true;
        food.SetPosition(unoccupiedCell.position);
    }

    //returns the end of the tail's old position
    Vector2Int MoveTail()
    {
        if (gameInput.GetDirection() != -moveDir)
            moveDir = gameInput.GetDirection();

        var head = snakeSegmentChain.First;
        var newPosition = head.Value.GetPosition() + moveDir;
        if (!PlaySpace.Instance.IsInBounds(newPosition))
        {
            Time.timeScale = 0;
            return snakeSegmentChain.Last.Value.GetPosition();//return the current end of tail
        }

        while (head != null)
        {
            var oldPosition = head.Value.GetPosition();
            head.Value.SetPosition(newPosition);

            head = head.Next;
            newPosition = oldPosition;
        }
        return newPosition;
    }

    void AddToTail(Vector2Int position)
    {
        var newSegement = Instantiate(snakeProtoType, this.transform);
        snakeSegmentChain.AddLast(new SnakeSegment(position, newSegement));
    }
}

public class SnakeSegment
{
    private Vector2Int position;
    private Transform visual;

    private Vector3 lerpStart;
    private Quaternion slerpStart;

    public SnakeSegment(Vector2Int position, Transform visual)
    {
        this.visual = visual;
        this.visual.position = new Vector3(position.x, position.y);
    }

    public void SetPosition(Vector2Int position)
    {
        lerpStart = visual.transform.position;
        slerpStart = visual.transform.localRotation;
        this.position = position;
    }

    public Vector2Int GetPosition()
    {
        return this.position;
    }

    public void UpdateVisual(float t, bool isEvenInPlacement)
    {
        var rotationRadian = isEvenInPlacement ? Time.time / (0.5f * Time.fixedDeltaTime) : Time.time / (-0.5f * Time.fixedDeltaTime);
        t = Mathf.Clamp01(t);
        this.visual.position = Vector3.Lerp(lerpStart, new Vector3(position.x, position.y), t);
        this.visual.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(rotationRadian) * 10f);
    }
}

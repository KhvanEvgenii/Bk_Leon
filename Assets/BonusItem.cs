using UnityEngine;

public class BonusItem : MonoBehaviour
{
    [SerializeField] private Vector2 moveDistance;
    [SerializeField] private float moveSpeed = 1f;
    private Vector2 startPosition;
    private float highestY;
    private float lowestY;

    private bool moveUp = true;
    void Start()
    {
        startPosition = transform.position;
        highestY = startPosition.y + moveDistance.y;
        lowestY = startPosition.y - moveDistance.y;
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = transform.position.y;
        float currentX = transform.position.x;
        if (currentY == highestY || currentY == lowestY)
            moveUp = !moveUp;

        if (moveUp)
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(currentX, highestY),Time.deltaTime * moveSpeed);
        else
            transform.position = Vector2.MoveTowards(transform.position,new Vector2(currentX, lowestY), Time.deltaTime * moveSpeed);
    }
}

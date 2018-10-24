using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody body;

    public GameObject bodies;
    public List<GameObject> bodyList;

    private int currentDirection;
    public int dir = 2;
    private int oldMove = 3;

    public float moveTime;
    private float nextMove;

    private Vector3 prevHead;
    private Vector3 newBody;
    private Vector3 oldBody;

    private GameController gameController;
    public GameObject game;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        nextMove = moveTime;
        gameController = game.GetComponent<GameController>();
        bodyList.Add(Instantiate(bodies, new Vector3(0.5f, -0.5f, 0), Quaternion.identity));
        gameController.generateFood();
	}
	
	// Update is called once per frame
	void Update () {
        int currentDirection = determineDirection();
        if (nextMove >= moveTime)
        {
            autoMove(currentDirection);
            nextMove = 0f;
        }
        nextMove = nextMove + Time.deltaTime;
    }

    

    public int determineDirection()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 && oldMove != 1)
        {
            dir = 0;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && oldMove != 0)
        {
            dir = 1;
        }
        else if (Input.GetAxisRaw("Vertical") == 1 && oldMove != 3)
        {
            dir = 2;
        }
        else if (Input.GetAxisRaw("Vertical") == -1 && oldMove != 2)
        {
            dir = 3;
        }
        return dir;
    }

    public void autoMove(int move)
    {

        prevHead = body.position;

        if (move == 0)
        {
            body.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else if (move == 1)
        {
            body.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else if (move == 2)
        {
            body.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        else if (move == 3)
        {
            body.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
        oldMove = move;
        moveBody();
    }

    public void moveBody()
    {
        for (int i = 0; i != bodyList.Count; ++i)
        {        
            if (i == 0)
            {
                //Debug.Log("primary move accessed.");
                oldBody = bodyList[i].transform.position;
                bodyList[i].transform.position = prevHead;
            }
            else
            {
                //Debug.Log(i + " move accessed.");
                newBody = oldBody;
                oldBody = bodyList[i].transform.position;
                bodyList[i].transform.position = newBody;
            }
        }
    }

    public void add()
    {
        if (bodyList.Count == 0)
        {
            bodyList.Add(Instantiate(bodies, prevHead, Quaternion.identity));
        }
        else
        {
            bodyList.Add(Instantiate(bodies, oldBody, Quaternion.identity));
        }
        gameController.generateFood();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            add();
            gameController.addScore();
        }
        else if (other.CompareTag("Body"))
        {
            gameController.gameOver = true;
        }
    }
}

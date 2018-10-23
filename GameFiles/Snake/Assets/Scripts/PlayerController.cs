using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public Boundary boundary;

    Rigidbody body;
    public GameObject bodies;
    List<GameObject> bodyList;

    private int currentDirection = 2;

    private float nextMove = 1;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        int currentDirection = determineDirection();
        if (nextMove >= 0.75f)
        {
            autoMove(currentDirection);
            nextMove = 0f;
        }
        nextMove = nextMove + Time.deltaTime;
    }

    public int dir = 2;

    public int determineDirection()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            dir = 0;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            dir = 1;
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            dir = 2;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            dir = 3;
        }
        return dir;
    }

    public void autoMove(int move)
    {
        if (move == 0)
        {
            body.position = new Vector3(
                Mathf.Clamp(transform.position.x + 1, boundary.xMin, boundary.xMax),
                Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
                transform.position.z);
        }
        else if (move == 1)
        {
            body.position = new Vector3(
                Mathf.Clamp(transform.position.x - 1, boundary.xMin, boundary.xMax),
                Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
                transform.position.z);
        }
        else if (move == 2)
        {
            body.position = new Vector3(
                Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(transform.position.y + 1, boundary.yMin, boundary.yMax),
                transform.position.z);
        }
        else if (move == 3)
        {
            body.position = new Vector3(
                Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(transform.position.y - 1, boundary.yMin, boundary.yMax),
                transform.position.z);
        }
    }

    public Vector3 determineDir(int daDir, int daSize)
    {
        Vector3 temp;
        if (daDir == 0)
        {
            temp = new Vector3(bodyList[daSize-1].transform.position.x - 1)
        }
        return temp;
    }

    public void addToList()
    {
        int sz = bodyList.Count;
        GameObject temp = bodyList[sz - 1];
        GameObject newBody = Instantiate(bodies, new Vector3(bodyList[sz - 1].transform.position.x), Quaternion.identity);
        bodyList.Add(newBody);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    private GameController gameController;
    public GameObject game;

    private void Start()
    {
        gameController = game.GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Boundary"))
        {
            if (other.CompareTag("Food"))
            {
                Debug.Log("Food Generated on Contact with Body");
                gameController.generateFood();
            }
            Destroy(other.gameObject);
        }
    }

}

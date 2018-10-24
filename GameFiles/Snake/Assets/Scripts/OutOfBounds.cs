using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    private GameController gameController;
    public GameObject controller;

    private void Start()
    {
        gameController = controller.GetComponent<GameController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.gameOver = true;
        }
        Destroy(other.gameObject);
    }
}

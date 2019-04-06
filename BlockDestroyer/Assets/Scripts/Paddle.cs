using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // Configs
    [SerializeField] float ScreenUnitsWide = 16f;
    [SerializeField] float maxLimit = 15f;
    [SerializeField] float minLimit = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * ScreenUnitsWide;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

        paddlePos.x = Mathf.Clamp(mousePosInUnits, minLimit, maxLimit);
        transform.position = paddlePos;
	}
}

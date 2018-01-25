using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    // public variable to set the speed of rotation
    public Vector3 rotationAmount;

    // public variables to control the size and speed the collectable oscilates between
    public float startSize;
    public float endSize;
    public float scaleSpeed;

	void Update () {
        this.transform.Rotate(rotationAmount * Time.deltaTime);

        // adjust size along a Sin wave
        float percent = Mathf.Sin(Time.time * scaleSpeed) * .5f + .5f;
        this.transform.localScale = Vector3.one * Mathf.Lerp(startSize, endSize, percent);
	}
}

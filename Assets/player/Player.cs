using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//  Attach MonoBehaviour to a Game Object player
public class Player : MonoBehaviour {

    Rigidbody rigid;
    AudioSource source;
    Text scoreText;

    static int score = 0;

    // Variables be set in the Unity inspector
    public float power;
    public float maxSpeed;
    public float jumpPower;
    public float spinPower;
    public Color colorToSetTo;

    bool canJump;

    void Start () {
        rigid = this.GetComponent<Rigidbody>();
        source = this.GetComponent<AudioSource>();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        scoreText.text = "Score: " + score;
 
        this.GetComponent<MeshRenderer>().material.color = colorToSetTo;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            // First give the player an upwards velocity.
            Vector3 velocity = rigid.velocity;
            velocity.y = jumpPower;
            rigid.velocity = velocity;

            canJump = false;  // disable jumping until the player hits ground again
        }

        if (this.transform.position.y < -10)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate () {
        this.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * spinPower, 0);

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = transform.TransformDirection(direction);

        rigid.AddForce(direction * power);

        Vector3 velocity = rigid.velocity;
        Vector3 groundVelocity = new Vector3(velocity.x, 0, velocity.z);
        if (groundVelocity.magnitude > maxSpeed) {  // cap the speed
            groundVelocity = groundVelocity.normalized * maxSpeed;
            rigid.velocity = new Vector3(groundVelocity.x, velocity.y, groundVelocity.z);
        }
	}

    // Called when hit the collectable
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Collectable") {
            other.transform.parent = this.transform;
            other.transform.localPosition = Vector3.up * 2;

            source.Play();

            score = score + 1;
            scoreText.text = "Score: " + score;
        }
        if (other.tag == "End") {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }

    // Called when hit the ground
    void OnCollisionEnter(Collision collision) {
        canJump = true;
    }
}
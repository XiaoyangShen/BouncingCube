using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject bullet;
    public float reloadTime = .2f;
    float timeUntilShot;
    public float fireSpeed = 20f;

    void Update() {
        timeUntilShot = timeUntilShot - Time.deltaTime;
    }

    // Called when detecting player, shooting bullet
    void OnTriggerStay(Collider other) {
        if (timeUntilShot < 0 && other.tag == "Player") {
            GameObject g = Instantiate(bullet, this.transform.position, Quaternion.identity);
            timeUntilShot = reloadTime;
            Vector3 direction = (other.transform.position - this.transform.position).normalized;
            g.GetComponent<Rigidbody>().velocity = direction * fireSpeed;
        }
    }
}

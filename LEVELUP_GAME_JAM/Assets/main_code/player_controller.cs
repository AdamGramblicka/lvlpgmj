using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    private bool grounded = true;
    private bool jumped = true;
    public int enabler = 0;

    private stat_vault vault_access;

    public float angle;

    public float speed = 5.5f;
    private float jump_speed_limiter = 1.6f;
    private float[] attack_radius = {1,2};

    private Rigidbody2D rg2d;

    void Start()
    {
        vault_access = GameObject.FindGameObjectWithTag("STATUS").GetComponent<stat_vault>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float a = Input.GetAxis("a_stick_x");
        float b = Input.GetAxis("a_stick_y");

        if (Input.GetKeyDown(KeyCode.JoystickButton2)) {
            rg2d.AddForce(Vector2.up * (speed / 2));
        }

        if (a != 0.0f || b != 0.0f) {
            angle = Mathf.Atan2(a, b) * Mathf.Rad2Deg;
        } else {
            angle = 0.0f;
        }
        if (enabler == 0) {
            transform.position += new Vector3(a,0,0) * (grounded ? speed : (speed/1.6f)) * Time.deltaTime;
        } else if(enabler == 1) {
            if(a > 0)
                transform.position += new Vector3(a,0,0) * (grounded ? speed : (speed/1.6f)) * Time.deltaTime;
        } else if(enabler == 2)
        {
            if(a < 0)
                transform.position += new Vector3(a,0,0) * (grounded ? speed : (speed/1.6f)) * Time.deltaTime;
        }
        if (grounded)
        {
            if (Input.GetKey(KeyCode.Joystick1Button3))
            {
                jumped = false;
                rg2d.AddForce(transform.up * speed * 1.5f, ForceMode2D.Impulse);
                // stav jumping
            }
            else
            {
                if (!jumped)
                    // stav walking
                    return;
            }
        }
        //Animation


        // ATTACKS

        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attack_radius[0], 1 << 7);
            Debug.Log("Attack 1");
            foreach(Collider2D col in hits) {
                vault_access.health[col.GetComponent<identificator>().NUMBER] -= 10;
                col.GetComponent<player_controller>().Hit(transform.position, 1);
            }
        }

         if (Input.GetKeyDown(KeyCode.Joystick1Button1)) {
             if((grounded && Mathf.Abs(angle) >= 150) || (!grounded /* && tazky vypocet nechce sa mi ... */ )) {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attack_radius[1], 1 << 7);
                Debug.Log("Attack 2");
                foreach(Collider2D col in hits) {
                    vault_access.health[col.GetComponent<identificator>().NUMBER] -= 30;
                    col.GetComponent<player_controller>().Hit(transform.position, 2);
                }
             }
        }
    }

    private void Hit(Vector3 pos, int type)
    {
        throw new NotImplementedException();
        // vypocet knockbacku
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attack_radius[0]);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attack_radius[1]);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("ground")){
            grounded = true;
            rg2d.velocity = Vector3.zero;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        grounded = false;
    }
}
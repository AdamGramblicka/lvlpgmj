using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_controller : MonoBehaviour
{
    public bool grounded = true;
    public int enabler = 0;

    private stat_vault vault_access;

    public float angle;

    private float a;
    private float b;
    private KeyCode a1;
    private KeyCode a2;
    private KeyCode a3;

    public bool started = true; // co uz
    bool restart = false;
    bool deat = false;

    public float speed = 5.5f;
    private float jump_speed_limiter = 1.6f;
    private float[] attack_radius = { 1.5f, 2.5f };
    float t = 0;

    private Rigidbody2D rg2d;

    private GamePad.Index state;

    void Start()
    {
        vault_access = GameObject.FindGameObjectWithTag("STATUS").GetComponent<stat_vault>();
        rg2d = GetComponent<Rigidbody2D>();
        switch (name)
        {
            case "Player":
                state = GamePad.Index.One;
                break;
            case "Player1":
                state = GamePad.Index.Two;
                break;
            case "Player2":
                state = GamePad.Index.Three;
                break;
            case "Player3":
                state = GamePad.Index.Four;
                break;
        }
    }

    void FixedUpdate()
    {
        if (restart) {
            Restart();
        }
        if (vault_access.health[GetComponent<identificator>().NUMBER-1] <= 0) {
            Die();
        }
        if (deat) {
            Dissapear(ref t, vault_access.colors[GetComponent<identificator>().NUMBER-1]);
        } else
        /*if (started)*/ {
        float a = GamePad.GetAxis(GamePad.Axis.LeftStick, state).x;
        float b = GamePad.GetAxis(GamePad.Axis.LeftStick, state).y;
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            rg2d.AddForce(Vector2.up * (speed / 2));
        }

        if (a != 0.0f || b != 0.0f)
        {
            angle = Mathf.Atan2(a, b) * Mathf.Rad2Deg;
        }
        else
        {
            angle = 0.0f;
        }
        if (enabler == 0)
        {
            transform.position += new Vector3(a, 0, 0) * (grounded ? speed : (speed / 1.6f)) * Time.deltaTime;
        }
        else if (enabler == 1)
        {
            if (a > 0)
                transform.position += new Vector3(a, 0, 0) * (grounded ? speed : (speed / 1.6f)) * Time.deltaTime;
        }
        else if (enabler == 2)
        {
            if (a < 0)
                transform.position += new Vector3(a, 0, 0) * (grounded ? speed : (speed / 1.6f)) * Time.deltaTime;
        }
        if (grounded)
        {
            if (GamePad.GetButton(GamePad.Button.Y, state))
            {
                rg2d.velocity += Vector2.up * speed * 1.5f;
                // stav jumping
            }
        }
        //Animation


        // ATTACKS

        if (GamePad.GetButtonDown(GamePad.Button.A, state))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attack_radius[0], 1 << 8);
            foreach (Collider2D col in hits)
            {
                if (!col.isTrigger && col.CompareTag("player"))
                {
                    if (col.gameObject == this.gameObject)
                        continue;
                    Debug.Log("p->" + state + " Attack 1" + col.transform.position);
                    vault_access.health[col.GetComponent<identificator>().NUMBER - 1] -= 10;
                    col.gameObject.GetComponent<player_controller>().Hit(transform.position, 1);
                    Vector2 direct = (transform.position - col.transform.position).normalized;
                    GetComponentInChildren<attack>().Attack(angle < 0 ? Vector3.Angle(transform.position, col.transform.position)-90 : Vector3.Angle(transform.position, col.transform.position)+90, Vector3.Distance(transform.position, col.transform.position));
                }
            }
        }

        /*if (GamePad.GetButtonDown(GamePad.Button.B, state))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attack_radius[1], 1 << 8);
            Debug.Log("p->" + state + " Attack 2");
            foreach (Collider2D col in hits)
            {
                if (!col.isTrigger && col.CompareTag("player"))
                {
                    if (col.gameObject == this.gameObject)
                        continue;
                    vault_access.health[col.GetComponent<identificator>().NUMBER - 1] -= 30;
                    col.GetComponent<player_controller>().Hit(transform.position, 2);
                    GetComponentInChildren<attack>().Attack1((180 * Vector3.Angle(transform.position, col.transform.position)) / Mathf.PI);
                }
            }
        }*/ //RIP ATTACK 10.2.2019 2:45 :'(
        }
    }


    private void Hit(Vector3 pos, int type)
    {
        StartCoroutine(Flash(0, vault_access.colors[GetComponent<identificator>().NUMBER-1]));
        Debug.DrawLine(pos, transform.position, Color.yellow, Vector3.Distance(transform.position, pos));
        Vector2 direction = (transform.position - pos).normalized;

        rg2d.AddForce(direction * type * 7, ForceMode2D.Impulse);
        // vypocet knockbacku
    }

    public void Die() {
        //StartCoroutine(death(0,GetComponentInChildren<SpriteRenderer>().color));
        deat = true;
        death(ref t, vault_access.colors[GetComponent<identificator>().NUMBER-1]);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attack_radius[0]);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attack_radius[1]);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
        {
            grounded = true;
            rg2d.velocity = Vector3.zero;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
            grounded = false;
    }

    IEnumerator Flash(float t, Color32 color)
    {
        while (t < 1)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color32.Lerp(new Color32(220, 20, 60, 150), color, t);
            t += Time.deltaTime;
            yield return null;
        }
    }

    void death(ref float t, Color32 color) {
         if(vault_access.DEATHS+1 == 3) {
                GameObject.Find("Text").GetComponent<texter>().Say("Player " + state + " won the game", new Color32(50,205,50,255));
                restart = true;
            } else {
                vault_access.DEATHS++;
                GameObject.Find("Text").GetComponent<texter>().Say("Player " + state + " eliminated", new Color32(255,0,0,255));
            }
            Dissapear(ref t,color);
    }

    void Dissapear(ref float t, Color32 color) {
        if(t < 1) {
             GetComponentInChildren<SpriteRenderer>().color = Color32.Lerp(color, new Color32(color.r, color.g, color.b, 0), t);
        } else {
            enabled = false;
        }
    }

    void Restart() {
        if(GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any) || GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Any) || GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Any) || GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.Any)) {
            SceneManager.LoadScene("confirm", LoadSceneMode.Single); //au
        }
    }
}
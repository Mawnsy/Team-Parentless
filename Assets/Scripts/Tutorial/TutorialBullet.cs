﻿using UnityEngine;
using System.Collections;

public class TutorialBullet : MonoBehaviour
{

    public float speed = 20;

    private Rigidbody2D Bulletbody2d = new Rigidbody2D();

    public GameObject WallHit;
    public GameObject BodyHit;
    public GameObject Blood;

    public AudioClip FirmWall;
    public AudioClip Body;

    public float Recoil;
    public float AngleShot = 0;
    public int damage;

    // Use this for initialization
    void Start()
    {
        damage = GameObject.Find("Character").GetComponent<TutorialMovment>().WeaponDamage;

        Bulletbody2d = GetComponent<Rigidbody2D>();
        GameObject[] Weapontype = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < Weapontype.Length; i++)
        {
            if (Weapontype[i].GetComponent<Weapon>().IsPickedUp)
            {
                Recoil = Weapontype[i].GetComponent<Weapon>().Recoil;
            }
        }

        Recoil = Random.Range(-Recoil, Recoil);

        float deltaX = -((Screen.width / 2) - Input.mousePosition.x);
        float deltaY = -((Screen.height / 2) - Input.mousePosition.y);

        float angle = Mathf.Atan2(deltaY, deltaX);

        Bulletbody2d.velocity = new Vector2(Mathf.Cos(angle + Recoil + AngleShot) * speed, Mathf.Sin(angle + Recoil + AngleShot) * speed);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "FirmWall")
        {
            AudioSource.PlayClipAtPoint(FirmWall, transform.position, 0.02f);
            Instantiate(WallHit, transform.position, transform.rotation);
        }
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Boss")
        {
            AudioSource.PlayClipAtPoint(Body, transform.position, 0.12f);
            Instantiate(BodyHit, transform.position, transform.rotation);
            Instantiate(Blood, transform.position, transform.rotation);
            coll.gameObject.SendMessage("ApplyDamage", damage);

        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {


    }
}

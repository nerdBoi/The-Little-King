﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float leftBound;
    public float rightBound;
    public GameObject bossMonster;
    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player") && !isActivated)
        {
            isActivated = true;

            Vector3 flameSpointLeft = transform.position;
            flameSpointLeft.x = flameSpointLeft.x - leftBound;

            Vector3 flameSpointRight = transform.position;
            flameSpointRight.x = transform.position.x + rightBound;

            Vector3 badGuySpot = transform.position;
            badGuySpot.y = badGuySpot.y + 10;
            Instantiate(bossMonster, badGuySpot, transform.rotation);

        }
    }

}

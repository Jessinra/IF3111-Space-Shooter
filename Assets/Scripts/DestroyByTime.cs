﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float lifetime = 2.0F;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
}

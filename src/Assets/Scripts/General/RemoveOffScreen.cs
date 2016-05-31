﻿using UnityEngine;

namespace Assets.Scripts.General
{
    public class RemoveOffScreen : MonoBehaviour
    {
        void Update()
        {
            if (Vector3.Distance(UnityEngine.Camera.main.transform.position, transform.position) > 100)
            {
                Destroy(gameObject);
            }
        }
    }
}
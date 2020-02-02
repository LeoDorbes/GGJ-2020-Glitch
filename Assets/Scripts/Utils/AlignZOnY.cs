using System;
using UnityEngine;

namespace Utils
{
    public class AlignZOnY : MonoBehaviour
    {
        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 100);
        }
    }
}
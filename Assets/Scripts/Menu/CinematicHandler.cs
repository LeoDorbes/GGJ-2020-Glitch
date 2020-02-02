using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Menu
{
    public class CinematicHandler : MonoBehaviour
    {
        private int current = 0;
        [SerializeField] public List<Image> cinematicElements;

        public void Awake()
        {
            if (cinematicElements.Count > 0)
            {
                
            }
        }

        public void DisplayNextCinematic()
        {
            
        }
    }
}
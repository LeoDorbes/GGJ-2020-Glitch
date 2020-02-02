using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaserEnemies
{
    public class LaserEnemyEntity : MonoBehaviour
    {
        [SerializeField] private GameObject _laser;
        [SerializeField] private int _maxRange = 20;
        [SerializeField] private int _precision = 3;
        [SerializeField] private LayerMask _layerMask;

        private void Start()
        {
            StartCoroutine(CallMakeLaser());
        }

        private IEnumerator CallMakeLaser()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                MakeLaser();
            }
        }
        
        private void MakeLaser()
        {
            var startCenter = transform.position + transform.right * 1f;
            var hits = GetLaserHits(startCenter);
            float size = _maxRange;

            if (hits.Any())
            {
                var closestHit = hits.Distinct().OrderBy(hit => Vector2.Distance(hit.transform.position, transform.position)).First();

                size = Vector2.Distance(startCenter, closestHit.transform.position) - 0.6f;
            }

            _laser.transform.position = startCenter + transform.right * size / 2;
            _laser.transform.localScale = new Vector3(size, 1, 1);
        }

        private List<GameObject> GetLaserHits(Vector3 startCenter)
        {
            var hits = new List<GameObject>();

            for (int i = 0; i < _precision; i++)
            {
                var startPosition = startCenter - (0.5f * transform.up) + ((1f / _precision) * i * transform.up);
                var hit = Physics2D.Raycast(startPosition, transform.right, 100, _layerMask);

                if (hit.collider)
                {
                    hits.Add(hit.collider.gameObject);
                }
            }
            return hits;
        }
    }
}
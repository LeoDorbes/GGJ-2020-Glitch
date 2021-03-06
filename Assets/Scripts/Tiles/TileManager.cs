using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class TileManager : Singleton<TileManager>
    {
        public List<TileEntity> Tiles { get; set; } = new List<TileEntity>();

        public float BugRatio { get; set; } = 0;

        public void addBugs(float bugRatio)
        {
            BugRatio += bugRatio;
            foreach (var tile in Tiles)
            {
                if (Random.value < bugRatio)
                {
                    tile.SetBugState();
                }
            }
        }

        public void SetBugs()
        {
            foreach (var tile in Tiles)
            {
                if (Random.value < BugRatio)
                {
                    tile.SetBugState();
                }
            }
        }

        private void Start()
        {
            SceneManager.sceneUnloaded += arg0 => Tiles.Clear();
            SceneManager.sceneLoaded += (arg0, mode) => StartCoroutine(SetBugsCoroutine());
        }

        private IEnumerator SetBugsCoroutine()
        {
            yield return new WaitForEndOfFrame();
            SetBugs();
        }
    }
}
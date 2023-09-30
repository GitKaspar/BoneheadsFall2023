using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Skriptid
{
    public class CollisionTracker : MonoBehaviour
    {
        private readonly List<GameObject> _collisions = new();

        private const string TagKlots = "Klots";

        private void OnCollisionEnter2D(Collision2D other)
        {
            print("ENTER");
            var go = other.gameObject;
            if (go.CompareTag(TagKlots))
            {
                _collisions.Add(other.gameObject);
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            print("EXIT");
            _collisions.Remove(other.gameObject);
        }

        public bool IsCollidingWithOtherBlocks()
        {
            return _collisions.Any();
        }

    }
}
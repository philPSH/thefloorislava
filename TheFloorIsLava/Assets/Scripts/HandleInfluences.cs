using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class HandleInfluences : MonoBehaviour
    {
        private PlatformerCharacter2D character;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        public void SteamBoost(float amount)
        {
            character.SetVerticalVelocity(amount);
        }

        public void FireBallBoost(Vector2 amount)
        {
            character.SetHorizontalVelocity(amount.x);
            character.SetVerticalVelocity(amount.y);
        }
    }
}

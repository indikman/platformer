using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class Player : Character
    {
        InputManager inputManager;

        public override void Start()
        {
            inputManager = FindObjectOfType<InputManager>();
            base.Start();
        }

        public override void Update()
        {
            if(inputManager == null) return;

            SetHorizontal(inputManager.Move.x);
            SetJumpPressed(inputManager.isJumpPressed);

            base.Update();
        }
    }
}

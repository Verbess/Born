using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using JackUtil;

namespace PixelGGJNS {

    public class ActorController : MonoBehaviour {

        public Vector2 moveAxis;
        Player player;

        void Awake() {
            player = ReInput.players.GetSystemPlayer();
        }

        public void Execute() {

            if (player == null) {
                return;
            }

            moveAxis.x = player.GetAxisRaw("UIHorizontal");
            moveAxis.y = player.GetAxisRaw("UIVertical");

        }
    }
}
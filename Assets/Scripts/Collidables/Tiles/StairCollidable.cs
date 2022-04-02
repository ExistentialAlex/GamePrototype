using Prototype.Common;
using Prototype.GameGeneration;
using Prototype.GameGeneration.Rooms;
using System;
using UnityEngine;

namespace Prototype.Collidables.Tiles
{
    public class StairCollidable : Collidable
    {
        protected override void OnCollide(Collider2D collider)
        {
            MovePlayerToLinkedStair(collider);
        }

        /// <summary>
        /// Move the player to the next stair room based on the current stair room.
        /// </summary>
        /// <param name="collision">The tile that the player collided with.</param>
        private void MovePlayerToLinkedStair(Collider2D collision)
        {
            if (IsCollidingWithPlayer(collision) || !DungeonGenerator.Instance.PlayerReady)
            {
                return;
            }

            DungeonGenerator.Instance.PlayerReady = false;

            GameObject parent = gameObject.transform.parent.gameObject;
            Vector3 parentVector = parent.transform.position;

            StairRoom currentStair = default;

            // Find the current stair
            foreach (Floor floor in DungeonGenerator.Instance.CurrentLevel.Floors)
            {
                foreach (StairRoom stair in floor.Stairs)
                {
                    if (stair.GlobalVectorPosition == parentVector)
                    {
                        currentStair = stair;
                        break;
                    }
                }
            }

            // if we haven't found the stair the player stepped on then exit
            if (currentStair == null)
            {
                Debug.Log("Could not find current stair.");
                DungeonGenerator.Instance.PlayerReady = true;
                return;
            }

            StairRoom nextStair = currentStair.StairPair;
            Vector3 teleportPos = new Vector3(nextStair.GlobalVectorPosition.x + (Room.RoomWidth / 2), nextStair.GlobalVectorPosition.y + (Room.RoomHeight / 2));

            Debug.Log("Moving player to stair: " + teleportPos.x + ":" + teleportPos.y);

            collision.transform.position = teleportPos;
        }
    }
}

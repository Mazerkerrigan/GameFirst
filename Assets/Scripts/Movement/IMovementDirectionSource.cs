﻿using UnityEngine;

namespace game_one.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
        bool HasAcceleration { get; }
        float AccelerationMultiplier { get; }
    }
}

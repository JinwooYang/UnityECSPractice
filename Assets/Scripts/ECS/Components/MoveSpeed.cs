using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Shooter.ECS
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }

    [DisallowMultipleComponent]
    public class MoveSpeedProxy : ComponentDataProxy<MoveSpeed> { }
}

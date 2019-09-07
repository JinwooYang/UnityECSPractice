using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Shooter.ECS
{
    public class MovementSystem : JobComponentSystem
    {
        [BurstCompile]
        struct MovementSystemJob : IJobForEach<Translation, Rotation, MoveSpeed>
        {
            public float topBound;
            public float bottomBound;
            public float deltaTime;


            public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref MoveSpeed speed)
            {
                var value = translation.Value;
                value += deltaTime * speed.Value * forward(rotation.Value);

                if (value.z < bottomBound)
                {
                    value.z = topBound;
                }

                translation.Value = value;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            if (GameManager.GM != null)
            {
                var moveJob = new MovementSystemJob()
                {
                    topBound = GameManager.GM.topBound,
                    bottomBound = GameManager.GM.bottomBound,
                    deltaTime = Time.deltaTime
                };

                return moveJob.Schedule(this, inputDependencies);
            }
            return inputDependencies;
        }
    }
}

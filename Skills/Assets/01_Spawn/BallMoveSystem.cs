﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System;
using Unity.Jobs;

namespace _01Spawn
{
    /// <summary>
    /// 开一个子线程
    /// </summary>
    public class BallMoveSystem : JobComponentSystem
    {

        struct MoveJob : IJobProcessComponentData<Translation, BallMoveSpeed>
        {
            public float time;
            public void Execute(ref Translation pos, ref BallMoveSpeed speed)
            {
                pos.Value.y = math.sin(time * speed.Value) * 5;
            }
        }
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new MoveJob() { time = Time.timeSinceLevelLoad };
            var handle = job.Schedule(this);
            return handle;
        }
    }
}
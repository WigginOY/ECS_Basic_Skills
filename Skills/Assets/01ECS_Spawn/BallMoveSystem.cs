﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System;
using Unity.Jobs;



public class BallMoveSystem : JobComponentSystem
{

    struct MoveJob : IJobProcessComponentData<Position, BallMoveSpeed>
    {
        public float time;
        public void Execute(ref Position pos, ref BallMoveSpeed speed)
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

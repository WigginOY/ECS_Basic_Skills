﻿using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

/// <summary>
/// 启动程序类
/// </summary>
public class Bootstrap : MonoBehaviour
{
    public int maxCount = 10000;
    public float maxSpeed = 5;
    public float Range = 100;

    public GameObject entityPrefab;

    public Button spawnBtn;
    public Button destroyBtn;
    public Text info;

    EntityManager entityManager;
    int ballCount = 0;

    private void Start()
    {
        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        spawnBtn.onClick.AddListener(() => { spawnEntities(1000); });
        destroyBtn.onClick.AddListener(() => { });
    }


    /// <summary>
    /// Spawn a certain number of Entities；产生count个实体
    /// </summary>
    /// <param name="count">count to spawn</param>
    void spawnEntities(int count)
    {
        Entity entity;
        Vector2 circle;

        #region Record the count of spawned Entities 记录现有实体数量
        if (ballCount >= maxCount)
        {
            return;
        }
        else if (ballCount + count > maxCount)
        {
            count = maxCount - ballCount;
            ballCount = maxCount;
        }
        else
            ballCount += count;

        info.text = "Entities:" + ballCount.ToString();
        #endregion

        for (int i = 0; i < count; i++)
        {
            entity = entityManager.Instantiate(entityPrefab);

            circle = UnityEngine.Random.insideUnitCircle * Range;

            Position pos = new Position()
            {
                Value = new float3(circle.x, 0, circle.y)
            };

            BallMoveSpeed speed = new BallMoveSpeed()
            {
                Value = UnityEngine.Random.Range(1, maxSpeed)
            };
            entityManager.SetComponentData(entity, pos);
            entityManager.SetComponentData(entity, speed);
        }
    }


}

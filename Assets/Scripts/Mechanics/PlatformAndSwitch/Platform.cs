using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform[] Points;
    [SerializeField][Range(0f, 10f)] private float moveSpeed;
    [SerializeField] private bool TriggerablePlatform;

    [NonSerialized] public bool IsPlayerOnboard;

    private void Awake()
    {
        foreach (Transform p in Points)
        {
            p.parent = null;
        }
    }

    private enum OnedDirection
    {
        Forward,
        Backward
    }

    private OnedDirection moveDirection;
    private int pointIndex;

    // Update is called once per frame
    void Update()
    {
        if (TriggerablePlatform)
        {
            if (IsPlayerOnboard)
            {
                pointIndex = 1;
                OnMove();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Points[pointIndex].position, moveSpeed * Time.deltaTime);
                pointIndex = 0;
            }
        }
        else
        {
            OnMove();
        }
    }

    private void OnMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, Points[pointIndex].position, moveSpeed * Time.deltaTime);

        if (transform.position == Points[pointIndex].position)
        {
            if (pointIndex >= Points.Length - 1)
            {
                moveDirection = OnedDirection.Backward;
            }

            if (pointIndex <= 0)
            {
                moveDirection = OnedDirection.Forward;
            }

            switch (moveDirection)
            {
                case OnedDirection.Forward:
                    pointIndex++;
                    break;
                case OnedDirection.Backward:
                    pointIndex--;
                    break;
            }
        }
    }
}

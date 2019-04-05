using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnit
{
    public class Unit_MovementModule : Unit_Module
    {
        public override void SetModule(object _data = null)
        {
            status_M = (Unit_StatusModule)_data;
            bArrivalTarget = true;
            curMoveRootIndex = -1;
            bEndMove = false;
        }

        [Header("Pos & move flag")]
        int curMoveRootIndex = -1;
        public Vector2 curNodePos;
        public Vector2 targetNodePos;
        /// <summary>
        /// 현재 목표지에 도착
        /// </summary>
        public bool bArrivalTarget;
        /// <summary>
        /// 모든 이동 종료
        /// </summary>
        public bool bEndMove;

        [Header("Move")]
        Vector2 moveVelocity = Vector2.zero;
        float realDistance = 0;
        

        public void MoveUpdate()
        {
            if (status_M.bEndTurn)
                return;
            if (bEndMove == true)
                return;

            // target Check
            if (bArrivalTarget == true)
                SetTargetPos();

            // move
            if (bEndMove == false)
                MovePos();

            status_M.bEndTurn = true;
        }

        void SetTargetPos()
        {
            if (curMoveRootIndex == -1)
            {
                curMoveRootIndex = 0;
                status_M.nodePos = status_M.moveRoot[0];
                curNodePos = Unit_StatusModule.mapData.nodeList[status_M.moveRoot[0]].pos;
                targetNodePos = Unit_StatusModule.mapData.nodeList[status_M.moveRoot[1]].pos;
                realDistance = Vector2.Distance(curNodePos, targetNodePos);
            }
            else if(status_M.moveRoot.Count > (curMoveRootIndex + 2))
            {
                try
                {
                    ++curMoveRootIndex;
                    status_M.nodePos = status_M.moveRoot[curMoveRootIndex];
                    curNodePos = Unit_StatusModule.mapData.nodeList[status_M.moveRoot[curMoveRootIndex]].pos;
                    targetNodePos = Unit_StatusModule.mapData.nodeList[status_M.moveRoot[curMoveRootIndex + 1]].pos;
                    realDistance = Vector2.Distance(curNodePos, targetNodePos);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
            else
            {
                bEndMove = true;
                return;
            }
            moveVelocity = Vector2.zero;
            bArrivalTarget = false;
        }

        void MovePos()
        {
            float moveSpeed = status_M.mainStatus.MoveSpeed + status_M.bufStatus.MoveSpeed;
            gameObject.transform.position = Vector2.SmoothDamp(gameObject.transform.position, targetNodePos,ref moveVelocity, realDistance / moveSpeed);
            float distance = Vector2.Distance(gameObject.transform.position, targetNodePos);
            if (distance<= 0.01f && distance >= -0.01f)
            {
                gameObject.transform.position = targetNodePos;
                bArrivalTarget = true;
            }
        }

    }
}
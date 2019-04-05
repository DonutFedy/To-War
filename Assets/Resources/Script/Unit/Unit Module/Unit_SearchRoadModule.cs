using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnit
{
    public class Unit_SearchRoadModule : Unit_Module
    {
        [SerializeField]
        public override void SetModule(object _data = null)
        {
            status_M = (Unit_StatusModule)_data;
            SearchRoad();
        }
        

        public void SearchRoad()
        {
            // 탐욕 알고리즘 이용.

            int curNodePos = status_M.nodePos;

            // 현재 노드
            SearchNode curNode = new SearchNode(status_M.nodePos,0,null);

            // 방문한 노드 list
            List<SearchNode> vistedNodeList = new List<SearchNode>();

            // 갈수 있는 노드 스택
            List<SearchNode> vistableStackNodeList = new List<SearchNode>();

            int SearchCount = 0;
            // 현재 노드가 목적지라면 break
            while (Unit_StatusModule.mapData.nodeList[curNode.indexOfNode].nodeType != NodeType.EndNode && SearchCount < Unit_StatusModule.mapData.nodeList.Count )
            {
                ++SearchCount;
                vistedNodeList.Add(curNode);
                // 갈수 있는 노드를 스택에 추가
                PushRoad(curNode,vistableStackNodeList,vistedNodeList);
                // 최단 노드 pop
                curNode = PopMinDistanceNode(vistableStackNodeList);
            }

            status_M.moveRoot.Clear();
            while (curNode.prevNode != null)
            {
                status_M.moveRoot.Add(curNode.indexOfNode);
                curNode = curNode.prevNode;
            }
            status_M.moveRoot.Add(curNode.indexOfNode);
            status_M.moveRoot.Reverse();
        }

        void PushRoad(SearchNode curNode,List<SearchNode> stack, List<SearchNode> visted)
        {
            List<NodeDistance> arroundNodes = Unit_StatusModule.mapData.nodeList[curNode.indexOfNode].rootList;

            for(int i = 0; i < arroundNodes.Count; ++i)
            {
                int curNewNode = arroundNodes[i].targetNode.indexOfNode;
                float curWeightValue = arroundNodes[i].distance + curNode.weightedValue;
                SearchNode targetNode = null;
                if ((targetNode = stack.Find(x => x.indexOfNode == curNewNode) )!= null)
                {
                    if(targetNode.weightedValue>= curWeightValue)
                    {
                        targetNode.weightedValue = curWeightValue;
                        targetNode.prevNode = curNode;
                    }
                }
                else if((targetNode = visted.Find(x => x.indexOfNode == curNewNode)) != null)
                {
                    if (targetNode.weightedValue >= curWeightValue)
                    {
                        targetNode.weightedValue = curWeightValue;
                        targetNode.prevNode = curNode;
                    }
                }
                else
                {
                    targetNode = new SearchNode(curNewNode,curWeightValue,curNode);
                    stack.Add(targetNode);
                }
            }

        }

        SearchNode PopMinDistanceNode(List<SearchNode> stack)
        {
            SearchNode resultNode = null;

            for(int i = 0; i < stack.Count; ++i)
            {
                if( resultNode == null || stack[i].weightedValue < resultNode.weightedValue)
                {
                    resultNode = stack[i];
                    stack.RemoveAt(i);
                }
            }

            return resultNode;
        }

        
    }

    /// <summary>
    /// 목적지를 찾기 위한 노드
    /// </summary>
    public class SearchNode
    {
        public int indexOfNode;
        public float weightedValue;
        public SearchNode prevNode;

        public SearchNode(int indexOfNode, float weightedValue, SearchNode prevNode)
        {
            this.indexOfNode = indexOfNode;
            this.weightedValue = weightedValue;
            this.prevNode = prevNode ;
        }
    }

    #region Test
    public enum NodeType
    {
        StartNode, EndNode, MiddleNode
    }
    public class MapData
    {
        public List<Node> nodeList;

        public MapData(List<Node> nodeList)
        {
            this.nodeList = nodeList;
        }
    }
    public class Node
    {
        public int indexOfNode;
        public NodeType nodeType;
        public Vector2 pos;
        public List<NodeDistance> rootList;

        public Node(int indexOfNode, NodeType nodeType, Vector2 pos)
        {
            this.indexOfNode = indexOfNode;
            this.nodeType = nodeType;
            this.pos = pos;
        }
    }
    public class NodeDistance
    {
        public float distance;
        public Node targetNode;

        public NodeDistance(Node sourceNode,Node targetNode)
        {
            this.distance = Vector2.Distance(sourceNode.pos, targetNode.pos);
            this.targetNode = targetNode ;
        }
    }
    #endregion
}
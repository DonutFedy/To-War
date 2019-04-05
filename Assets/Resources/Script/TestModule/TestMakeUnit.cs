using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnit
{
    public class TestMakeUnit : MonoBehaviour
    {
        static TestMakeUnit instance;
        public static TestMakeUnit Instance { get => instance; }

        public List<UnitController> unitList;
        public GameObject unitParent;

        public GameObject unitPrefab;

        public bool bMakeMode_Enemy;

        [Header("Road")]
        public GameObject roadPrefab;
        public List<GameObject> roadList = new List<GameObject>();
        public GameObject roadParent;

        [Header("Root")]
        public List<GameObject> rootList = new List<GameObject>();

        private void Start()
        {
            if(instance == null)
            {
                instance = this;
                bMakeMode_Enemy = false;
                if (Unit_StatusModule.mapData == null)
                    SetMapDataBeta();
            }
            else
            {
                DestroyImmediate(this.gameObject);
            }
        }


        public void MakeUnit(Vector2 _unitpos, int _nodeIndex)
        {
            UnitController newUnit = Instantiate(unitPrefab,_unitpos,new Quaternion(), unitParent.transform).GetComponent<UnitController>();

            if (bMakeMode_Enemy)
            {
                newUnit.SetUnit(new UnitSetData(DecType.Minion, BattleType.Defence, _nodeIndex, 1, unitList.Count));
            }
            else
            {
                newUnit.SetUnit(new UnitSetData(DecType.Minion, BattleType.Offence, _nodeIndex, 0, unitList.Count));
            }

            unitList.Add(newUnit);
        }

        public void OnMouseDownTest(Vector2 _pos, int _nodeIndex)
        {
            if (Management.MenuGameManager.Instance == null || Management.MenuGameManager.Instance.DataArea == null)
                return;
            Debug.Log("makeUnit : " + (unitList.Count+1));
            MakeUnit(_pos, _nodeIndex);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                foreach (var val in unitList)
                    if(val.status_M.bAlive)
                        val.UnitUpdate();
            }
        }
        #region Test



        void SetMapDataBeta()
        {
            Debug.Log("맵의 데이터는 여기서 대충 세팅");
            List<Node> mapNodeList = new List<Node>();

            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.StartNode, rootList[0].transform.position)); // 0 node : -> 1 -> 3 -> 5 - > 6(end), -> 2 -> 4 -> 6(end)
            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.MiddleNode, rootList[1].transform.position)); // 1 node : -> 3 -> 5 - > 6(end)
            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.MiddleNode, rootList[2].transform.position)); // 2 node : -> 4 -> 6(end)
            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.MiddleNode, rootList[3].transform.position)); // 3 node : -> 5 -> 6(end)
            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.MiddleNode, rootList[4].transform.position)); // 4 node : -> 6(end)
            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.MiddleNode, rootList[5].transform.position)); // 5 node : -> 6(end)
            mapNodeList.Add(new Node(mapNodeList.Count, NodeType.EndNode, rootList[6].transform.position)); // 6 node : end
            mapNodeList[0].rootList = new List<NodeDistance>() { new NodeDistance(mapNodeList[0], mapNodeList[1]), new NodeDistance(mapNodeList[0], mapNodeList[2]) };
            mapNodeList[1].rootList = new List<NodeDistance>() { new NodeDistance(mapNodeList[1], mapNodeList[3]) };
            mapNodeList[3].rootList = new List<NodeDistance>() { new NodeDistance(mapNodeList[3], mapNodeList[5]) };
            mapNodeList[5].rootList = new List<NodeDistance>() { new NodeDistance(mapNodeList[5], mapNodeList[6]) };
            mapNodeList[2].rootList = new List<NodeDistance>() { new NodeDistance(mapNodeList[2], mapNodeList[4]) };
            mapNodeList[4].rootList = new List<NodeDistance>() { new NodeDistance(mapNodeList[4], mapNodeList[6]) };


            // make road
            for (int i = 0; i < mapNodeList.Count; ++i)
            {
                var curList = mapNodeList[i].rootList;
                if (curList == null)
                    continue;
                for (int j = 0; j < curList.Count; ++j)
                {
                    var newRoad = Instantiate(TestMakeUnit.Instance.roadPrefab, ((Vector3)mapNodeList[i].pos), new Quaternion(), TestMakeUnit.Instance.roadParent.transform);
                    TestMakeUnit.Instance.roadList.Add(newRoad);
                    Vector3 direction = ((Vector3)curList[j].targetNode.pos) - (Vector3)mapNodeList[i].pos;
                    direction.z = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
                    direction.x = 0;
                    direction.y = 0;
                    newRoad.transform.Rotate(direction);
                    float distance = Vector2.Distance(mapNodeList[i].pos, curList[j].targetNode.pos) - 1;
                    Vector3 scale = newRoad.transform.localScale + Vector3.right * distance;
                    newRoad.transform.localScale = scale;
                }
            }

            Unit_StatusModule.mapData = new MapData(mapNodeList);
        }

        #endregion
    }
}
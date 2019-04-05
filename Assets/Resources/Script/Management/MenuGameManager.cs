using GameDataBase;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class MenuGameManager : MonoBehaviour
    {
        public Text errorText;

        static MenuGameManager instance;
        [SerializeField]
        DataArea dataArea;

        public static MenuGameManager Instance { get => instance; set => instance = value; }
        public DataArea DataArea { get => dataArea; set => dataArea = value; }


        private void Awake()
        {
            if( instance != null)
            {
                DestroyImmediate(this);
                return;
            }
            else
            {
                instance = this;
            }

        }

        public void SaveData()
        {
            string basicPath = "";
#if UNITY_STANDALONE_WIN||UNITY_EDITOR
            basicPath = Application.streamingAssetsPath;
#else
            basicPath = "jar:file://" + Application.dataPath + "!/assets";
#endif
            string Json = JsonUtility.ToJson(dataArea, true);
            File.WriteAllText(basicPath + DataPath.defaultPath, Json);
        }

    }
}
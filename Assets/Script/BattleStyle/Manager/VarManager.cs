using Script.Core.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.BattleStyle.Manager
{
    public class VarManager : ManagerBase<VarManager>
    {
        public TMP_InputField inputField { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            inital();
        }
        void OnEnable()
        {
            SceneManager.sceneLoaded += FindInputField;
        }

        void FindInputField(Scene scene, LoadSceneMode mode)
        {
            inital();
        }

        void inital()
        {
            inputField = FindFirstObjectByType<TMP_InputField>();
            inputField.text = "10";
        }
    }
}
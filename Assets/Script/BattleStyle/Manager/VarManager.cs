using Script.Core.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.BattleStyle.Manager
{
    public class VarManager : ManagerBase<VarManager>
    {
        public static int cost = 10;
        private TMP_InputField inputField { get; set; }

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

        void OnDisable()
        {
            SceneManager.sceneLoaded -= FindInputField; 
        }

        void FindInputField(Scene scene, LoadSceneMode mode)
        {
            inital();
        }

        void inital()
        {
            inputField = FindFirstObjectByType<TMP_InputField>();
            
            if (inputField != null)
            {
                inputField.text = cost.ToString();

                inputField.onValueChanged.RemoveAllListeners(); // 중복 방지
                inputField.onValueChanged.AddListener(UpdateCostValue);
            }
            else
            {
                Debug.LogWarning("TMP_InputField not found in this scene.");
            }
        }

        void UpdateCostValue(string value)
        {
            if (int.TryParse(value, out int result))
            {
                cost = result;
            }
        }
    }
}
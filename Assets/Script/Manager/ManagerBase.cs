using UnityEngine;

namespace Script.Manager
{
    public class ManagerBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Manager { get; private set; }

        protected virtual void Awake()
        {
            GameObject managerObject = GameObject.Find("Manager");
            if (managerObject == null)
            {
                managerObject = new GameObject();
                managerObject.name = "Manager";
            }
            
            DontDestroyOnLoad(GameObject.Find("Manager"));

            if (!managerObject.TryGetComponent(out T manager) && Manager == null)
            {
                managerObject.AddComponent<T>();
                Destroy(gameObject);
            }

            Manager = GameObject.Find("Manager").GetComponent<T>();
        }
    }
}
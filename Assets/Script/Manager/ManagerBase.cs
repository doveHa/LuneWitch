using UnityEngine;

namespace Script.Manager
{
    public class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
    {
        public static T Manager { get; private set; }

        protected virtual void Awake()
        {
            GameObject managerObject = GameObject.Find("Manager");
            if (managerObject == null)
            {
                managerObject = new GameObject("Manager");
                DontDestroyOnLoad(managerObject);
            }

            if (Manager != null && Manager != this)
            {
                Destroy(gameObject);
                return;
            }

            if (!managerObject.TryGetComponent(out T manager))
            {
                manager = managerObject.AddComponent<T>();
            }

            Manager = manager;
        }
    }
}
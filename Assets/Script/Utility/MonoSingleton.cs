using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantA.Utils
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T m_Instance = null;
        public static T Instance
        {
            get 
            {
                if( m_Instance != null )
                {
                    m_Instance = new GameObject("Temp of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }

                if(!_isInitialized)
                {
                    _isInitialized = true;
                    m_Instance.Init();
                }

                return m_Instance;
            }
        }

        private static bool _isInitialized;

        private void Awake()
        {
            if(!m_Instance)
            {
                m_Instance = this as T;
            }
            else if(m_Instance != this)
            {
                DestroyImmediate(this);
                return ;
            }
            if(!_isInitialized)
            {
                DontDestroyOnLoad(this.gameObject);
                _isInitialized = true;
                m_Instance.Init();
            }
        }
        
        public virtual void Init() {}
        private void OnApplicationQuit()
        {
            m_Instance = null;
        }
    }
}
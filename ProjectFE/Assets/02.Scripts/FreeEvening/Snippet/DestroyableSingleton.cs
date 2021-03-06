﻿using UnityEngine;
using System.Reflection;
using System;

namespace FreeEvening.Snippet
{
    /// <summary>소멸(Destroyable)되는 singleton class</summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DestroyableSingleton<T> : SingletonBase where T : DestroyableSingleton<T>
    {
        private static object mLock = new object();
        private static T mInstance;
        
#region - Properties
		/// <summary>Destroyable Instance</summary>
		/// <returns>Destroyable Instance</returns>
        public static T Instance
        {
            get
            {
                if (IsAppQuitting)
                {
                    Debug.LogWarning("return null - can't get Instance because application is quitting");
                    return null;
                }
                lock (mLock)
                {
                    Type _type = typeof(T);
                    ConstructorInfo[] ctors = _type.GetConstructors();
                    if (ctors.Length > 0)
                    {
                        Debug.LogError(String.Format("{0} has one or more constructors.", _type.Name));
                    }

                    if (mInstance == null)
                    {
                        mInstance = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogWarning(String.Format("{0}(singleton) must exist only one.", _type));
                            return mInstance;
                        }
                        if (mInstance == null)
                        {
                            GameObject singleton = new GameObject();
                            singleton.transform.parent = DestroyableTap.transform;
                            mInstance = singleton.AddComponent<T>();
                            singleton.name = typeof(T).ToString();
                            mInstance.Init();
                            Debug.Log(String.Format("{0}(singleton) was created", _type));
                        }
                    }
                    return mInstance;
                }
            }
        }
#endregion

#region - MonoBehaviour Methods
        void Awake()
        {
            lock (mLock)
            {
                if (mInstance == null)
                {
                    mInstance = this as T;
                    mInstance.Init();
                }
            }
        }
#endregion

#region - public Methods
        /// <summary>
        /// singleton instance를 제거
        /// instance가 0이 되면 Tap(GameObject) 제거
        /// </summary>
        public override void DestroySingletonInstance()
        {
            string _instanceName = Instance.name;
            Transform _tran = DestroyableTap.transform.FindChild(_instanceName);
            if (_tran != null)
            {
                DestroyImmediate(_tran.gameObject);
                Debug.Log(String.Format("{0}(singleton instance) was destroyed", _instanceName));
            }
            else
            {
                Debug.LogWarning(String.Format("{0}(singleton instance) does not exist", _instanceName));
            }
            if (DestroyableTap.transform.childCount <= 0)
            {
                DestroyImmediate(DestroyableTap);
                Debug.Log("[DestroyableTap] was destroyed");
            }
        }
#endregion

    }
}

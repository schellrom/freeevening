using UnityEngine;

namespace FreeEvening.Snippet
{
    /// <summary>
    /// singleton 생성을 위한 base class
	/// 하나의 GameObject(Tap)에서 관리
    /// </summary>
	public abstract class SingletonBase : BaseBehaviour
	{
		private static GameObject mDontDestroyTap = null;
		private static GameObject mDestroyableTap = null;
		
#region - Properties
		/// <summary>DontDestroyTap</summary>
		/// <returns>DontDestroyTap</returns>
		protected static GameObject DontDestroyTap
		{
			get
			{
				if (mDontDestroyTap == null)
				{
					mDontDestroyTap = new GameObject("[DontDestroyTap]");
					mDontDestroyTap.transform.parent = null;
					mDontDestroyTap.transform.position = Vector3.zero;
					GameObject.DontDestroyOnLoad(mDontDestroyTap);
					Debug.Log("[DontDestroyTap] was created");
				}
				return mDontDestroyTap;
			}
		}
		
		/// <summary>DestroyableTap</summary>
		/// <returns>DestroyableTap</returns>
		protected static GameObject DestroyableTap
		{
			get
			{
				if (mDestroyableTap == null)
				{
					mDestroyableTap = new GameObject("[DestroyableTap]");
					mDestroyableTap.transform.parent = null;
					mDestroyableTap.transform.position = Vector3.zero;
					Debug.Log("[DestroyableTap] was created");
				}
				return mDestroyableTap;
			}
		}
#endregion

#region - public Methods
        /// <summary>init method</summary>
		public virtual void Init() {}
        /// <summary>singleton instance를 제거</summary>
		public abstract void DestroySingletonInstance();
#endregion
	}
}

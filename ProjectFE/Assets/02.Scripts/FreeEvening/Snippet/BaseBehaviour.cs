using UnityEngine;

namespace FreeEvening.Snippet
{
    public class BaseBehaviour : MonoBehaviour
    {
        private static bool mIsAppQuitting = false;
        
#region - MonoBehaviour Methods
        void OnApplicationQuit()
        {
            mIsAppQuitting = true;
        }
#endregion

#region - public Methods
        public static bool IsAppQuitting
        {
            get {return mIsAppQuitting;}
        }
#endregion
    }
}

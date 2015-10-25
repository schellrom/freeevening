using UnityEngine;
using System.Collections.Generic;

namespace FreeEvening.Utility
{
    public static class Yielders
    {
        static Dictionary<float, WaitForSeconds> mTimeInterval = new Dictionary<float, WaitForSeconds>(100);
        static WaitForEndOfFrame mEndOfFrame = new WaitForEndOfFrame();
        static WaitForFixedUpdate mFixedUpdate = new WaitForFixedUpdate();
                        
#region - Properties
        public static WaitForEndOfFrame EndOfFrame
        {
            get { return mEndOfFrame; }
        }
        
        public static WaitForFixedUpdate FixedUpdate
        {
            get { return mFixedUpdate; }
        }
        
#endregion

#region - public Methods
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (!mTimeInterval.ContainsKey(seconds))
                mTimeInterval.Add(seconds, new WaitForSeconds(seconds));
            return mTimeInterval[seconds];
        }
#endregion
    }
}

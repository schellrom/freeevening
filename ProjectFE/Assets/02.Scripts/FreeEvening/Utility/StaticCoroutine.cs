using System.Collections;
using FreeEvening.Snippet;

namespace FreeEvening.Utility
{
    /// <summary>static IEnumerator를 실행</summary>
    public class StaticCoroutine : DontDestroySingleton<StaticCoroutine>
    {
#region - public Methods
        /// <summary>Instance에 있는 코루틴을 실행</summary>
        /// <param name="coroutine">IEnumerator</param>
        public static void DoCoroutine(IEnumerator coroutine)
        {
            Instance.StartCoroutine(Instance.Perform(coroutine));
        }
#endregion

#region - private Methods
        IEnumerator Perform(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine);
            DestroySingletonInstance();
        }
#endregion
    }
}

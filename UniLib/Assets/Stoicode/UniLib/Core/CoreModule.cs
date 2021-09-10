using System.Collections;
using UnityEngine;

namespace Stoicode.UniLib.Core
{
    /// <summary>
    /// Core module
    /// </summary>
    public abstract class CoreModule : MonoBehaviour
    {
        /// <summary>
        /// Core Initializer
        /// </summary>
        public virtual IEnumerator Initialize()
        {
            yield return null;
        }

        /// <summary>
        /// Core shutdown
        /// </summary>
        public virtual void Shutdown() { }
    }
}
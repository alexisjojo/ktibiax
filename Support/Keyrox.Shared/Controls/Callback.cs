using System;
using System.Runtime.InteropServices;

namespace System {

    /// <summary>
    /// References a method to be called to the corresponding operation completes.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public delegate void Callback();

    /// <summary>
    /// References a method to be called to the corresponding operation completes.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public delegate void ActionCallback(object[] args);
}

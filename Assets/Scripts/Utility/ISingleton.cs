using UnityEngine;


namespace Utility {
    public interface ISingleton<T>
    {
        public static T Instance { get; protected set; }
    }
}

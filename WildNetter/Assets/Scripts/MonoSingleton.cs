
using UnityEngine;

public interface ISingleton { void Init(); }

public abstract class MonoSingleton<T> : MonoBehaviour , ISingleton where T : Component
{
    public static T _Instance;

    public virtual void Awake()
    {
        if (isActiveAndEnabled)
        {
            if (_Instance == null)
            {
                _Instance = this as T;
            }
            else if (_Instance != this as T)
            {
                Destroy(this);
            }
        }
    }

    public abstract void Init();
}

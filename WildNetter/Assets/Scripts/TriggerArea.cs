using UnityEngine;
public enum TriggerAreaEffect { OpenUI , GoToScene }
public class TriggerArea : MonoBehaviour
{
    bool flag = true;
    [SerializeField] internal int goToScene;

    [SerializeField] TriggerAreaEffect TriggerType;

    private void OnTriggerEnter(Collider other)
    {
        if (flag)
        {
            SetFlag = false;
            SceneHandler._Instance.TriggerNotification(this);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (!flag)
        {
            SetFlag = true;   
        }
    }
    public TriggerAreaEffect GetTriggerType => TriggerType;

    public bool SetFlag 
    {

        set
        {
            if (flag != value)
            {
                flag = value;
            }
        }
    }
}

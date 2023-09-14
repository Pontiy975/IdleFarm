using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    #region Singleton

    private static ProgressManager _instance;
    public static ProgressManager Instance => _instance;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    [SerializeField]
    private List<LockObject> lockedObjects;

    
    public void UnlockNext(LockObject lockable)
    {
        int index = lockedObjects.IndexOf(lockable);
        print(index);

        if (index >= 0 && lockedObjects.Count > index)
        {
            LockObject obj = lockedObjects[index + 1];
            obj.gameObject.SetActive(true);
            obj.Unlock();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MemoryDataManager : MonoBehaviour
{
    #region Singleton
    private static MemoryDataManager instance = null;
    public static MemoryDataManager Instance
    {
        get
        {
            GetInstance();
            return instance;
        }
    }

    public static MemoryDataManager GetInstance()
    {
        if (instance == null)
        {
            instance = new MemoryDataManager();
        }
        return instance;

    }
    #endregion
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public List<GameObject> cardPositions;
    public List<GameObject> clubCards;
    public List<GameObject> diamondCards;
    public List<GameObject> heartCards;
    public List<GameObject> spadeCards;

}

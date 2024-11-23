using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerPrefs.DeleteAll();
    }
}

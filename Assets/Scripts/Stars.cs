using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    [SerializeField] Image starImage;
    [SerializeField] int timeForRemovingStar;
    float elapsedTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        if (seconds >= timeForRemovingStar)
        {
            // opacity of the star image is set to 40%
            starImage.color = new Color(1, 1, 1, 0.4f);
        }
    }
}

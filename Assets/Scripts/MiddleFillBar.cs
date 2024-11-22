using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiddleFillBar : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float fillAmount;
    [SerializeField] private Image rightFillBar;
    [SerializeField] private Image leftFillBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rightFillBar.fillAmount != fillAmount || leftFillBar.fillAmount != fillAmount)
        {
            rightFillBar.fillAmount = fillAmount;
            leftFillBar.fillAmount = fillAmount;
        }
    }

    public void SetFillAmount(float fillAmount)
    {
        this.fillAmount = fillAmount;
    }
}

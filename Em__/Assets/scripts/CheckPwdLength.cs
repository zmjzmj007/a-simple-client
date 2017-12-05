using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPwdLength : MonoBehaviour {
    public UILabel check;
    private UIInput uIInput;
    private void Start()
    {
        uIInput = GetComponent<UIInput>();
    }
    private void Check()
    {
        int count = uIInput.value.Length;
        if(count<=6)
        {
            //显示lable
            check.enabled = true;
        }
        else
        {
            check.enabled = false;
        }

    }
    
	
}

﻿using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("Status indicator : Kono healtbar object nai :@");
        }
            if(healthText==null)
        {
            Debug.LogError("Status indicator : Kono healthtext object nai :@");
        }

    }

        public void SetHealth(int _cur,int _max)
        {
            float _value = (float)_cur / _max;

            healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
            healthText.text = _cur + "/" + _max ;
        }


}

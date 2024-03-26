using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MouthChangeManager : MonoBehaviour
{
    public string ipaString = "";

    public char[] vowelIPAList = new char[29];
    public int [] vowelMouthTypeList = new int [29];
    /* index 에 따른 입 모양
    0 : idle
    1 : ㅏ
    2 : ㅓ
    3 : ㅗ
    4 : ㅜ
    5 : ㅡ
    6 : ㅣ
    7 : ㅔ
    8 : ㅐ
    */
    public bool[] nowMouthShape = new bool[9];
    public bool[] nowMouthType = new bool[11];
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator IPAtoMouthChange()
    {
        WaitForSeconds _newtWaitTime = new WaitForSeconds(0.125f);
        string _ipaString = ipaString;

        string _checkMessage = "";

        for(int i = 0; i < _ipaString.Length; i++)
        {
            _checkMessage += IPAtoMouthType(_ipaString[i]) + " ";
        }
        Debug.Log(_checkMessage);
        return null;
    }

    public void Check()
    {
        StartCoroutine("IPAtoMouthChange");
    }

    private int IPAtoMouthType(char _nowIPA)
    {
        int _mouthType = 0;
        for(int i = 0; i < vowelIPAList.Length; i++)
        {
            if(vowelIPAList[i] == _nowIPA)
            {
                _mouthType = vowelMouthTypeList[i];
                break;
            }
        }

        return _mouthType;
    }

    private void MouthChange_Beta()
    {

    }
}

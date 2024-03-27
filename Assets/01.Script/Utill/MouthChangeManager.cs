using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
public class MouthChangeManager : MonoBehaviour
{
    public string ipaString = "";

    public GameObject[] mouthObjects = new GameObject[9];

    public char[] vowelIPAList = new char[29];
    public int [] vowelMouthTypeList = new int [29];

    /* nowMouthShape 값 에 따른 입 모양
    -1 : 기본 입모양
    0 : idle ( 다문 입 )
    1 : ㅏ
    2 : ㅓ
    3 : ㅗ
    4 : ㅜ
    5 : ㅡ
    6 : ㅣ
    7 : ㅔ
    8 : ㅐ
    */
    public /*bool[]*/ int nowMouthShape = -1;

    /* nowMouthType 값 에 따른 입 움직임
    -1 : 움직임 없음
    0 : idle ( 다문 입 )
    1 : ㅏ
    2 : ㅓ
    3 : ㅗ
    4 : ㅜ
    5 : ㅡ
    6 : ㅣ
    7 : ㅔ
    8 : ㅐ
    9 : ㅟ
    10 : ㅚ
    11 : 띄어쓰기
    */
    public /*bool[]*/ int nowMouthType = -1;
    void Start()
    {
        MouthShapeReset_Hard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IPAtoMouthChange()
    {
        string _ipaString = ipaString;
        Queue<int> _ipaStringToMouthTypeList = new Queue<int>();
        string _checkMessage = "";

        for(int i = 0; i < _ipaString.Length; i++)
        {
            _ipaStringToMouthTypeList.Enqueue(IPAtoMouthType(_ipaString[i]));
            _checkMessage += IPAtoMouthType(_ipaString[i]) + " ";
        }
        Debug.Log(_checkMessage);
        StartCoroutine(MouthChange_Beta(_ipaStringToMouthTypeList));
    }

    private IEnumerator MouthChange_Beta(Queue<int> iSToMtList)
    {
        WaitForSeconds _normalWaitTime = new WaitForSeconds(0.125f);
        WaitForSeconds _extraWaitTimeA = new WaitForSeconds(0.025f);
        WaitForSeconds _extraWaitTimeB = new WaitForSeconds(0.1f);


        int _listLength = iSToMtList.Count;

        for(int i = 0; i < _listLength; i++)
        {
            nowMouthType = iSToMtList.Dequeue();

            // 입 모양 움직임이 9, 10이 아닌 경우 일반적인 입모양 변화
            if(nowMouthType < 9)
            {
                MouthShapeChanging_NormalType(nowMouthType);
                yield return _normalWaitTime;
            }

            // 입 모양 움직임이 9 : ㅟ ( ㅜ + ㅣ ) 일 경우 입모양 변화
            else if (nowMouthType == 9)
            {
                MouthShapeChanging_NormalType(4);
                yield return _extraWaitTimeA;
                MouthShapeChanging_NormalType(6);
                yield return _extraWaitTimeB;
            }

            // 입 모양 움직임이 10 : ㅚ ( ㅗ + ㅔ ) 일 경우 입모양 변화
            else if (nowMouthType == 10)
            {
                MouthShapeChanging_NormalType(3);
                yield return _extraWaitTimeA;
                MouthShapeChanging_NormalType(7);
                yield return _extraWaitTimeB;
            }

            // 입 모양 움직임이 띄어쓰기일 경우 텀 주기
            else if (nowMouthType == 11)
            {
                yield return _normalWaitTime;
            }
        }

        MouthShapeReset_Soft();

        yield return null;
    }

    private void MouthShapeReset_Soft()
    {
        // for(int i = 0; i < mouthObjects.Length; i++)
        // {
        //     mouthObjects[i].SetActive(false);
        //     nowMouthShape[i] = false;
        // }

        mouthObjects[nowMouthShape].SetActive(false);
        nowMouthShape = -1;
        nowMouthType = -1;
    }

    private void MouthShapeReset_Hard()
    {
        // for(int i = 0; i < mouthObjects.Length; i++)
        // {
        //     mouthObjects[i].SetActive(false);
        //     nowMouthShape[i] = false;
        // }

        for(int i = 0; i < mouthObjects.Length; i++)
        {
            mouthObjects[i].SetActive(false);
        }

        nowMouthShape = -1;
        nowMouthType = -1;
    }

    private void MouthShapeChanging_NormalType(int _nextMouthType)
    {
        // 이번이 문장의 처음 입모양 변환일 경우 이전 입 모양 오브젝트의 비활성화를 실행하지 않는다.
        if(nowMouthShape == -1) {}
        else
        {
            // 현재 입 모양 오브젝트를 비활성화
            mouthObjects[nowMouthShape].SetActive(false);
        }
        mouthObjects[_nextMouthType].SetActive(true);
        nowMouthShape = _nextMouthType;
    }

    public void Check()
    {
        IPAtoMouthChange();
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
            else if (_nowIPA == ' ')
            {
                _mouthType = 11;
            }
        }

        return _mouthType;
    }

}

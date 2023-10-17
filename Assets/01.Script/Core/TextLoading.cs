using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextLoading : MonoBehaviour
{
    private const string URL =
        "https://docs.google.com/spreadsheets/d/1NwY0Oc28H4cGVH_NTGm--U2d9il21lZh0--6hEq0cK8/export?format=tsv&gid=64630080";

    private float time = 0;
    private bool isLoading;

    public List<TextInfo> SpriteData = new List<TextInfo>();

    private void Update()
    {
        if (isLoading)
            time += Time.deltaTime;
    }

    [ContextMenu("구글스프레드시트 불러오기")]
    public void LoadText()
    {
        StartCoroutine(GetText());
    }

    public IEnumerator GetText()
    {
        UnityWebRequest _www = UnityWebRequest.Get(URL);

        isLoading = true;
        yield return _www.SendWebRequest();
        isLoading = false;

        string _data = _www.downloadHandler.text;

        string[] _row = _data.Split('\n');
        int _rowSize = _row.Length;
        int _columeSize = _row[0].Split('\t').Length;

        for (int i = 1; i < _rowSize; i++)
        {
            string[] _column = _row[i].Split('\t');

            TextInfo _textInfo = new TextInfo(_column);

            SpriteData.Add(_textInfo);
        }

        LogText();
    }

    private void LogText()
    {
        foreach (var VARIABLE in SpriteData)
        {
            Debug.Log(VARIABLE.conversationLog);
        }
    }
}

public class TextInfo
{
    public string conversationCode;
    public int order;
    public CurrentChapter currentChapter;
    public CurrentTime currentTime;
    public BackgroundCode backgroundCode;
    public string speaker;
    public string conversationLog;

    public AppearanceCharacterCode characterCode_A;
    public CharacterStatus characterStatus_A;
    
    public AppearanceCharacterCode characterCode_B;
    public CharacterStatus characterStatus_B;
    
    public AppearanceCharacterCode characterCode_C;
    public CharacterStatus characterStatus_C;

    public ConditionsForConversationProgress conversationProgress;
    public string nextConversationCode;
    public bool cameraTarget;
    public string cameraProductionID;

    public List<string> option = new List<string>();
    public List<string> codeSequence = new List<string>();

    public TextInfo(params string[] _textData)
    {
        conversationCode = _textData[0];
        order = _textData[1].Trim() != "" ? int.Parse(_textData[1].Trim()) : 0;
        currentChapter = ConvertTextToEnum.ChapterConverter(_textData[2]);
        currentTime = ConvertTextToEnum.TimeConverter(_textData[3]);
        backgroundCode = ConvertTextToEnum.BackgroundCodeConverter(_textData[4]);
        speaker = _textData[5];
        conversationLog = _textData[6];

        characterCode_A = ConvertTextToEnum.CharacterCodeConverter(_textData[7]);
        characterStatus_A = ConvertTextToEnum.CharacterStatusConverter(_textData[8]);
            
        characterCode_B = ConvertTextToEnum.CharacterCodeConverter(_textData[9]);
        characterStatus_B = ConvertTextToEnum.CharacterStatusConverter(_textData[10]);
            
        characterCode_C = ConvertTextToEnum.CharacterCodeConverter(_textData[11]);
        characterStatus_C = ConvertTextToEnum.CharacterStatusConverter(_textData[12]);

        conversationProgress = ConvertTextToEnum.ConditionsForConversationProgressConverter(_textData[13]);
        nextConversationCode = _textData[14];
        cameraTarget = _textData[15] == "TRUE";
        cameraProductionID = _textData[16];
            
        option.Add(_textData[17]);
        codeSequence.Add(_textData[18]);
        option.Add(_textData[19]);
        codeSequence.Add(_textData[20]);
    }
}
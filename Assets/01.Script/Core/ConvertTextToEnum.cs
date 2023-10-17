using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AppearanceCharacterCode
{
    NULL,
    BIYN,
    ROBN,
    KSAE,
    JGWN
}

public enum CharacterStatus
{
    IDLE,
    NGTV,
    PSTV,
    NULL
}

public enum BackgroundCode
{
    InBuilding,
    OutBuilding,
    NULL
}

public enum CurrentChapter
{
    DAY1,
    DAY2,
    DAY3,
    DAY4,
    DAY5,
    DAY6,
    NULL
}

public enum CurrentTime
{
    NULL,
    DAWN,
    MRNG,
    AFTN,
    EVNG,
    NIGT
}

public enum ConditionsForConversationProgress
{
    NULL,
    NEXT,
    CHCE,
    INFO
}

public class ConvertTextToEnum
{
    public static CurrentChapter ChapterConverter(string _currentChapter) => _currentChapter switch
    {
        "DAY1" => CurrentChapter.DAY1,
        "DAY2" => CurrentChapter.DAY2,
        "DAY3" => CurrentChapter.DAY3,
        "DAY4" => CurrentChapter.DAY4,
        "DAY5" => CurrentChapter.DAY5,
        "DAY6" => CurrentChapter.DAY6,
        _ => CurrentChapter.NULL
    };
    
    public static CurrentTime TimeConverter(string _currentTime) => _currentTime switch
    {
        "DAWN" => CurrentTime.DAWN,
        "MRNG" => CurrentTime.MRNG,
        "AFTN" => CurrentTime.AFTN,
        "EVNG" => CurrentTime.EVNG,
        "NIGT" => CurrentTime.NIGT,
        _ => CurrentTime.NULL
    };
    
    public static BackgroundCode BackgroundCodeConverter(string _backgroundCode) => _backgroundCode switch
    {
        "InBuilding" => BackgroundCode.InBuilding,
        "OutBuilding" => BackgroundCode.OutBuilding,
        _ => BackgroundCode.NULL
    };
    
    public static AppearanceCharacterCode CharacterCodeConverter(string _characterCode) => _characterCode switch
    {
        "BIYN" => AppearanceCharacterCode.BIYN,
        "ROBN" => AppearanceCharacterCode.ROBN,
        "KSAE" => AppearanceCharacterCode.KSAE,
        "JGWN" => AppearanceCharacterCode.JGWN,
        _ => AppearanceCharacterCode.NULL
    };

    public static CharacterStatus CharacterStatusConverter(string _characterStatus) => _characterStatus switch
    {
        "IDLE" => CharacterStatus.IDLE,
        "NGTV" => CharacterStatus.NGTV,
        "PSTV" => CharacterStatus.PSTV,
        _ => CharacterStatus.NULL
    };

    public static ConditionsForConversationProgress
        ConditionsForConversationProgressConverter(string _conversationProgress) => _conversationProgress switch
    {
        "CHCE" => ConditionsForConversationProgress.CHCE,
        "INFO" => ConditionsForConversationProgress.INFO,
        "NEXT" => ConditionsForConversationProgress.NEXT,
        _ => ConditionsForConversationProgress.NULL
    };
}

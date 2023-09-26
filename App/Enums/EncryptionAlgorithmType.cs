using System.ComponentModel;

namespace App.Enums;

public enum EncryptionAlgorithmType
{
    [Description("Моно алфавитная подстановка")]
    Mono = 1,
    
    [Description("Цезаря")]
    Caesar = 2,
    
    [Description("Тритемиуса")]
    Tritemius = 3
}
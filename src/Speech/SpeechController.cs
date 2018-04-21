﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speech
{
    public class SpeechController
    {
        public static SpeechEngineInfo[] GetAllSpeechEngine()
        {
            List<SpeechEngineInfo> info = new List<SpeechEngineInfo>();

            // VOICEROID+ を列挙
            var voiceroidPlus = new VoiceroidPlusEnumerator();
            if(voiceroidPlus.GetSpeechEngineInfo().Length > 0)
            {
                info.AddRange(voiceroidPlus.GetSpeechEngineInfo());
            }

            // VOICEROID2 を列挙
            info.AddRange(GetVoiceroid2SpeechEngine());

            // SAPI5 を列挙
            var sapi5 = new SAPI5Enumerator();
            info.AddRange(sapi5.GetSpeechEngineInfo());

            return info.ToArray();
        }

        public static SpeechEngineInfo[] GetVoiceroid2SpeechEngine()
        {
            List<SpeechEngineInfo> info = new List<SpeechEngineInfo>();

            // VOICEROID2 を列挙
            var voiceroid2 = new Voiceroid2Enumerator();
            if (voiceroid2.GetSpeechEngineInfo().Length > 0)
            {
                info.AddRange(voiceroid2.GetSpeechEngineInfo());
            }

            return info.ToArray();
        }

        public static ISpeechEngine GetInstance(string libraryName)
        {
            var info = GetAllSpeechEngine();
            foreach (var e in info)
            {
                if (e.LibraryName == libraryName)
                {
                    return GetInstance(e);
                }
            }
            return null;
        }

        public static Voiceroid2Controller GetVoiceroid2Instance(string libraryName)
        {
            var info = GetVoiceroid2SpeechEngine();
            foreach(var e in info)
            {
                if(e.LibraryName == libraryName)
                {
                    return (Voiceroid2Controller)GetInstance(e);
                }
            }
            return null;
        }

        public static ISpeechEngine GetInstance(SpeechEngineInfo info)
        {
            switch (info.EngineName)
            {
                case VoiceroidPlusEnumerator.EngineName:
                    return new VoiceroidPlusController(info);
                case Voiceroid2Enumerator.EngineName:
                    return new Voiceroid2Controller(info);
                case SAPI5Enumerator.EngineName:
                    return new SAPI5Controller(info);
                default:
                    break;
            }
            return null;
        }
    }
}

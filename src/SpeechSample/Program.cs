using Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 利用可能な音声合成エンジンを列挙
            // Windows 10 (x64) 上での VOICEROID+, VOICEROID2, SAPI5 に対応
            var engines = SpeechController.GetVoiceroid2SpeechEngine();
            foreach(var c in engines)
            {
                Console.WriteLine($"{c.LibraryName},{c.EngineName},{c.EnginePath}");
            }

            // ライブラリ名を入力(c.LibraryName列)
            Console.Write("\r\nLibrary Name:");
            string name = Console.ReadLine().Trim();

            // 対象となるライブラリを実行
            var engine = SpeechController.GetVoiceroid2Instance(name);
            if(engine == null)
            {
                Console.WriteLine($"{name} を起動できませんでした。");
                Console.ReadKey();
                return;
            }
            engine.Finished += Engine_Finished;

            // 音声合成エンジンを起動
            engine.Activate();
            engine.Play("こんにちは");
            engine.SetPitch(1.00f);

            string fileName = "";
            string line = "";
            do
            {
                Console.WriteLine($"保存ファイル名を入力: ");
                fileName = Console.ReadLine();
                Console.WriteLine($"読み上げテキストを入力: ");
                line = Console.ReadLine();
                Console.WriteLine($"Volume: {engine.GetVolume()}, Speed: {engine.GetSpeed()}, Pitch: {engine.GetPitch()}, PitchRange: {engine.GetPitchRange()} to {fileName}");
                engine.Stop(); // 喋っている途中に文字が入力されたら再生をストップ
                if (fileName == "")
                {
                    engine.Play(line);
                }
                else
                {
                    engine.Save(fileName, line);
                }
            } while (line != "");
            engine.Dispose();
        }

        private static void Engine_Finished(object sender, EventArgs e)
        {
            Console.WriteLine("* 再生完了 *");
        }
    }
}

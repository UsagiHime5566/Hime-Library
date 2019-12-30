using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

namespace HimeLib
{
    public class RunBatCmd
    {
        /// <summary>
        /// 建立自動開機開程式bat, 建議程式執行時運行此指令
        /// </summary>
        public static void CreateBatFile()
        {
#if !UNITY_EDITOR
        string companyName = Application.companyName;
        string productName = Application.productName;
        //example : GetDirectoryName('C:\MyDir\MySubDir\myfile.ext') returns 'C:\MyDir\MySubDir'
        //example : GetDirectoryName('C:\MyDir\MySubDir') returns 'C:\MyDir'
        string exePath = Path.GetDirectoryName(Application.dataPath);
        string batName = exePath + "/" + Application.productName + ".bat";
        UnityEngine.Debug.Log(batName);
        var file = File.Open(batName, FileMode.Create, FileAccess.ReadWrite);
        var writer = new StreamWriter(file);
        writer.WriteLine("@echo off");
        writer.WriteLine("echo !!!");
        writer.WriteLine("echo Wait for system prepare...");
        writer.WriteLine("ping 127.0.0.1 -n 10 -w 1000");
        writer.WriteLine("set regkey=\"HKEY_CURRENT_USER\\Software\\" + companyName + "\\" + productName + "\"");
        writer.WriteLine("reg add %regkey% /v \"Screenmanager Resolution Height_h2627697771\" /T REG_DWORD /D 1080 /f");
        writer.WriteLine("endlocal");
        writer.WriteLine(Application.productName + ".exe -screen-width 1920 -screen-height 1080 -screen-fullscreen 1");
        writer.Flush();
        file.Close();
#endif
        }

        /// <summary>
        /// 執行立即重開程式, 需搭配 CreateBatFile() 產生的bat檔案
        /// </summary>
        public void RestartApplication(){
            System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".bat")); //new program
            Application.Quit(); //kill current process
        }
        
        public static void Run(string fileName, string args, string outputDirectory)
        {
            // start the child process
            Process process = new Process();

            // redirect the output stream of the child process.
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = args;

            if (!string.IsNullOrEmpty(outputDirectory))
            {
                process.StartInfo.WorkingDirectory = outputDirectory;
            }
            else
            {
                process.StartInfo.WorkingDirectory = Application.temporaryCachePath; // nb. can only be called on the main thread
            }

            int exitCode = -1;
            string output = null;

            try
            {
                process.Start();

                // do not wait for the child process to exit before
                // reading to the end of its redirected stream.
                // process.WaitForExit();

                // read the output stream first and then wait.
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("Run error" + e.ToString()); // or throw new Exception
            }
            finally
            {
                exitCode = process.ExitCode;

                process.Dispose();
                process = null;
            }

            // process exitCode/output, call onComplete handlers etc.
            // ...
        }
    }
}
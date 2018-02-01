using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace DELL_ISPtool
{
    class MsgProgress
    {
        ResourceManager Rm;
        //public string[] Msg_Temp = { "Preparing monitor for update", "Erasing flash", "Updating", "Verifying update", "Update Successful" };
        //public string[] Msg_Temp = new string[5];
        public enum ProgramInfo : int { Preparing = 0, Erasing = 1, Updating = 2, Verifying = 3, Success = 4 };
        public enum ErrInfo : byte { Normal, MonitorNotDetect = 1 << 0, FileNotFound = 1 << 1, UpdatingNoted = 1 << 2, ProgramFail = 1 << 3, EraseFail = 1 << 4, UpdateError = 1 << 5, ChKError = 1 << 6 };
        
        public int DotCount = 0;
        public MStarISP.ISPStep preISPStep = MStarISP.ISPStep.Finished;

        public bool MonitorNotDetect = false;
        public bool FileNotFound = false;

        public enum Language : int { L_French, L_Spanish, L_German, L_Japanese, L_Portuguese, L_Russian, L_Chinese_S, L_English};

        private string Msg_ErrorStatus = " ";
        private string Msg_Progrmstep = " ";


        public void SwitchUILangauge(Language lang)
        {
            CultureInfo ci = new CultureInfo("en");
            string userUICulture = Thread.CurrentThread.CurrentUICulture.Name;
            
            switch (lang)
            {
                case Language.L_French:
                    ci = new CultureInfo("fr");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
                    break;
                case Language.L_German:
                    ci = new CultureInfo("de");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
                    break;
                case Language.L_Japanese:
                    ci = new CultureInfo("ja");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja");
                    break;
                case Language.L_Portuguese:
                    ci = new CultureInfo("pt-BR");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
                    break;
                case Language.L_Russian:
                    ci = new CultureInfo("be");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("be");
                    break;
                case Language.L_Spanish:
                    ci = new CultureInfo("es");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                    break;
                case Language.L_Chinese_S:
                    ci = new CultureInfo("zh-CN");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                    break;
                default:
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    break;
            }
            Rm = new ResourceManager("DELL_ISPtool.LanguagePack", Assembly.GetExecutingAssembly());
        }

        public string SwitchProgramStringLag(ProgramInfo _steps)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ISP));
            switch(_steps)
            {
                case ProgramInfo.Preparing:
                    Msg_Progrmstep = resources.GetString("p_preparing");
                    break;
                case ProgramInfo.Erasing:
                    Msg_Progrmstep = resources.GetString("p_erasing");
                    break;
                case ProgramInfo.Updating:
                    Msg_Progrmstep = resources.GetString("p_updateing");
                    break;
                case ProgramInfo.Verifying:
                    Msg_Progrmstep = resources.GetString("p_verify");
                    break;
                case ProgramInfo.Success:
                    Msg_Progrmstep = resources.GetString("p_success");
                    break;
            }
            return Msg_Progrmstep;

        }

        public string ReturnStatusString(ErrInfo _status)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ISP));

            switch (_status)
            {
                case ErrInfo.Normal:
                    Msg_ErrorStatus = resources.GetString("s_normal");
                    break;
                case ErrInfo.MonitorNotDetect:
                    Msg_ErrorStatus = resources.GetString("s_notdetect");
                    break;
                case ErrInfo.FileNotFound:
                    Msg_ErrorStatus = resources.GetString("s_notfound");
                    break;
                case ErrInfo.UpdatingNoted:
                    Msg_ErrorStatus = resources.GetString("s_noted");//Rm.GetString("s_noted");
                    break;
                case ErrInfo.ProgramFail:
                    Msg_ErrorStatus = resources.GetString("s_progfail");
                    break;
                case ErrInfo.EraseFail:
                    Msg_ErrorStatus = resources.GetString("s_erasefail");
                    break;
                case ErrInfo.UpdateError:
                    Msg_ErrorStatus = resources.GetString("s_updateerror");
                    break;
                case ErrInfo.ChKError:
                    Msg_ErrorStatus = resources.GetString("s_chkerror");
                    break;
                default:
                    break;
            }
            return Msg_ErrorStatus;
        }

    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DELL_ISPtool {
    using System;
    
    
    /// <summary>
    ///   用於查詢當地語系化字串等的強類型資源類別。
    /// </summary>
    // 這個類別是自動產生的，是利用 StronglyTypedResourceBuilder
    // 類別透過 ResGen 或 Visual Studio 這類工具。
    // 若要加入或移除成員，請編輯您的 .ResX 檔，然後重新執行 ResGen
    // (利用 /str 選項)，或重建您的 VS 專案。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DE_string {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DE_string() {
        }
        
        /// <summary>
        ///   傳回這個類別使用的快取的 ResourceManager 執行個體。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DELL_ISPtool.DE_string", typeof(DE_string).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   覆寫目前執行緒的 CurrentUICulture 屬性，對象是所有
        ///   使用這個強類型資源類別的資源查閱。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查詢類似 Fehler bei der Monitor-Firmware-Prüfsumme. Monitor-Firmware-Aktualisierungsprogramm schließen, Monitor ausschalten, Netzkabel erneut anschließen, Monitor einschalten und Aktualisierung noch einmal versuchen. 的當地語系化字串。
        /// </summary>
        internal static string s_chkerror {
            get {
                return ResourceManager.GetString("s_chkerror", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Fehler beim Löschen des Flash-Speichers. Monitor-Firmware-Aktualisierungsprogramm schließen, Monitor ausschalten, Netzkabel erneut anschließen, Monitor einschalten und Aktualisierung noch einmal versuchen. 的當地語系化字串。
        /// </summary>
        internal static string s_erasefail {
            get {
                return ResourceManager.GetString("s_erasefail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Zum Abschließen der Aktualisierung Monitor ausschalten, Netzkabel wieder anschließen, Monitor einschalten. 的當地語系化字串。
        /// </summary>
        internal static string s_normal {
            get {
                return ResourceManager.GetString("s_normal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Monitor nicht erkannt. Stellen Sie sicher, dass das USB-Kabel angeschlossen und das Display eingeschaltet ist. Weitere Informationen finden Sie in der Hilfe. 的當地語系化字串。
        /// </summary>
        internal static string s_notdetect {
            get {
                return ResourceManager.GetString("s_notdetect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Die Aktualisierung kann mehrere Minuten dauern; während der Aktualisierung dürfen Sie weder den Monitor abschalten noch Kabel trennen. 的當地語系化字串。
        /// </summary>
        internal static string s_noted {
            get {
                return ResourceManager.GetString("s_noted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Firmware-Datei nicht gefunden. Zum Fortfahren die Firmware-Datei (*.bin) öffnen. 的當地語系化字串。
        /// </summary>
        internal static string s_notfound {
            get {
                return ResourceManager.GetString("s_notfound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Schreibschutzfehler. Monitor-Firmware-Aktualisierungsprogramm schließen, Monitor abschalten, Netzkabel erneut anschließen, Monitor einschalten und Aktualisierung noch einmal versuchen. 的當地語系化字串。
        /// </summary>
        internal static string s_progfail {
            get {
                return ResourceManager.GetString("s_progfail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查詢類似 Error de actualización. Cierre la Utilidad de actualización de firmware del monitor, apague el monitor, vuelva a enchufar el cable de alimentación, encienda el monitor y, por último, vuelva a realizar la actualización. 的當地語系化字串。
        /// </summary>
        internal static string s_updateerror {
            get {
                return ResourceManager.GetString("s_updateerror", resourceCulture);
            }
        }
    }
}

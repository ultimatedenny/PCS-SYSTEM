//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PCSSystem.Properties {
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TESTING")]
        public string ServerStatus {
            get {
                return ((string)(this["ServerStatus"]));
            }
            set {
                this["ServerStatus"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public string DLPTime {
            get {
                return ((string)(this["DLPTime"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Sharing\\ajrfiles\\")]
        public string FolderExport {
            get {
                return ((string)(this["FolderExport"]));
            }
            set {
                this["FolderExport"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public string Maxrow {
            get {
                return ((string)(this["Maxrow"]));
            }
            set {
                this["Maxrow"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("600")]
        public string ConTime {
            get {
                return ((string)(this["ConTime"]));
            }
            set {
                this["ConTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=sbm-vmsqlsqa03; Initial Catalog=ADVJEQDB; User Id=sa; Password=admin2" +
            "12")]
        public string ConnString {
            get {
                return ((string)(this["ConnString"]));
            }
            set {
                this["ConnString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=sbm-vmsqlsqa03; Initial Catalog=ADVJEQDB; User Id=sa; Password=admin2" +
            "12")]
        public string ConnStringQA {
            get {
                return ((string)(this["ConnStringQA"]));
            }
            set {
                this["ConnStringQA"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\172.18.100.125\\apps_datastore\\PCS\\QA")]
        public string AppPath {
            get {
                return ((string)(this["AppPath"]));
            }
            set {
                this["AppPath"] = value;
            }
        }
    }
}

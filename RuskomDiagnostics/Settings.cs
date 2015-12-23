using System.ComponentModel;
using System.Configuration;
using RuskomDiagnostics.Annotations;

// ReSharper disable CheckNamespace
namespace RuskomDiagnostics.Properties {
    // ReSharper restore CheckNamespace
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        
        // ReSharper disable EmptyConstructor
        private Settings() {
            // ReSharper restore EmptyConstructor
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private void SettingChangingEventHandler( [NotNull] object sender,
                                                  [NotNull] SettingChangingEventArgs e)
        {
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
            // Add code to handle the SettingChangingEvent event here.
        }

        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private void SettingsSavingEventHandler( [NotNull] object sender,
                                                 [NotNull] CancelEventArgs e)
        {
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
            // Add code to handle the SettingsSaving event here.
        }
    }
}

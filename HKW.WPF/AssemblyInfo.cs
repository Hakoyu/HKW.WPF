using System.Windows;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page,
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
//(used if a resource is not found in the page,
// app, or any theme specific resource dictionaries)
)]

//[assembly: XmlnsDefinition("https://hkw.com/wpf", "HKW.WPF.Triggers")]
[assembly: XmlnsDefinition("https://hkw.com/wpf", "HKW.WPF.Converters")]
[assembly: XmlnsDefinition("https://hkw.com/wpf", "HKW.WPF.Behaviors")]
[assembly: XmlnsDefinition("https://hkw.com/wpf", "HKW.WPF.Helpers")]
[assembly: XmlnsDefinition("https://hkw.com/wpf", "HKW.WPF.MVVMDialogs")]
[assembly: XmlnsDefinition("https://hkw.com/wpf", "HKW.WPF.TypeExtension")]

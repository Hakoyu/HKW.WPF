using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HKW.WPF.Native;

internal partial class PInvoke
{
    [LibraryImport("uxtheme.dll", EntryPoint = "#94")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    public static partial uint GetImmersiveColorSetCount();

    [LibraryImport("uxtheme.dll", EntryPoint = "#95")]
    private static partial uint GetImmersiveColorFromColorSetEx(
        uint dwImmersiveColorSet,
        uint dwImmersiveColorType,
        [MarshalAs(UnmanagedType.Bool)] bool bIgnoreHighContrast,
        uint dwHighContrastCacheMode
    );

    [LibraryImport("uxtheme.dll", EntryPoint = "#96")]
    private static partial uint GetImmersiveColorTypeFromName(IntPtr pName);

    [LibraryImport("uxtheme.dll", EntryPoint = "#98")]
    private static partial int GetImmersiveUserColorSetPreference(
        [MarshalAs(UnmanagedType.Bool)] bool bForceCheckRegistry,
        [MarshalAs(UnmanagedType.Bool)] bool bSkipCheckOnFail
    );

    [LibraryImport("uxtheme.dll", EntryPoint = "#100")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    public static partial IntPtr GetImmersiveColorNamedTypeByIndex(uint index);

    private static List<string> GetAllColorNames()
    {
        var allColorNames = new List<string>();
        for (uint i = 0; i < 0xFFF; i++)
        {
            IntPtr typeNamePtr = GetImmersiveColorNamedTypeByIndex(i);
            if (typeNamePtr != IntPtr.Zero)
            {
                IntPtr typeName = (IntPtr)Marshal.PtrToStructure(typeNamePtr, typeof(IntPtr))!;
                allColorNames.Add(Marshal.PtrToStringUni(typeName)!);
            }
        }

        return allColorNames;
    }

    // Get theme color
    public static Color? GetSystemAccentColor()
    {
        try
        {
            var pElementName = Marshal.StringToHGlobalUni(
                "ImmersiveInputSwitchDarkRadioButtonBackgroundSelected"
            );
            var colorSetEx = GetImmersiveColorFromColorSetEx(
                (uint)GetImmersiveUserColorSetPreference(false, false),
                GetImmersiveColorTypeFromName(pElementName),
                false,
                0
            );
            Marshal.FreeCoTaskMem(pElementName);

            var color = Color.FromArgb(
                (byte)((0xFF000000 & colorSetEx) >> 24),
                (byte)(0x000000FF & colorSetEx),
                (byte)((0x0000FF00 & colorSetEx) >> 8),
                (byte)((0x00FF0000 & colorSetEx) >> 16)
            );

            return color;
        }
        catch
        {
            return null;
        }
    }
}

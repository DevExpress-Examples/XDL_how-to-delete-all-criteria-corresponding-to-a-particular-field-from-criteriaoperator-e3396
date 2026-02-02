Imports System
Imports System.Windows.Forms
Imports DevExpress.Skins

Namespace DXSample

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            DevExpress.UserSkins.BonusSkins.Register()
            Call SkinManager.EnableFormSkins()
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New Main())
        End Sub
    End Module
End Namespace

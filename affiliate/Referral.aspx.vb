Imports System.Threading

Public Class Referral
    Inherits System.Web.UI.Page

    Private Sub Referral_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuReferral")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("mhaUserID") = "A000001"
        Dim hexID As String = ConvertStringToHex(Session("mhaUserID"))
        lblTextLink.Text = "https://localhost:44354/Redirect?x=" & hexID
        lblBannerCode.Text = "&lt;script src='mhAffliate.js' x='" & hexID & "'></script&gt;"
    End Sub

    Public Shared Function ConvertStringToHex(ByVal input As String) As String
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Unicode
        Dim stringBytes As Byte() = encoding.GetBytes(input)
        Dim sbBytes As StringBuilder = New StringBuilder(stringBytes.Length * 2)

        For Each b As Byte In stringBytes
            sbBytes.AppendFormat("{0:X2}", b)
        Next

        Return sbBytes.ToString()
    End Function

    Private Sub btnCopyTextLink_Click(sender As Object, e As EventArgs) Handles btnCopyTextLink.Click
        Dim thread As New Thread(Sub() Windows.Clipboard.SetText(lblTextLink.Text))
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
        thread.Join()
    End Sub

    Private Sub btnCopyBannerLink_Click(sender As Object, e As EventArgs) Handles btnCopyBannerLink.Click
        Dim str As String = lblBannerCode.Text.Replace("&lt;", "<").Replace("&gt;", ">")
        Dim thread As New Thread(Sub() Windows.Clipboard.SetText(str))
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
        thread.Join()
    End Sub
End Class
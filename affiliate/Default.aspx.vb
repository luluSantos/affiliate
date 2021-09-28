Imports System.Data.SqlClient

Public Class _Default
    Inherits Page

    Private Sub Default_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuHome")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


    End Sub

End Class
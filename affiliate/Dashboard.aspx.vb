Public Class Dashboard
    Inherits System.Web.UI.Page

    Private Sub Dashboard_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuDashboard")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session IsNot Nothing) Then
            Dim item As Object = Session("mhaUserID")
            If (item IsNot Nothing) Then
                lblDashboardWelcome.Text = "Welcome back, " & Session("mhaUserFirstName") & " " & Session("mhaUserLastName")
                lblDashboardWelcome2.Text = " [ Your Last Login : " & Session("mhaUserLastLogin") & " ]"
            Else
                Session.Abandon()
                Response.Redirect("Default.aspx")
            End If
        Else
            Session.Abandon()
            Response.Redirect("Default.aspx")
        End If

    End Sub


End Class
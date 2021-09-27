Imports System.Data.SqlClient

Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        menuDashboard.Visible = False
        menuReferrals.Visible = False
        menuPerformance.Visible = False
        menuAccount.Visible = False
        btnLogout.Visible = False
    End Sub

    Private Sub btnSubmitLogin_Click(sender As Object, e As EventArgs) Handles btnSubmitLogin.Click

        Dim password As String = txtPassword.Text.Trim
        If Not String.IsNullOrEmpty(password) Then
            Dim sec As New clsEncryption
            password = sec.EncryptData(password)
        End If
        Dim _temp = AuthenticateUser(txtUsername.Text.Trim, password)
        If _temp Then
            Response.Redirect("Dashboard.aspx")
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
        End If

    End Sub


    Private Function AuthenticateUser(ByVal email As String, ByVal password As String) As Boolean

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT mhaUserID,mhaUserIDNumber,mhaUserEmail,mhaUserCompany,mhaUserFirstName,mhaUserLastName,mhaUserTimeStamp,mhaUserLastLogin,mhaUserCompleteProfile,mhaUserStatus FROM mhaUser WHERE mhaUserEmail=@mhaUserEmail AND mhaUserPassword=@mhaUserPassword AND mhaUserStatus='Active'"
        Dim dt As New DataTable

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserEmail", email)
                sqlCmd.Parameters.AddWithValue("@mhaUserPassword", password)
                sqlConn.Open()
                Dim sdr As New SqlDataAdapter
                sdr.SelectCommand = sqlCmd
                sdr.Fill(dt)
                sqlConn.Close()
            End Using
            'MsgBox(dt.Rows.Count)
            If dt.Rows.Count = 0 Then
                Return False
            Else
                For Each row In dt.Rows
                    Dim sqlQuery2 As String = "Update mhaUser SET mhaUserLastLogin=@mhaUserLastLogin WHERE mhaUserID=@mhaUserID"
                    Using sqlCmd As New SqlCommand(sqlQuery2, sqlConn)
                        sqlCmd.Parameters.AddWithValue("@mhaUserLastLogin", DateTime.Now)
                        sqlCmd.Parameters.AddWithValue("@mhaUserID", row("mhaUserID"))
                        sqlConn.Open()
                        Dim _temp2 = sqlCmd.ExecuteNonQuery()
                        sqlConn.Close()
                    End Using
                    Session("mhaUserID") = row("mhaUserID").ToString
                    Session("mhaUserIDNumber") = row("mhaUserIDNumber").ToString
                    Session("mhaUserEmail") = row("mhaUserEmail").ToString
                    Session("mhaUserCompany") = row("mhaUserCompany").ToString
                    Session("mhaUserFirstName") = row("mhaUserFirstName").ToString
                    Session("mhaUserLastName") = row("mhaUserLastName").ToString
                    Session("mhaUserLastLogin") = row("mhaUserLastLogin").ToString
                    Session("mhaUserCompleteProfile") = row("mhaUserCompleteProfile").ToString
                Next
                Return True
            End If

        Catch ex As Exception
            Return False
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function
End Class
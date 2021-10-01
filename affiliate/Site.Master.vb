Imports MySql.Data.MySqlClient

Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        menuDashboard.Visible = False
        menuReferrals.Visible = False
        menuPerformance.Visible = False
        menuAccount.Visible = False
        btnLogout.Visible = False
    End Sub

    Protected Sub btnSubmitLogin_Click(sender As Object, e As EventArgs) Handles btnSubmitLogin.Click

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

    Protected Sub btnSubmitForgot_Click(sender As Object, e As EventArgs)

        Dim email As String = txtFgEMail.Text.Trim
        Dim emailCheck As Boolean = CheckEmailExists(email)
        Dim result As Boolean = False

        If emailCheck Then
            Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
            Dim sqlQuery As String = "Update mm_af_list SET Password=@Password WHERE Email=@Email"

            Dim password As String = "123mhartanah"
            Dim sec As New clsEncryption
            password = sec.EncryptData(password)

            Try
                Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                    sqlCmd.Parameters.AddWithValue("@Email", email)
                    sqlCmd.Parameters.AddWithValue("@Password", password)
                    sqlConn.Open()
                    sqlCmd.ExecuteNonQuery()
                    sqlConn.Close()
                End Using
                Dim strMessage As String = "Your request to reset password is successful. <br\> Your new password is 123mhartanah. <br\> Please <a href='#'>login</a> and change the password immediately."
                Dim _temp = SendMailSMTPAUTH("mHartanah Password Reset", "mHartanah06@gmail.com", email, strMessage)
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If sqlConn.State = ConnectionState.Open Then
                    sqlConn.Close()
                End If
                sqlConn.Dispose()
            End Try

        End If
    End Sub

    Protected Function AuthenticateUser(ByVal email As String, ByVal password As String) As Boolean

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT AFID,AFLvl,Email,FirstName,LastName,Company,isProfileComplete,LastLogin,IsActive FROM mm_af_list WHERE Email=@Email AND Password=@Password AND IsActive=1"
        Dim dt As New DataTable

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@Email", email)
                sqlCmd.Parameters.AddWithValue("@Password", password)
                sqlConn.Open()
                Dim sdr As New MySqlDataAdapter
                sdr.SelectCommand = sqlCmd
                sdr.Fill(dt)
                sqlConn.Close()
            End Using
            'MsgBox(dt.Rows.Count)
            If dt.Rows.Count = 0 Then
                Return False
            Else
                For Each row In dt.Rows
                    Dim sqlQuery2 As String = "Update mm_af_list SET LastLogin=@LastLogin WHERE AFID=@AFID"
                    Using sqlCmd As New MySqlCommand(sqlQuery2, sqlConn)
                        sqlCmd.Parameters.AddWithValue("@LastLogin", DateTime.Now)
                        sqlCmd.Parameters.AddWithValue("@AFID", row("AFID"))
                        sqlConn.Open()
                        Dim _temp2 = sqlCmd.ExecuteNonQuery()
                        sqlConn.Close()
                    End Using
                    Session("AFID") = row("AFID").ToString
                    Session("AFLvl") = row("AFLvl").ToString
                    Session("Email") = row("Email").ToString
                    Session("FirstName") = row("FirstName").ToString
                    Session("LastName") = row("LastName").ToString
                    Session("Company") = row("Company").ToString
                    Session("isProfileComplete") = row("isProfileComplete").ToString
                    Session("LastLogin") = row("LastLogin").ToString
                Next
                Return True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Protected Function CheckEmailExists(ByVal email As String) As Boolean

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(AFID) FROM mm_af_list WHERE Email=@Email"
        Dim returnCount As Integer = 0

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@Email", email)
                sqlConn.Open()
                returnCount = sqlCmd.ExecuteScalar()
                sqlConn.Close()
            End Using
            'MsgBox(returnCount.ToString)
            If returnCount = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Return True
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Protected Function SendMailSMTPAUTH(strSubject As String, strSender As String, strRecipient As String, strMessage As String) As Boolean
        Try
            Dim mail As New Net.Mail.MailMessage()
            mail.IsBodyHtml = True
            mail.From = New Net.Mail.MailAddress(strSender)
            mail.To.Add(strRecipient)

            mail.Subject = strSubject
            mail.Body = strMessage

            Dim smtp As New Net.Mail.SmtpClient("smtp.gmail.com")
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New Net.NetworkCredential("mhartanah6@gmail.com", "pqlamz1234")

            smtp.Port = 587
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.EnableSsl = True
            smtp.Timeout = 2 * 60 * 1000

            smtp.Send(mail)
            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
End Class
Imports MySql.Data.MySqlClient

Public Class Registration
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        Dim div As LinkButton = Me.Master.FindControl("btnLogout")
        div.Visible = False

        Dim menuDashboard As HtmlGenericControl = Me.Master.FindControl("menuDashboard")
        menuDashboard.Visible = False
        Dim menuReferral As HtmlGenericControl = Me.Master.FindControl("menuReferral")
        menuReferral.Visible = False
        Dim menuPerformance As HtmlGenericControl = Me.Master.FindControl("menuPerformance")
        menuPerformance.Visible = False
        Dim menuAccount As HtmlGenericControl = Me.Master.FindControl("menuAccount")
        menuAccount.Visible = False
    End Sub

    Private Sub cbRegAgreement_CheckedChanged(sender As Object, e As EventArgs) Handles cbRegAgreement.CheckedChanged

        btnSubmitReg.Enabled = cbRegAgreement.Checked
        If cbRegAgreement.Checked Then
            btnSubmitReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#97cc02")
        Else
            btnSubmitReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#808080")
        End If

    End Sub

    Private Sub btnSubmitReg_Click(sender As Object, e As EventArgs) Handles btnSubmitReg.Click

        Dim emailCheck As Boolean = CheckEmailExists(txtRegEmail.Text.Trim)
        Dim result As Boolean = False


        If Not emailCheck Then
            'Dim mhaID As String = String.Empty
            'Dim mhaIDNumber As Integer = 0
            'Dim str As String() = GenerateID().Split(",")
            'mhaID = str(0)
            'mhaIDNumber = str(1)
            'MsgBox(mhaID)
            'Exit Sub
            Dim password As String = txtRegPassword.Text.Trim
            Dim sec As New clsEncryption
            password = sec.EncryptData(password)
            Dim _temp As Boolean = RegisterUser(txtRegEmail.Text.Trim, txtRegFirstName.Text.Trim, txtRegLastName.Text.Trim, password)
            result = _temp
        End If

        If result = True Then
            lblRegModalTitle.Text = "Registration Successful"
            lblRegModalMessage.Text = "Please check your email for verification."
            Session("RS") = 0
        Else
            lblRegModalTitle.Text = "Registration Error"
            lblRegModalMessage.Text = "Email already registered."
            Session("RS") = 1
        End If


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)

    End Sub

    Private Sub btnRegModalOk_Click(sender As Object, e As EventArgs) Handles btnRegModalOk.Click

        If Session("RS") = 0 Then
            Response.Redirect("Default.aspx")
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "Pop", "$('#myModal').modal('hide');$('.modal-backdrop').remove();", True)
        End If

    End Sub

    Private Function GenerateID() As String
        Dim mhaID As String = String.Empty

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT mhaUserIDNumber FROM mhaUser ORDER BY mhaUserTimeStamp DESC LIMIT 1 "
        Dim returnNumber As Integer = 0
        Dim col As New Object

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlConn.Open()
                col = sqlCmd.ExecuteScalar()
                sqlConn.Close()
            End Using
            'MsgBox(returnCount.ToString)
            If col Is Nothing Then
                returnNumber = 1
            Else
                returnNumber = CInt(col.ToString)
                returnNumber += 1
            End If

            Dim id As String = returnNumber.ToString
            mhaID = "A" & id.PadLeft(6, "0")

            mhaID = mhaID & "," & returnNumber

            Return mhaID

        Catch ex As Exception
            MsgBox(ex.Message)
            Return mhaID
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Private Function CheckEmailExists(ByVal email As String) As Boolean

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(AFID) FROM mm_af_list WHERE Email=@email"
        Dim returnCount As Integer = 0

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@email", email)
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

    Private Function RegisterUser(ByVal mhaUserEmail As String, ByVal mhaUserFirstName As String, ByVal mhaUserLastName As String, ByVal mhaUserPassword As String) As Boolean

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "INSERT INTO mm_af_list (Email,AFLvl,Password,IsActive,FirstName,LastName,isProfileComplete,CreatedDate,UpdatedDate) VALUES (@Email,1,@Password,1,@FirstName,@LastName,0,@CreatedDate,@UpdatedDate)"
        Dim returnResult As Integer = 0

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@Email", mhaUserEmail)
                sqlCmd.Parameters.AddWithValue("@Password", mhaUserPassword)
                sqlCmd.Parameters.AddWithValue("@FirstName", mhaUserFirstName)
                sqlCmd.Parameters.AddWithValue("@LastName", mhaUserLastName)
                sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now)
                sqlCmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now)
                sqlConn.Open()
                returnResult = sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using
            'MsgBox(returnResult.ToString)
            If returnResult = 0 Then
                Return False
            Else
                SendMailSMTPAUTH("mHartanah Affiliate Registration.", "mhartanah6@gmail.com", "lulusantos@gmail.com", "Thank you for registering with us.<br/> Please proceed to <a href='https://affliate.mhartanah.com'>login page</a> to login.")
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
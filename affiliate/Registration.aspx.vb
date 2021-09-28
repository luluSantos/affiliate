Imports System.Data.SqlClient
Public Class Registration
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
            Dim mhaID As String = String.Empty
            Dim mhaIDNumber As Integer = 0
            Dim str As String() = GenerateID().Split(",")
            mhaID = str(0)
            mhaIDNumber = str(1)
            'MsgBox(mhaID)
            'Exit Sub
            Dim password As String = txtRegPassword.Text.Trim
            Dim sec As New clsEncryption
            password = sec.EncryptData(password)
            Dim _temp As Boolean = RegisterUser(mhaID, mhaIDNumber, txtRegEmail.Text.Trim, txtRegFirstName.Text.Trim, txtRegLastName.Text.Trim, password)
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

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT TOP(1) mhaUserIDNumber FROM mhaUser ORDER BY mhaUserTimeStamp DESC"
        Dim returnNumber As Integer = 0
        Dim col As New Object

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
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
            Return mhaID
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Private Function CheckEmailExists(ByVal email As String) As Boolean

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(mhaUserID) FROM mhaUser WHERE mhaUserEmail=@email"
        Dim returnCount As Integer = 0

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
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
            Return True
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Private Function RegisterUser(ByVal mhaUserID As String, ByVal mhaUserIDNumber As Integer, ByVal mhaUserEmail As String, ByVal mhaUserFirstName As String, ByVal mhaUserLastName As String, ByVal mhaUserPassword As String) As Boolean

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "INSERT INTO mhaUser (mhaUserID,mhaUserIDNumber,mhaUserEmail,mhaUserFirstName,mhaUserLastName,mhaUserPassword,mhaUserTimeStamp) VALUES (@mhaUserID,@mhaUserIDNumber,@mhaUserEmail,@mhaUserFirstName,@mhaUserLastName,@mhaUserPassword,@mhaUserTimeStamp)"
        Dim returnResult As Integer = 0

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", mhaUserID)
                sqlCmd.Parameters.AddWithValue("@mhaUserIDNumber", mhaUserIDNumber)
                sqlCmd.Parameters.AddWithValue("@mhaUserEmail", mhaUserEmail)
                sqlCmd.Parameters.AddWithValue("@mhaUserFirstName", mhaUserFirstName)
                sqlCmd.Parameters.AddWithValue("@mhaUserLastName", mhaUserLastName)
                sqlCmd.Parameters.AddWithValue("@mhaUserPassword", mhaUserPassword)
                sqlCmd.Parameters.AddWithValue("@mhaUserTimeStamp", DateTime.Now)
                sqlConn.Open()
                returnResult = sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using
            'MsgBox(returnResult.ToString)
            If returnResult = 0 Then
                Return False
            Else
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
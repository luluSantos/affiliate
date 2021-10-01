Imports MySql.Data.MySqlClient

Public Class Withdrawal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not IsPostBack Then
        '    If (Session IsNot Nothing) Then
        '        Dim item As Object = Session("AFID")
        '        If (item IsNot Nothing) Then

        '        Else
        '            Session.Abandon()
        '            Response.Redirect("Default.aspx")
        '        End If
        '    Else
        '        Session.Abandon()
        '        Response.Redirect("Default.aspx")
        '    End If
        'End If

        UpdateAFEarnings()

        gvWR.DataSource = getgvWRData()
        gvWR.DataBind()

    End Sub

    Protected Function getgvWRData() As DataTable
        Dim sqlQuery As String = "SELECT CoID, Amount, BankName, BankAcc, coStatus FROM mm_af_cashout WHERE AFID=@AFID"
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim dt As New DataTable
        Dim AFID = Session("AFID")

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", AFID)
                sqlConn.Open()
                Dim sda As New MySqlDataAdapter
                sda.SelectCommand = sqlCmd
                sda.Fill(dt)
                sqlConn.Close()
            End Using
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        Return dt
    End Function

    Protected Sub UpdateAFEarnings()
        txtName.Text = Session("FirstName") & " " & Session("LastName")
        txtAmountAv.Text = "RM 0.00"

        Dim sqlQuery As String = "SELECT SUM1, SUM2 FROM  ( select SUM(Earning) AS SUM1 FROM mm_af_ads WHERE AFID=@AFID ) A CROSS JOIN  ( select SUM(Amount) AS SUM2 FROM mm_af_cashout WHERE AFID=@AFID ) B"
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim dt As New DataTable
        Dim AFID = Session("AFID")
        Dim TotalEarnings As Double = 0
        Dim TotalCashOut As Double = 0

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", AFID)
                sqlConn.Open()
                Dim sda As New MySqlDataAdapter
                sda.SelectCommand = sqlCmd
                sda.Fill(dt)
                sqlConn.Close()
            End Using

            For Each row In dt.Rows
                If Not String.IsNullOrEmpty(row("SUM1").ToString) Then
                    TotalEarnings = CDbl(row("SUM1"))
                End If
                If Not String.IsNullOrEmpty(row("SUM2").ToString) Then
                    TotalCashOut = CDbl(row("SUM2"))
                End If
            Next

            Dim av As Double = TotalEarnings - TotalCashOut
            txtAmountAv.Text = "RM " & av

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub btnSubmitWRequest_Click(sender As Object, e As EventArgs) Handles btnSubmitWRequest.Click

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT Bank, BankAcc, BranchCode, SwiftCode FROM mm_af_infolist WHERE AFID=@AFID"

        Dim dt As New DataTable


        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                sqlConn.Open()
                Dim sda As New MySqlDataAdapter
                sda.SelectCommand = sqlCmd
                sda.Fill(dt)
                sqlConn.Close()
            End Using

            If dt.Rows.Count > 0 Then

                For Each Row In dt.Rows
                    Dim sqlQuery2 = "INSERT INTO mm_af_cashout (AFID,Amount,BankAcc,BankName,BranchCode,SwiftCode,Remark1,coStatus,CreatedDate) VALUES (@AFID,@Amount,@BankAcc,@BranchCode,@SwiftCode,@Remark1,@coStatus,@CreatedDate)"
                    Using sqlCmd As New MySqlCommand(sqlQuery2, sqlConn)
                        sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                        sqlCmd.Parameters.AddWithValue("@Amount", ddlWithdrawalReqAmount.SelectedValue)
                        sqlCmd.Parameters.AddWithValue("@BankAcc", Row("BankAcc"))
                        sqlCmd.Parameters.AddWithValue("@BankName", Row("BankName"))
                        sqlCmd.Parameters.AddWithValue("@BranchCode", Row("BranchCode"))
                        sqlCmd.Parameters.AddWithValue("@SwiftCode", Row("SwiftCode"))
                        sqlCmd.Parameters.AddWithValue("@Remark1", txtAFRemark.Text.Trim)
                        sqlCmd.Parameters.AddWithValue("@coStatus", "Pending")
                        sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now)

                        sqlConn.Open()
                        sqlCmd.ExecuteNonQuery()
                        sqlConn.Close()
                    End Using
                Next

            Else
                lblWModalTitle.Text = "Withdrawal Request Incomplete."
                lblWModalMessage.Text = "Please update your profile before trying again."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
            End If

            lblWModalTitle.Text = "Withdrawal Request."
            lblWModalMessage.Text = "Successful. Thank you."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)

        Catch ex As Exception
            MsgBox(ex.Message)
            lblWModalTitle.Text = "Withdrawal Request."
            lblWModalMessage.Text = "Request Failed. Please try again later."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Sub
End Class
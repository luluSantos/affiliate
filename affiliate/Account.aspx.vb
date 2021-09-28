Imports System.Data.SqlClient
Imports System.Globalization

Public Class Account
    Inherits System.Web.UI.Page

    Private Sub Account_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuAccount")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("mhaUserID") = "A000001"
        Session("mhaUserCompleteProfile") = True
        If Not IsPostBack Then

            If Session("mhaUserCompleteProfile") = False Then
                pnlDisplayBilling.Visible = False
                pnlEditBilling.Visible = True
                btnSubmitBilling.Visible = True
                btnEditBilling.Visible = False
            Else
                pnlDisplayBilling.Visible = True
                pnlEditBilling.Visible = False
                btnSubmitBilling.Visible = False
                btnEditBilling.Visible = True
            End If

            pnlEditPersonal.Visible = False
            pnlDisplayPersonal.Visible = True
            btnEditPersonal.Visible = True
            btnSubmitPersonal.Visible = False
            btnEditPersonalCancel.Visible = False

            txtPFirstName.Text = Session("mhaUserFirstName")
            txtPLastName.Text = Session("mhaUserLastName")
            txtPEmail.Text = Session("mhaUserEmail")
            txtPCompany.Text = Session("mhaUserCompany")
            lblPFirstName.Text = Session("mhaUserFirstName")
            lblPLastName.Text = Session("mhaUserLastName")
            lblPEmail.Text = Session("mhaUserEmail")

            If Session("mhaUserCompany") = "" Then
                lblPCompany.Text = "-"
            Else
                lblPCompany.Text = Session("mhaUserCompany")
            End If

            ddlBNationality.Items.Clear()
            ddlBNationality.DataSource = CountryList()
            ddlBNationality.DataBind()

            Dim ExistsFData As Boolean = RetrieveFinancialInfo()
            If ExistsFData Then
                pnlEditFinancial.Visible = False
                btnSubmitFinancial.Visible = False
            Else
                pnlDisplayFinancialBank.Visible = False
                pnlDisplayFinancialPaypal.Visible = False
                pnlEditFinancial.Visible = True
                btnSubmitFinancial.Visible = True
            End If

        End If

    End Sub

    Private Sub rbFBank_CheckedChanged(sender As Object, e As EventArgs) Handles rbFBank.CheckedChanged

        If rbFBank.Checked Then
            divPaypal.Visible = False
            divBank0.Visible = True
            divBank1.Visible = True
            divBank2.Visible = True
            divBank3.Visible = True
        Else
            divPaypal.Visible = True
            divBank0.Visible = False
            divBank1.Visible = False
            divBank2.Visible = False
            divBank3.Visible = False
        End If

    End Sub

    Private Sub rbFPaypal_CheckedChanged(sender As Object, e As EventArgs) Handles rbFPaypal.CheckedChanged

        If rbFPaypal.Checked Then
            divPaypal.Visible = True
            divBank0.Visible = False
            divBank1.Visible = False
            divBank2.Visible = False
            divBank3.Visible = False
        Else
            divPaypal.Visible = False
            divBank0.Visible = True
            divBank1.Visible = True
            divBank2.Visible = True
            divBank3.Visible = True
        End If

    End Sub

    Private Sub btnEditPersonal_Click(sender As Object, e As EventArgs) Handles btnEditPersonal.Click
        pnlDisplayPersonal.Visible = False
        pnlEditPersonal.Visible = True
        btnEditPersonal.Visible = False
        btnSubmitPersonal.Visible = True
        btnEditPersonalCancel.Visible = True
    End Sub

    Private Sub btnEditPersonalCancel_Click(sender As Object, e As EventArgs) Handles btnEditPersonalCancel.Click
        pnlDisplayPersonal.Visible = True
        pnlEditPersonal.Visible = False
        btnEditPersonal.Visible = True
        btnSubmitPersonal.Visible = False
        btnEditPersonalCancel.Visible = False
    End Sub

    Private Sub btnEditBilling_Click(sender As Object, e As EventArgs) Handles btnEditBilling.Click
        pnlDisplayBilling.Visible = False
        pnlEditBilling.Visible = True
        btnEditBilling.Visible = False
        btnSubmitBilling.Visible = True
        btnEditBillingCancel.Visible = True
    End Sub

    Private Sub btnEditBillingCancel_Click(sender As Object, e As EventArgs) Handles btnEditBillingCancel.Click
        pnlDisplayBilling.Visible = True
        pnlEditBilling.Visible = False
        btnEditBilling.Visible = True
        btnSubmitBilling.Visible = False
        btnEditBillingCancel.Visible = False
    End Sub

    Private Function CountryList() As List(Of String)
        Dim CultureList As List(Of String) = New List(Of String)()
        Dim getCultureInfo As CultureInfo() = CultureInfo.GetCultures(CultureTypes.SpecificCultures)

        For Each getCulture As CultureInfo In getCultureInfo
            'creating the object of RegionInfo class
            Dim getRegionInfo As New RegionInfo(getCulture.LCID)
            'adding each country Name into the Dictionary
            If Not (CultureList.Contains(getRegionInfo.EnglishName)) Then
                CultureList.Add(getRegionInfo.EnglishName)
            End If
        Next
        CultureList.Sort()
        CultureList.Remove("Malaysia")
        CultureList.Insert(0, "Malaysia")
        Return CultureList
    End Function

    Private Function RetrieveFinancialInfo() As Boolean

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT mhaBIAccType,mhaBINationality,mhaBIIDType,mhaBIIDNumber,mhaBICountry,mhaBIPhone,mhaBIGender,mhaBIBirthday,mhaBIAddress,mhaFIPayment,mhaFIPayPal,mhaFIBank,mhaFIBankAccount,mhaFIBankAccountName,mhaFIBankSwift,mhaFITimeStamp FROM mhaBillingInfo WHERE mhaUserID=@mhaUserID "
        Dim dt As New DataTable

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
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
                    Dim mhaBIAccType As String = row("mhaBIAccType").ToString

                    If Not String.IsNullOrEmpty(mhaBIAccType) Then
                        lblBAccountType.Text = mhaBIAccType
                        If mhaBIAccType = "Personal" Then
                            rbBPersonal.Checked = True
                        ElseIf mhaBIAccType = "Business" Then
                            rbBBusiness.Checked = True
                        End If
                        ddlBNationality.SelectedValue = row("mhaBINationality").ToString
                        ddlBIDType.SelectedValue = row("mhaBIIDType").ToString
                        txtBIDNumber.Text = row("mhaBIIDNumber").ToString
                        ddlBCountryCode.SelectedValue = row("mhaBICountry").ToString
                        txtBPhone.Text = row("mhaBIPhone").ToString
                        ddlBGender.SelectedValue = row("mhaBIGender").ToString
                        'txtBBirthday.Text = row("mhaBIBirthday").ToString
                        If Not String.IsNullOrEmpty(row("mhaBIBirthday").ToString) Then
                            txtBBirthday.Text = DateTime.Parse(row("mhaBIBirthday")).ToString("yyyy-MM-dd")
                        End If
                        txtBAddress.Text = row("mhaBIAddress").ToString
                        lblBNationality.Text = row("mhaBINationality").ToString
                        lblBIDtype.Text = row("mhaBIIDType").ToString
                        lblBIDNumber.Text = row("mhaBIIDNumber").ToString
                        lblBCountryCode.Text = "+" & row("mhaBICountry").ToString
                        lblBPhone.Text = row("mhaBIPhone").ToString
                        lblBGender.Text = row("mhaBIGender").ToString
                        lblBBirthday.Text = row("mhaBIBirthday").ToString.Split(" "c)(0)
                        lblBAddress.Text = row("mhaBIAddress").ToString
                    End If

                    Dim mhaFIPayment As String = row("mhaFIPayment").ToString

                    If Not String.IsNullOrEmpty(mhaFIPayment) Then
                        If mhaFIPayment = "PayPal" Then
                            rbFPaypal.Checked = True
                            rbFBank.Checked = False
                            txtFPaypal.Text = row("mhaFIPayPal").ToString
                            lblFEmail.Text = row("mhaFIPayPal").ToString
                            pnlDisplayFinancialPaypal.Visible = True
                            pnlDisplayFinancialBank.Visible = False
                        ElseIf mhaFIPayment = "Bank" Then
                            rbFPaypal.Checked = False
                            rbFBank.Checked = True
                            txtFBank.Text = row("mhaFIBank").ToString
                            txtFAccount.Text = row("mhaFIBank").ToString
                            txtFAccountNo.Text = row("mhaFIBank").ToString
                            txtFSwift.Text = row("mhaFIBank").ToString
                            lblFBName.Text = row("mhaFIBank").ToString
                            lblFBAccount.Text = row("mhaFIBankAccount").ToString
                            lblFBAccountNo.Text = row("mhaFIBankAccountName").ToString
                            lblFBSwift.Text = row("mhaFIBankSwift").ToString
                            pnlDisplayFinancialPaypal.Visible = False
                            pnlDisplayFinancialBank.Visible = True
                        End If
                        Return True
                    Else
                        Return False
                    End If

                Next

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

    Private Sub btnSubmitFinancial_Click(sender As Object, e As EventArgs) Handles btnSubmitFinancial.Click

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(mhaUserID) FROM mhaBillingInfo WHERE mhaUserID=@mhaUserID"
        Dim mhaFIPayment As String = String.Empty
        Dim returnCount As Integer = 0

        If rbFBank.Checked = True Then
            mhaFIPayment = "Bank"
        ElseIf rbFPaypal.Checked = True Then
            mhaFIPayment = "PayPal"
        End If

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                sqlConn.Open()
                returnCount = sqlCmd.ExecuteScalar()
                sqlConn.Close()
            End Using

            Dim sqlQuery2 = String.Empty

            If returnCount = 0 Then
                sqlQuery2 = "INSERT INTO mhaBillingInfo (mhaUserID,mhaFIPayment,mhaFIPayPal,mhaFIBank,mhaFIBankAccount,mhaFIBankAccountName,mhaFIBankSwift,mhaFITimeStamp) VALUES (@mhaUserID,@mhaFIPayment,@mhaFIPayPal,@mhaFIBank,@mhaFIBankAccount,@mhaFIBankAccountName,@mhaFIBankSwift,@mhaFITimeStamp)"
            Else
                sqlQuery2 = "UPDATE mhaBillingInfo SET mhaFIPayment=@mhaFIPayment, mhaFIPayPal=@mhaFIPayPal, mhaFIBank=@mhaFIBank, mhaFIBankAccount=@mhaFIBankAccount, mhaFIBankAccountName=@mhaFIBankAccountName, mhaFIBankSwift=@mhaFIBankSwift, mhaFITimeStamp=@mhaFITimeStamp WHERE mhaUserID=@mhaUserID"
            End If

            Using sqlCmd As New SqlCommand(sqlQuery2, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                sqlCmd.Parameters.AddWithValue("@mhaFIPayment", mhaFIPayment)
                sqlCmd.Parameters.AddWithValue("@mhaFITimeStamp", DateTime.Now)
                If mhaFIPayment = "PayPal" Then
                    sqlCmd.Parameters.AddWithValue("@mhaFIPayPal", txtFPaypal.Text.Trim)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBank", DBNull.Value)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBankAccount", DBNull.Value)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBankAccountName", DBNull.Value)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBankSwift", DBNull.Value)
                ElseIf mhaFIPayment = "Bank" Then
                    sqlCmd.Parameters.AddWithValue("@mhaFIPayPal", DBNull.Value)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBank", txtFBank.Text.Trim)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBankAccount", txtFAccountNo.Text.Trim)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBankAccountName", txtFAccount.Text.Trim)
                    sqlCmd.Parameters.AddWithValue("@mhaFIBankSwift", txtFSwift.Text.Trim)
                End If
                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using

            pnlEditFinancial.Visible = False
            btnSubmitFinancial.Visible = False
            RetrieveFinancialInfo()
            lblRegModalTitle.Text = "Financial Info"
            lblRegModalMessage.Text = "Update Successful."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)

        Catch ex As Exception
            'MsgBox(ex.Message)
            lblRegModalTitle.Text = "Financial Info"
            lblRegModalMessage.Text = "Update Failed. Please try again later."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Sub

    Private Sub btnSubmitBilling_Click(sender As Object, e As EventArgs) Handles btnSubmitBilling.Click

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(mhaUserID) FROM mhaBillingInfo WHERE mhaUserID=@mhaUserID"
        Dim AccountType As String = String.Empty
        Dim returnCount As Integer = 0

        If rbBPersonal.Checked = True Then
            AccountType = "Personal"
        ElseIf rbBBusiness.Checked = True Then
            AccountType = "Business"
        End If

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                sqlConn.Open()
                returnCount = sqlCmd.ExecuteScalar()
                sqlConn.Close()
            End Using

            Dim sqlQuery2 = String.Empty

            If returnCount = 0 Then
                sqlQuery2 = "INSERT INTO mhaBillingInfo (mhaUserID,mhaBIAccType,mhaBINationality,mhaBIIDType,mhaBIIDNumber,mhaBICountry,mhaBIPhone,mhaBIGender,mhaBIBirthday,mhaBIAddress,mhaBITimeStamp) VALUES (@mhaUserID,@mhaBIAccType,@mhaBINationality,@mhaBIIDType,@mhaBIIDNumber,@mhaBICountry,@mhaBIPhone,@mhaBIGender,@mhaBIBirthday,@mhaBIAddress,@mhaBITimeStamp)"
            Else
                sqlQuery2 = "UPDATE mhaBillingInfo SET mhaBIAccType=@mhaBIAccType, mhaBINationality=@mhaBINationality, mhaBIIDType=@mhaBIIDType, mhaBIIDNumber=@mhaBIIDNumber, mhaBICountry=@mhaBICountry, mhaBIPhone=@mhaBIPhone, mhaBIGender=@mhaBIGender, mhaBIBirthday=@mhaBIBirthday, mhaBIAddress=@mhaBIAddress, mhaBITimeStamp=@mhaBITimeStamp WHERE mhaUserID=@mhaUserID"
            End If

            Using sqlCmd As New SqlCommand(sqlQuery2, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                sqlCmd.Parameters.AddWithValue("@mhaBIAccType", AccountType)
                sqlCmd.Parameters.AddWithValue("@mhaBINationality", ddlBNationality.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@mhaBIIDType", ddlBIDType.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@mhaBIIDNumber", txtBIDNumber.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@mhaBICountry", ddlBCountryCode.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@mhaBIPhone", txtBPhone.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@mhaBIGender", ddlBGender.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@mhaBIBirthday", txtBBirthday.Text)
                sqlCmd.Parameters.AddWithValue("@mhaBIAddress", txtBAddress.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@mhaBITimeStamp", DateTime.Now)
                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using


            Dim sqlQuery3 = "UPDATE mhaUser Set mhaUserCompleteProfile=1 WHERE mhaUserID=@mhaUserID"
            Using sqlCmd As New SqlCommand(sqlQuery3, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using

            pnlEditBilling.Visible = False
            pnlDisplayBilling.Visible = True
            btnEditBilling.Visible = True
            btnSubmitBilling.Visible = False
            btnEditBillingCancel.Visible = False
            RetrieveFinancialInfo()
            lblRegModalTitle.Text = "Billing Info"
            lblRegModalMessage.Text = "Update Successful."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)

        Catch ex As Exception
            'MsgBox(ex.Message)
            lblRegModalTitle.Text = "Billing Info"
            lblRegModalMessage.Text = "Update Failed. Please try again later."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Sub

    Private Sub btnSubmitPersonal_Click(sender As Object, e As EventArgs) Handles btnSubmitPersonal.Click

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "Update mhaUser SET mhaUserFirstName=@mhaUserFirstName, mhaUserLastName=@mhaUserLastName, mhaUserCompany=@mhaUserCompany WHERE mhaUserID=@mhaUserID"

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                sqlCmd.Parameters.AddWithValue("@mhaUserFirstName", txtPFirstName.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@mhaUserLastName", txtPLastName.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@mhaUserCompany", txtPCompany.Text.Trim)
                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using

            pnlDisplayPersonal.Visible = True
            pnlEditPersonal.Visible = False
            btnEditPersonal.Visible = True
            btnSubmitPersonal.Visible = False
            btnEditPersonalCancel.Visible = False
            lblRegModalTitle.Text = "Personal Info"
            lblRegModalMessage.Text = "Update Successful."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)

        Catch ex As Exception
            lblRegModalTitle.Text = "Personal Info"
            lblRegModalMessage.Text = "Update Failed. Please try again later."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Sub
End Class
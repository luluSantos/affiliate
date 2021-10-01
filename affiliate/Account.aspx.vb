Imports System.Globalization
Imports MySql.Data.MySqlClient

Public Class Account
    Inherits System.Web.UI.Page

    Private Sub Account_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuAccount")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            If (Session IsNot Nothing) Then
                Dim item As Object = Session("AFID")
                If (item IsNot Nothing) Then

                Else
                    Session.Abandon()
                    Response.Redirect("Default.aspx")
                End If
            Else
                Session.Abandon()
                Response.Redirect("Default.aspx")
            End If

            'If Session("isProfileComplete") = 0 Then
            '    pnlDisplayBilling.Visible = False
            '    pnlEditBilling.Visible = True
            '    btnSubmitFinancial.Visible = True
            '    btnEditFinancial.Visible = False
            'Else
            '    pnlDisplayBilling.Visible = True
            '    pnlEditBilling.Visible = False
            '    btnSubmitFinancial.Visible = False
            '    btnEditFinancial.Visible = True
            'End If

            pnlEditPersonal.Visible = False
            pnlDisplayPersonal.Visible = True
            btnEditPersonal.Visible = True
            btnSubmitPersonal.Visible = False
            btnEditPersonalCancel.Visible = False

            txtPFirstName.Text = Session("FirstName")
            txtPLastName.Text = Session("LastName")
            txtPEmail.Text = Session("Email")
            txtPCompany.Text = Session("Company")
            lblPFirstName.Text = Session("FirstName")
            lblPLastName.Text = Session("LastName")
            lblPEmail.Text = Session("Email")

            If Session("Company") = "" Then
                lblPCompany.Text = "-"
            Else
                lblPCompany.Text = Session("Company")
            End If

            ddlBNationality.Items.Clear()
            ddlBNationality.DataSource = CountryList()
            ddlBNationality.DataBind()

            Dim ExistsFData As Boolean = RetrieveFinancialInfo()
            If ExistsFData Then
                pnlDisplayBilling.Visible = True
                pnlDisplayFinancialBank.Visible = True
                pnlEditBilling.Visible = False
                pnlEditFinancial.Visible = False
                btnEditFinancial.Visible = True
                btnCancelEditFinancial.Visible = False
                btnSubmitFinancial.Visible = False
            Else
                pnlDisplayFinancialBank.Visible = False
                pnlDisplayBilling.Visible = False
                pnlEditFinancial.Visible = True
                pnlEditBilling.Visible = True
                btnEditFinancial.Visible = False
                btnCancelEditFinancial.Visible = True
                btnSubmitFinancial.Visible = True
            End If

        End If

    End Sub

    Private Sub rbFBank_CheckedChanged(sender As Object, e As EventArgs) Handles rbFBank.CheckedChanged

        If rbFBank.Checked Then
            divBank0.Visible = True
            divBank1.Visible = True
            divBank2.Visible = True
            divBank3.Visible = True
        Else
            divBank0.Visible = False
            divBank1.Visible = False
            divBank2.Visible = False
            divBank3.Visible = False
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

    Private Sub btnEditFinancialCancel_Click(sender As Object, e As EventArgs) Handles btnCancelEditFinancial.Click
        pnlDisplayBilling.Visible = True
        pnlDisplayFinancialBank.Visible = True
        pnlEditBilling.Visible = False
        pnlEditFinancial.Visible = False
        btnEditFinancial.Visible = True
        btnSubmitFinancial.Visible = False
        btnCancelEditFinancial.Visible = False
    End Sub

    Private Sub btnEditFinancial_Click(sender As Object, e As EventArgs) Handles btnEditFinancial.Click
        pnlDisplayBilling.Visible = False
        pnlDisplayFinancialBank.Visible = False
        pnlEditBilling.Visible = True
        pnlEditFinancial.Visible = True
        btnEditFinancial.Visible = False
        btnSubmitFinancial.Visible = True
        btnCancelEditFinancial.Visible = True
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

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT AccType,Nationality,IDType,IDNumber,CountryCode,Phone,Gender,Birthday,Address,PaymentType,Bank,BankAcc,BranchCode,SwiftCode FROM mm_af_infolist WHERE AFID=@AFID "
        Dim dt As New DataTable

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
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
                    Dim mhaBIAccType As String = row("AccType").ToString

                    If Not String.IsNullOrEmpty(mhaBIAccType) Then
                        lblBAccountType.Text = mhaBIAccType
                        If mhaBIAccType = "Personal" Then
                            rbBPersonal.Checked = True
                        ElseIf mhaBIAccType = "Business" Then
                            rbBBusiness.Checked = True
                        End If
                        ddlBNationality.SelectedValue = row("Nationality").ToString
                        ddlBIDType.SelectedValue = row("IDType").ToString
                        txtBIDNumber.Text = row("IDNumber").ToString
                        ddlBCountryCode.SelectedValue = row("CountryCode").ToString
                        txtBPhone.Text = row("Phone").ToString
                        ddlBGender.SelectedValue = row("Gender").ToString
                        'txtBBirthday.Text = row("mhaBIBirthday").ToString
                        If Not String.IsNullOrEmpty(row("Birthday").ToString) Then
                            txtBBirthday.Text = DateTime.Parse(row("Birthday")).ToString("yyyy-MM-dd")
                        End If
                        txtBAddress.Text = row("Address").ToString
                        lblBNationality.Text = row("Nationality").ToString
                        lblBIDtype.Text = row("IDType").ToString
                        lblBIDNumber.Text = row("IDNumber").ToString
                        lblBCountryCode.Text = "+" & row("CountryCode").ToString
                        lblBPhone.Text = row("Phone").ToString
                        lblBGender.Text = row("Gender").ToString
                        lblBBirthday.Text = row("Birthday").ToString.Split(" "c)(0)
                        lblBAddress.Text = row("Address").ToString
                    End If

                    Dim mhaFIPayment As String = row("PaymentType").ToString

                    If Not String.IsNullOrEmpty(mhaFIPayment) Then
                        If mhaFIPayment = "Bank" Then
                            rbFBank.Checked = True
                            txtFBank.Text = row("Bank").ToString
                            txtFBranch.Text = row("BranchCode").ToString
                            txtFAccountNo.Text = row("BankAcc").ToString
                            txtFSwift.Text = row("SwiftCode").ToString
                            lblFBName.Text = row("Bank").ToString
                            lblFBBranch.Text = row("BranchCode").ToString
                            lblFBAccountNo.Text = row("BankAcc").ToString
                            lblFBSwift.Text = row("SwiftCode").ToString
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

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(AFID) FROM mm_af_infolist WHERE AFID=@AFID"
        Dim mhaFIPayment As String = "Bank"
        Dim AccountType As String = String.Empty
        Dim returnCount As Integer = 0

        If rbBPersonal.Checked = True Then
            AccountType = "Personal"
        ElseIf rbBBusiness.Checked = True Then
            AccountType = "Business"
        End If

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                sqlConn.Open()
                returnCount = sqlCmd.ExecuteScalar()
                sqlConn.Close()
            End Using

            Dim sqlQuery2 = String.Empty

            If returnCount = 0 Then
                sqlQuery2 = "INSERT INTO mm_af_infolist (AFID,PaymentType,Bank,BankAcc,BranchCode,SwiftCode,AccType,Nationality,IDType,IDNumber,CountryCode,Phone,Gender,Birthday,Address,CreatedDate) VALUES (@AFID,@PaymentType,@Bank,@BankAcc,@BranchCode,@SwiftCode,@AccType,@Nationality,@IDType,@IDNumber,@CountryCode,@Phone,@Gender,@Birthday,@Address,@CreatedDate)"
            Else
                sqlQuery2 = "UPDATE mm_af_infolist SET AFID=@AFID, PaymentType=@PaymentType, Bank=@Bank, BankAcc=@BankAcc, BranchCode=@BranchCode, SwiftCode=@SwiftCode, AccType=@AccType, Nationality=@Nationality, IDType=@IDType, IDNumber=@IDNumber, CountryCode=@CountryCode, Phone=@Phone, Gender=@Gender, Birthday=@Birthday, Address=@Address, UpdatedDate=@UpdatedDate WHERE AFID=@AFID"
            End If

            Using sqlCmd As New MySqlCommand(sqlQuery2, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                sqlCmd.Parameters.AddWithValue("@PaymentType", mhaFIPayment)
                sqlCmd.Parameters.AddWithValue("@mhaFITimeStamp", DateTime.Now)
                sqlCmd.Parameters.AddWithValue("@Bank", txtFBank.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@BankAcc", txtFAccountNo.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@BranchCode", txtFBranch.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@SwiftCode", txtFSwift.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@AccType", AccountType)
                sqlCmd.Parameters.AddWithValue("@Nationality", ddlBNationality.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@IDType", ddlBIDType.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@IDNumber", txtBIDNumber.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@CountryCode", ddlBCountryCode.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@Phone", txtBPhone.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@Gender", ddlBGender.SelectedValue)
                sqlCmd.Parameters.AddWithValue("@Birthday", txtBBirthday.Text)
                sqlCmd.Parameters.AddWithValue("@Address", txtBAddress.Text.Trim)
                If returnCount = 0 Then
                    sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now)
                Else
                    sqlCmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now)
                End If

                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using

            Dim sqlQuery3 = "UPDATE mm_af_list Set isProfileComplete=1 WHERE AFID=@AFID"
            Using sqlCmd As New MySqlCommand(sqlQuery3, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using

            pnlEditFinancial.Visible = False
            pnlEditBilling.Visible = False
            pnlDisplayFinancialBank.Visible = True
            pnlDisplayBilling.Visible = True
            btnSubmitFinancial.Visible = False
            btnEditBilling.Visible = True
            btnSubmitBilling.Visible = False
            btnEditBillingCancel.Visible = False
            RetrieveFinancialInfo()
            lblRegModalTitle.Text = "Financial/Billing Info"
            lblRegModalMessage.Text = "Update Successful."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)

        Catch ex As Exception
            MsgBox(ex.Message)
            lblRegModalTitle.Text = "Financial/Billing Info"
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

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "SELECT COUNT(mhaUserID) FROM mhaBillingInfo WHERE mhaUserID=@mhaUserID"
        Dim AccountType As String = String.Empty
        Dim returnCount As Integer = 0

        If rbBPersonal.Checked = True Then
            AccountType = "Personal"
        ElseIf rbBBusiness.Checked = True Then
            AccountType = "Business"
        End If

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
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

            Using sqlCmd As New MySqlCommand(sqlQuery2, sqlConn)
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
            Using sqlCmd As New MySqlCommand(sqlQuery3, sqlConn)
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
            MsgBox(ex.Message)
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

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "Update mm_af_list SET FirstName=@FirstName, LastName=@LastName, Company=@Company WHERE AFID=@AFID"

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                sqlCmd.Parameters.AddWithValue("@FirstName", txtPFirstName.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@LastName", txtPLastName.Text.Trim)
                sqlCmd.Parameters.AddWithValue("@Company", txtPCompany.Text.Trim)
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

    Private Sub btnSubmitNewPassword_Click(sender As Object, e As EventArgs) Handles btnSubmitNewPassword.Click
        Dim sec As New clsEncryption
        Dim oldpassword = sec.EncryptData(txtOldPassword.Text.Trim)
        Dim newpassword = sec.EncryptData(txtNewPassword.Text.Trim)
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "Select count(mhaUserID) FROM mm_af_list WHERE Password=@Password AND AFID=@AFID"
        Dim returnCount As Integer = 0

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@Password", oldpassword)
                sqlCmd.Parameters.AddWithValue("@AFID", Session("AFID"))
                sqlConn.Open()
                returnCount = sqlCmd.ExecuteScalar()
                sqlConn.Close()
            End Using

            If Not returnCount = 0 Then
                Dim sqlQuery2 As String = "Update mm_af_list SET Password=@Password WHERE AFID=@AFID"
                Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                    sqlCmd.Parameters.AddWithValue("@mhaUserID", Session("mhaUserID"))
                    sqlCmd.Parameters.AddWithValue("@Password", newpassword)
                    sqlConn.Open()
                    sqlCmd.ExecuteNonQuery()
                    sqlConn.Close()

                    lblRegModalTitle.Text = "Password"
                    lblRegModalMessage.Text = "Update Successful."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "showModal();", True)
                End Using
            End If


        Catch ex As Exception
            lblRegModalTitle.Text = "Password"
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
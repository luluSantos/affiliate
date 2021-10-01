Imports MySql.Data.MySqlClient

Public Class Dashboard
    Inherits System.Web.UI.Page

    Private Sub Dashboard_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuDashboard")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AFID") = 1
        If (Session IsNot Nothing) Then
            Dim item As Object = Session("AFID")
            If (item IsNot Nothing) Then
                lblDashboardWelcome.Text = "Welcome back, " & Session("FirstName") & " " & Session("LastName")
                lblDashboardWelcome2.Text = " [ Your Last Login : " & Session("LastLogin") & " ]"
            Else
                Session.Abandon()
                Response.Redirect("Default.aspx")
            End If
        Else
            Session.Abandon()
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            UpdateDashboardData()
        End If
    End Sub

    Protected Sub UpdateDashboardData()
        Dim sqlQuery As String = "SELECT (SELECT count(adsrID) FROM mm_af_adsrecords WHERE AFID=@AFID And CreatedDate >= DATE(NOW()) - INTERVAL 7 DAY) AS TOTAL7CLKS,
                                    (SELECT count(adsrID) FROM mm_af_adsrecords WHERE AFID=@AFID And CreatedDate >= Date(Now()) - INTERVAL 30 DAY) As TOTAL30CLKS,
                                    (SELECT count(adsrID) FROM mm_af_adsrecords WHERE AFID=@AFID And CreatedDate >= DATE(NOW()) - INTERVAL 90 DAY) AS TOTAL90CLKS,
                                    (SELECT count(AdsID) FROM mm_af_ads WHERE AFID=@AFID And CreatedDate >= DATE(NOW()) - INTERVAL 7 DAY) AS TOTAL7ADS, 
                                    (SELECT count(AdsID) FROM mm_af_ads WHERE AFID=@AFID And CreatedDate >= DATE(NOW()) - INTERVAL 30 DAY) AS TOTAL30ADS,
                                    (SELECT count(AdsID) FROM mm_af_ads WHERE AFID=@AFID And CreatedDate >= DATE(NOW()) - INTERVAL 90 DAY) AS TOTAL90ADS "
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
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

            lbl7clks.Attributes.Add("data-to", CInt(dt.Rows(0).Item("TOTAL7CLKS").ToString))
            lbl30clks.Attributes.Add("data-to", CInt(dt.Rows(0).Item("TOTAL30CLKS").ToString))
            lbl90clks.Attributes.Add("data-to", CInt(dt.Rows(0).Item("TOTAL90CLKS").ToString))
            lbl7ads.Attributes.Add("data-to", CInt(dt.Rows(0).Item("TOTAL7ADS").ToString))
            lbl30ads.Attributes.Add("data-to", CInt(dt.Rows(0).Item("TOTAL30ADS").ToString))
            lbl90ads.Attributes.Add("data-to", CInt(dt.Rows(0).Item("TOTAL90ADS").ToString))


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try
    End Sub

    Protected Sub UpdateEarningsAndAffliateLevel()
        Dim sqlQuery As String = "Select AFLvl, (Select count(adsrID) FROM mm_af_adsrecords WHERE AFID= @AFID) As TotalCLicks,  
                                    (SELECT sum(Earning) FROM mm_af_ads WHERE AFID=1) AS TOTALEARNINGS, (SELECT sum(Amount) FROM mm_af_cashout WHERE AFID=1@AFID) AS TotalCashO,
                                    (SELECT Amount FROM mm_af_cashout WHERE AFID=1 ORDER BY UpdatedDate DESC LIMIT 1) AS LastCashOut 
                                    From mm_af_list Where AFID=@AFID"
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
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

            lblaqclks.Text = dt.Rows(0).Item("TotalCLicks").ToString
            lblcl.Text = dt.Rows(0).Item("TotalCLicks").ToString
            lblcb.Text = CInt(dt.Rows(0).Item("TOTALEARNINGS").ToString) - CInt(dt.Rows(0).Item("TotalCashO").ToString)
            lblRecentpay.Text = "RM " & dt.Rows(0).Item("LastCashOut").dt.Rows(0).Item("TOTALEARNINGS").ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try
    End Sub
End Class
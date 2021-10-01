Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class Referral
    Inherits System.Web.UI.Page

    Private Sub Referral_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuReferral")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("mhaUserID") = "A000001"

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

        If Not IsPostBack Then
            ddlTargetPage_Load()
            ddlTargetPage2_Load()
            hypTargetPage.NavigateUrl = "https://mhartanah.com/property/" & ddlTargetPage.SelectedValue
            hypdyn1.NavigateUrl = "https://mhartanah.com/property/" & ddldyntarget1.SelectedValue.Split(",")(1)
            hypdyn2.NavigateUrl = "https://mhartanah.com/property/" & ddldyntarget2.SelectedValue.Split(",")(1)
            hypdyn3.NavigateUrl = "https://mhartanah.com/property/" & ddldyntarget3.SelectedValue.Split(",")(1)
        End If


        'lblTextLink.Text = "https://localhost:44354/Redirect?x=" & hexID
        'lblBannerCode.Text = "&lt;script src='mhAffliate.js' x='" & hexID & "'></script&gt;"
    End Sub

    Public Shared Function ConvertStringToHex(ByVal input As String) As String
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Unicode
        Dim stringBytes As Byte() = encoding.GetBytes(input)
        Dim sbBytes As StringBuilder = New StringBuilder(stringBytes.Length * 2)

        For Each b As Byte In stringBytes
            sbBytes.AppendFormat("{0:X2}", b)
        Next

        Return sbBytes.ToString()
    End Function

    Private Sub btnCopyTextLink_Click(sender As Object, e As EventArgs) Handles btnCopyTextLink.Click
        Dim thread As New Thread(Sub() Windows.Clipboard.SetText(lblTextLink.Text))
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
        thread.Join()
    End Sub

    'Private Sub btnCopyBannerLink_Click(sender As Object, e As EventArgs) Handles btnCopyBannerLink.Click
    '    Dim str As String = lblBannerCode.Text.Replace("&lt;", "<").Replace("&gt;", ">")
    '    Dim thread As New Thread(Sub() Windows.Clipboard.SetText(str))
    '    thread.SetApartmentState(ApartmentState.STA)
    '    thread.Start()
    '    thread.Join()
    'End Sub

    Private Sub ddlTargetPage_Load()

        Dim sqlQuery As String = "SELECT post_name, post_title FROM wpy2_posts where post_type='property' and post_status='publish' order by post_date desc"
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim ds As New DataSet

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlConn.Open()
                Dim sda As New MySqlDataAdapter
                sda.SelectCommand = sqlCmd
                sda.Fill(ds)
                sqlConn.Close()
            End Using

            ddlTargetPage.DataTextField = ds.Tables(0).Columns("post_title").ToString()
            ddlTargetPage.DataValueField = ds.Tables(0).Columns("post_name").ToString()
            ddlTargetPage.DataSource = ds.Tables(0)
            ddlTargetPage.DataBind()

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ddlTargetPage2_Load()

        Dim sqlQuery As String = "SELECT post_title, CONCAT(id, ',', post_name) AS concatvalue FROM wpy2_posts where post_type='property' and post_status='publish' order by post_date desc"
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim ds As New DataSet

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlConn.Open()
                Dim sda As New MySqlDataAdapter
                sda.SelectCommand = sqlCmd
                sda.Fill(ds)
                sqlConn.Close()
            End Using

            ddldyntarget1.DataTextField = ds.Tables(0).Columns("post_title").ToString()
            ddldyntarget1.DataValueField = ds.Tables(0).Columns("concatvalue").ToString()
            ddldyntarget1.DataSource = ds.Tables(0)
            ddldyntarget1.DataBind()

            ddldyntarget2.DataTextField = ds.Tables(0).Columns("post_title").ToString()
            ddldyntarget2.DataValueField = ds.Tables(0).Columns("concatvalue").ToString()
            ddldyntarget2.DataSource = ds.Tables(0)
            ddldyntarget2.DataBind()

            ddldyntarget3.DataTextField = ds.Tables(0).Columns("post_title").ToString()
            ddldyntarget3.DataValueField = ds.Tables(0).Columns("concatvalue").ToString()
            ddldyntarget3.DataSource = ds.Tables(0)
            ddldyntarget3.DataBind()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ddlTargetPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTargetPage.SelectedIndexChanged
        hypTargetPage.NavigateUrl = "https://mhartanah.com/property/" & ddlTargetPage.SelectedValue
        lblTextLink.Text = String.Empty
    End Sub

    Private Sub ddldyntarget1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldyntarget1.SelectedIndexChanged
        hypdyn1.NavigateUrl = "https://mhartanah.com/property/" & ddldyntarget1.SelectedValue.Split(",")(1)
        lblDynLink.Text = String.Empty
        pnldisplaydemo.Visible = False
    End Sub

    Private Sub ddldyntarget2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldyntarget2.SelectedIndexChanged
        hypdyn2.NavigateUrl = "https://mhartanah.com/property/" & ddldyntarget2.SelectedValue.Split(",")(1)
        lblDynLink.Text = String.Empty
        pnldisplaydemo.Visible = False
    End Sub

    Private Sub ddldyntarget3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldyntarget3.SelectedIndexChanged
        hypdyn3.NavigateUrl = "https://mhartanah.com/property/" & ddldyntarget3.SelectedValue.Split(",")(1)
        lblDynLink.Text = String.Empty
        pnldisplaydemo.Visible = False
    End Sub

    Private Sub lnkbGenerateTextLink_Click(sender As Object, e As EventArgs) Handles lnkbGenerateTextLink.Click

        Dim hexID As String = ConvertStringToHex(Session("AFID"))
        Dim targetPage As String = HttpUtility.UrlEncode(hypTargetPage.NavigateUrl)
        Dim type As String = "t"
        Dim AdsID As Integer = InsertAds(txtTextAdsName.Text.Trim, "text")

        If AdsID = 0 Then
            Exit Sub
        End If
        Thread.Sleep(1000)
        Dim console As String = "?afid=" & hexID & "&aid=" & AdsID & "&typ=" & type & "&target=" & targetPage
        UpdateAds(AdsID, console)

        lblTextLink.Text = "https://localhost:44354/Redirect" & console
    End Sub

    Private Sub lnkbGenerateDynLink_Click(sender As Object, e As EventArgs) Handles lnkbGenerateDynLink.Click

        Dim hexID As String = ConvertStringToHex(Session("AFID"))
        Dim targetPage1 As String = HttpUtility.UrlEncode(hypdyn1.NavigateUrl)
        Dim targetPage2 As String = HttpUtility.UrlEncode(hypdyn2.NavigateUrl)
        Dim targetpage3 As String = HttpUtility.UrlEncode(hypdyn3.NavigateUrl)
        Dim type As String = "d"
        Dim AdsID As Integer = InsertAds(txtDynAdsName.Text.Trim, "dynamic")

        If AdsID = 0 Then
            Exit Sub
        End If
        Thread.Sleep(1000)

        Dim props As String = String.Concat(ddldyntarget1.SelectedValue.Split(",")(0), ",", ddldyntarget2.SelectedValue.Split(",")(0), ",", ddldyntarget3.SelectedValue.Split(",")(0))
        Dim console1 As String = "?afid=" & hexID & "&aid=" & AdsID & "&typ=" & type & "&target=" & targetPage1
        Dim console2 As String = "?afid=" & hexID & "&aid=" & AdsID & "&typ=" & type & "&target=" & targetPage2
        Dim console3 As String = "?afid=" & hexID & "&aid=" & AdsID & "&typ=" & type & "&target=" & targetpage3
        Dim console As String = console1 & "," & console2 & "," & console3
        UpdateAds(AdsID, Console)

        lblDynLink.Text = "&lt;script src='mhAffliate.js' w='" & props & "' x='" & HttpUtility.UrlEncode(console1) & "' y='" & HttpUtility.UrlEncode(console2) & "' z='" & HttpUtility.UrlEncode(console3) & "'>&lt;/script&gt;"
        'lbldynDemo.Text = "<script src='mhAffliate.js' w='" & props & "' x='" & console2 & "' y='" & console2 & "' z='" & console3 & "' >"
        ltliFrame.Text = "<iframe src='https://localhost:44354/DynamicWidget.aspx?w=" & props & "&x=" & HttpUtility.UrlEncode(console1) & "&y=" & HttpUtility.UrlEncode(console2) & "&z=" & HttpUtility.UrlEncode(console3) & "' style='border-width:0' width='800' height='400' frameborder='0' scrolling='no'></iframe >"
        pnldisplaydemo.Visible = True
    End Sub

    Protected Function InsertAds(ByVal adsname As String, ByVal method As String) As Integer

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "PSP_AF_INSERTADS"
        Dim result As Integer = 0

        Try
            Using sqlCmd As New MySqlCommand
                sqlCmd.CommandText = sqlQuery
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.Connection = sqlConn
                sqlCmd.CommandTimeout = 0
                sqlCmd.Parameters.AddWithValue("IN_AFID", Session("AFID"))
                sqlCmd.Parameters.AddWithValue("IN_ADNAME", adsname)
                sqlCmd.Parameters.AddWithValue("IN_METHOD", method)
                sqlConn.Open()
                result = CInt(sqlCmd.ExecuteScalar())
            End Using

            Return result

        Catch ex As Exception
            MsgBox(ex.Message)
            Return result
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Protected Sub UpdateAds(ByVal AdsID As String, ByVal console As String)

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "UPDATE mm_af_ads SET console=@console WHERE AdsID=@AdsID;"

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AdsID", AdsID)
                sqlCmd.Parameters.AddWithValue("@console", console)
                sqlConn.Open()
                sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using

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
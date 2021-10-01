Imports MySql.Data.MySqlClient
Imports OfficeOpenXml

Public Class Performance
    Inherits System.Web.UI.Page

    Private Sub Performance_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuPerformance")
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

            UpdateDashboardData()

            GridView1.DataSource = getgvData()
            GridView1.DataBind()
        End If


    End Sub

    Protected Function GetDataTable() As DataTable

        Dim sqlQuery As String = "SELECT * FROM mhaRecords"
        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim dt As New DataTable

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                'sqlCmd.Parameters.AddWithValue("@mhaUserID", mhaUserID)
                'sqlCmd.Parameters.AddWithValue("@mhaRecIP", mhaRecIP)
                sqlCmd.Parameters.AddWithValue("@mhaRecCountry", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecCity", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecType", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecTimeStamp", DateTime.Now)
                sqlConn.Open()
                Dim sda As New MySqlDataAdapter
                sda.SelectCommand = sqlCmd
                sda.Fill(dt)
                sqlConn.Close()
            End Using

            Return dt

        Catch ex As Exception
            MsgBox(ex.Message)
            Return dt
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Function

    Protected Function getgvData() As DataTable
        Dim sqlQuery As String = "SELECT Method, AdsName, 'CPC/CPL', Earning, CreatedDate FROM mm_af_ads WHERE AFID=@AFID"
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
            MsgBox(ex.Message)
        End Try

        Return dt
    End Function

    Protected Sub ExportTableExcel(ByVal dt As DataTable)

        If dt.Rows.Count > 0 Then
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ClearContent()
            HttpContext.Current.Response.ClearHeaders()
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=mhaffliate.xlsx")
            Using pck As ExcelPackage = New ExcelPackage
                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("Records")
                ws.Cells("A1").LoadFromDataTable(dt, True)
                ws.Column(7).Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss"
                Dim ms As New IO.MemoryStream()
                pck.SaveAs(ms)
                ms.WriteTo(HttpContext.Current.Response.OutputStream)
            End Using
        End If

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportTableExcel(GetDataTable())
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
End Class
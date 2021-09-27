Imports System.Data.SqlClient
Imports OfficeOpenXml

Public Class Performance
    Inherits System.Web.UI.Page

    Private Sub Performance_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim li As HtmlGenericControl = Page.Master.FindControl("menuPerformance")
        li.Attributes.Add("class", "current-menu-item")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GridView1.DataSource = New List(Of String)
        GridView1.DataBind()
    End Sub

    Protected Function GetDataTable() As DataTable

        Dim sqlQuery As String = "SELECT * FROM mhaRecords"
        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim dt As New DataTable

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                'sqlCmd.Parameters.AddWithValue("@mhaUserID", mhaUserID)
                'sqlCmd.Parameters.AddWithValue("@mhaRecIP", mhaRecIP)
                sqlCmd.Parameters.AddWithValue("@mhaRecCountry", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecCity", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecType", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecTimeStamp", DateTime.Now)
                sqlConn.Open()
                Dim sda As New SqlDataAdapter
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
End Class
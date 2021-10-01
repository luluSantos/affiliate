Imports MySql.Data.MySqlClient

Public Class DynamicWidget
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim console1 As String = HttpUtility.UrlDecode(Request.QueryString("x"))
        Dim console2 As String = HttpUtility.UrlDecode(Request.QueryString("y"))
        Dim console3 As String = HttpUtility.UrlDecode(Request.QueryString("z"))
        Dim console As String = String.Empty
        Dim list As String() = Request.QueryString("w").Split(",")
        For i = 0 To list.Length - 1

            Dim sqlQuery As String = "select post_title,(select meta_value from wpy2_postmeta where post_id = @postid And meta_key = 'real_estate_property_address') as post_address,
                                    CONCAT('https://mhartanah.com/property/',post_name,'/') as post_url,
                                    (select meta_value from wpy2_postmeta where post_id = @postid And meta_key = 'real_estate_property_price') as post_price,
                                    CONCAT('https://mhartanah.com/wp-content/uploads/',(select meta_value from wpy2_postmeta where post_id =  (select meta_value as pic_id from wpy2_postmeta where post_id = @postid and meta_key = '_thumbnail_id') and meta_key = '_wp_attached_file')) as post_pic 
                                    From wpy2_posts
                                    Where ID = @postid"
            Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
            Dim dt As New DataTable

            Try
                Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                    sqlCmd.Parameters.AddWithValue("@postid", list(i))
                    sqlConn.Open()
                    Dim sda As New MySqlDataAdapter
                    sda.SelectCommand = sqlCmd
                    sda.Fill(dt)
                    sqlConn.Close()
                End Using

                If i = 0 Then
                    console = console1
                ElseIf i = 1 Then
                    console = console2
                Else
                    console = console3
                End If

                Dim img As Image = Me.FindControl("img" & i)
                img.ImageUrl = dt.Rows(0)("post_pic").ToString
                Dim hyp As HyperLink = Me.FindControl("hyp" & i)
                hyp.NavigateUrl = "https://localhost:44354/Redirect" & console 'dt.Rows(0)("post_url").ToString
                Dim title As Label = Me.FindControl("title" & i)
                title.Text = dt.Rows(0)("post_title")
                Dim body As Label = Me.FindControl("text" & i)
                body.Text = Truncate(dt.Rows(0)("post_address").ToString, 40)


            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If sqlConn.State = ConnectionState.Open Then
                    sqlConn.Close()
                End If
                sqlConn.Dispose()
            End Try

        Next
    End Sub

    Public Function Truncate(value As String, length As Integer) As String
        ' If argument is too big, return the original string.
        ' ... Otherwise take a substring from the string's start index.
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length) & "..."
        End If
    End Function

End Class
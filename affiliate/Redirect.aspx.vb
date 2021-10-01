Imports MySql.Data.MySqlClient

Public Class Redirect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim afid As String = Request.QueryString("afid")
        Dim adsid As String = Request.QueryString("aid")
        Dim type As String = Request.QueryString("typ")
        Dim target As String = HttpUtility.UrlDecode(Request.QueryString("target"))
        If Not String.IsNullOrEmpty(afid) Then
            afid = ConvertHexToString(afid)
        End If

        InsertRecord(afid, adsid, type, target)
        Response.Redirect(target)
    End Sub

    Protected Sub InsertRecord(ByVal afid As String, ByVal AdsID As String, ByVal type As String, ByVal target As String)

        Dim sqlConn As New MySqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "INSERT INTO mm_af_adsrecords (AFID,AdsID,Method,adsrIP,CreatedDate) VALUES (@AFID,@AdsID,@Method,@adsrIP,@CreatedDate)"
        Dim returnResult As Integer = 0

        Dim mhaRecIP = GetIPAddress()

        Try
            Using sqlCmd As New MySqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@AFID", afid)
                sqlCmd.Parameters.AddWithValue("@AdsID", AdsID)
                sqlCmd.Parameters.AddWithValue("@Method", type)
                sqlCmd.Parameters.AddWithValue("@adsrIP", mhaRecIP)
                sqlCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now)
                sqlConn.Open()
                returnResult = sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using
            'MsgBox(returnResult.ToString)
            If returnResult = 0 Then

            Else

            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            If sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
            End If
            sqlConn.Dispose()
        End Try

    End Sub

    Private Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If
    End Function

    Public Shared Function ConvertStringToHex(ByVal input As String) As String
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Unicode
        Dim stringBytes As Byte() = encoding.GetBytes(input)
        Dim sbBytes As StringBuilder = New StringBuilder(stringBytes.Length * 2)

        For Each b As Byte In stringBytes
            sbBytes.AppendFormat("{0:X2}", b)
        Next

        Return sbBytes.ToString()
    End Function

    Public Shared Function ConvertHexToString(ByVal hexInput As String) As String
        Dim encoding As System.Text.Encoding = System.Text.Encoding.Unicode
        Dim numberChars As Integer = hexInput.Length
        Dim bytes As Byte() = New Byte(numberChars / 2 - 1) {}

        For i As Integer = 0 To numberChars - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hexInput.Substring(i, 2), 16)
        Next

        Return encoding.GetString(bytes)
    End Function

End Class
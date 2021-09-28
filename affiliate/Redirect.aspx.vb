Imports System.Data.SqlClient

Public Class Redirect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mhaUserID As String = Request.QueryString("x")
        If Not String.IsNullOrEmpty(mhaUserID) Then
            mhaUserID = ConvertHexToString(mhaUserID)
        Else
            Exit Sub
        End If
        InsertRecord(mhaUserID)
        'Response.Redirect("UserDetail.aspx")
    End Sub

    Protected Sub InsertRecord(ByVal mhaUserID As String)

        Dim sqlConn As New SqlConnection(ConfigurationManager.ConnectionStrings("mhaDB").ConnectionString)
        Dim sqlQuery As String = "INSERT INTO mhaRecords (mhaUserID,mhaRecIP,mhaRecCountry,mhaRecCity,mhaRecType,mhaRecTimeStamp) VALUES (@mhaUserID,@mhaRecIP,@mhaRecCountry,@mhaRecCity,@mhaRecType,@mhaRecTimeStamp)"
        Dim returnResult As Integer = 0

        Dim mhaRecIP = GetIPAddress()

        Try
            Using sqlCmd As New SqlCommand(sqlQuery, sqlConn)
                sqlCmd.Parameters.AddWithValue("@mhaUserID", mhaUserID)
                sqlCmd.Parameters.AddWithValue("@mhaRecIP", mhaRecIP)
                sqlCmd.Parameters.AddWithValue("@mhaRecCountry", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecCity", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecType", DBNull.Value)
                sqlCmd.Parameters.AddWithValue("@mhaRecTimeStamp", DateTime.Now)
                sqlConn.Open()
                returnResult = sqlCmd.ExecuteNonQuery()
                sqlConn.Close()
            End Using
            'MsgBox(returnResult.ToString)
            If returnResult = 0 Then

            Else

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
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
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Public Class DataControl

    Private p_conexion As String = ""
    Private parms As String = String.Empty
    Private parmsC As String = String.Empty
    Private parametros As String = String.Empty

    Sub New(Optional ByVal conexion As Integer = 0)
        If conexion = 0 Then
            p_conexion = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Else
            p_conexion = HttpContext.Current.Session("conexion").ToString
        End If
    End Sub

    Public Sub Catalogo(ByVal combo As Web.UI.WebControls.DropDownList, ByVal sql As String, ByVal sel As Integer, Optional ByVal todo As Boolean = False)
        '
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlDataAdapter(sql, conn)
        Dim ds As DataSet = New DataSet
        cmd.Fill(ds)
        combo.DataSource = ds
        combo.DataValueField = ds.Tables(0).Columns(0).ColumnName
        combo.DataTextField = ds.Tables(0).Columns(1).ColumnName
        combo.DataBind()
        '
        If todo Then
            combo.Items.Insert(0, New ListItem("--Todos--", "0"))
        Else
            combo.Items.Insert(0, New ListItem("--Seleccione--", "0"))
        End If
        If sel > 0 Then
            combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByValue(sel))
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
    End Sub

    Public Sub Catalogo(ByVal combo As DropDownList, ByVal dt As DataTable, ByVal sel As Integer, ByVal pValueMember As String, ByVal pDisplayMember As String, Optional ByVal todo As Boolean = False)
        combo.DataSource = dt
        combo.DataValueField = pValueMember
        combo.DataTextField = pDisplayMember
        combo.DataBind()

        If todo Then
            combo.Items.Insert(0, New ListItem("--Todos--", "0"))
        Else
            combo.Items.Insert(0, New ListItem("--Seleccione--", "0"))
        End If

        If sel > 0 Then
            combo.SelectedValue = sel
        End If

    End Sub

    Public Sub CatalogoString(ByVal combo As DropDownList, ByVal dt As DataTable, ByVal sel As String, ByVal pValueMember As String, ByVal pDisplayMember As String, Optional ByVal todo As Boolean = False)
        combo.DataSource = dt
        combo.DataValueField = pValueMember
        combo.DataTextField = pDisplayMember
        combo.DataBind()

        If todo Then
            combo.Items.Insert(0, New ListItem("--Todos--", "0"))
        Else
            combo.Items.Insert(0, New ListItem("--Seleccione--", "0"))
        End If

        If sel.Length > 0 Then
            combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByValue(sel))
        End If

    End Sub

    Public Sub CatalogoString(ByVal combo As Web.UI.WebControls.DropDownList, ByVal sql As String, ByVal sel As String, Optional ByVal todo As Boolean = False)
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlDataAdapter(sql, conn)
        Dim ds As DataSet = New DataSet
        cmd.Fill(ds)
        combo.DataSource = ds
        combo.DataValueField = ds.Tables(0).Columns(0).ColumnName
        combo.DataTextField = ds.Tables(0).Columns(1).ColumnName
        combo.DataBind()
        '
        If todo Then
            combo.Items.Insert(0, New ListItem("--Todos--", "0"))
        Else
            combo.Items.Insert(0, New ListItem("--Seleccione--", "0"))
        End If
        If sel.Length > 0 Then
            combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByValue(sel))
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Public Sub Catalogo(ByVal combo As Telerik.Web.UI.RadComboBox, ByVal dt As DataTable, ByVal sel As Integer, ByVal pValueMember As String, ByVal pDisplayMember As String)
        combo.DataSource = dt
        combo.DataValueField = pValueMember
        combo.DataTextField = pDisplayMember
        combo.DataBind()
        If sel > 0 Then
            combo.SelectedValue = sel
        End If
    End Sub

    Public Sub CatalogoRad(ByVal combo As Telerik.Web.UI.RadComboBox, ByVal dt As DataTable, Optional ByVal textIni As Boolean = False, Optional ByVal todo As Boolean = False)

        combo.DataSource = dt
        combo.DataValueField = dt.Columns(0).ColumnName
        combo.DataTextField = dt.Columns(1).ColumnName
        combo.DataBind()

        If textIni Then
            If todo Then
                combo.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Todos--", "0"))
            Else
                combo.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Seleccione--", "0"))
            End If
        End If

    End Sub

    Public Sub FillRadComboBox(ByVal RadComboBox As Telerik.Web.UI.RadComboBox, ByVal SQLCommand As String)
        Dim conn As New SqlConnection(p_conexion)
        Try
            Dim cmd As SqlDataAdapter = New SqlDataAdapter(SQLCommand, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim ds As New DataSet
            cmd.Fill(ds)

            RadComboBox.DataSource = ds.Tables(0)
            RadComboBox.DataTextField = ds.Tables(0).Columns(1).ColumnName.ToString()
            RadComboBox.DataValueField = ds.Tables(0).Columns(0).ColumnName.ToString()
            RadComboBox.DataBind()
        Catch ex As Exception
            Throw New Exception(" - FillRadComboBox: " + ex.Message.ToString())
        Finally
            If conn.State <> ConnectionState.Closed Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
        End Try

    End Sub

    Public Sub FillRadComboBoxMultiple(ByVal RadComboBox As RadComboBox, ByVal SQLCommand As String, ByVal sel As String, Optional ByVal todo As Boolean = False)
        Dim conn As New SqlConnection(p_conexion)
        Try
            Dim cmd As SqlDataAdapter = New SqlDataAdapter(SQLCommand, p_conexion)
            conn.Open()
            Dim ds As New DataSet
            cmd.Fill(ds)
            RadComboBox.DataSource = ds.Tables(0)
            RadComboBox.DataTextField = ds.Tables(0).Columns(1).ColumnName.ToString()
            RadComboBox.DataValueField = ds.Tables(0).Columns(0).ColumnName.ToString()
            RadComboBox.DataBind()
            If todo Then
                RadComboBox.Items.Insert(0, New RadComboBoxItem("--Todos--", 0))
            Else
            End If
            If sel.Length > 0 Then
                RadComboBox.SelectedIndex = RadComboBox.Items.IndexOf(RadComboBox.FindItemByValue(sel))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Public Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New System.Security.Cryptography.SHA1CryptoServiceProvider

        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function

    Public Function GetBytes(str As String) As Byte()
        Dim bytes As Byte() = New Byte(str.Length * 2 - 1) {}
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
        Return bytes
    End Function

    Public Function GetString(bytes As Byte()) As String
        Dim chars As Char() = New Char(bytes.Length / 2 - 1) {}
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
        Return New String(chars)
    End Function

    Public Function FillDataSet(ByVal SQL As String) As DataSet
        Dim conn As New SqlConnection(p_conexion)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            Dim cmd As New SqlDataAdapter(SQL, conn)
            cmd.SelectCommand.CommandTimeout = 180
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
            conn = Nothing
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
       
        Return ds

    End Function

    Public Function FillDataSet(ByVal spName As String, ByVal params As ArrayList) As DataSet
        Dim conn As New SqlConnection(p_conexion)
        Dim ds As New DataSet
        Try
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parametros = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                If parms = String.Empty Then
                    parms = param.ParameterName.ToString() + "=" + param.SqlValue.ToString()
                Else
                    parms &= "," & param.ParameterName.ToString() + "=" + param.SqlValue.ToString()
                End If
            Next

            parametros = parms

            Dim da As New SqlDataAdapter(command)
            da.Fill(ds)

            If ds.Tables.Count > 0 Then
                Return ds
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

    End Function

    Public Function FillDataTable(ByVal spName As String, ByVal params As ArrayList) As DataTable
        Dim conn As New SqlConnection(p_conexion)
        Try
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parametros = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                If parms = String.Empty Then
                    parms = param.ParameterName.ToString() + "=" + param.SqlValue.ToString()
                Else
                    parms &= "," & param.ParameterName.ToString() + "=" + param.SqlValue.ToString()
                End If
            Next

            parametros = parms

            Dim ds As New DataSet
            Dim da As New SqlDataAdapter(command)
            da.Fill(ds)

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Function

    Public Function FillDataTable(ByVal SQL As String) As DataTable
        Dim conn As New SqlConnection(p_conexion)
        Dim dt As DataTable = New DataTable
        Try
            conn.Open()
            Dim cmd As New SqlDataAdapter(SQL, conn)
            cmd.Fill(dt)
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Return dt

    End Function

    Public Function ExecuteNonQueryIntScalar(ByVal spName As String, ByVal params As ArrayList) As Int64
        Dim conn As New SqlConnection(p_conexion)
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            'crear comando
            Dim value As Int32
            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parametros = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                If parms = String.Empty Then
                    parms = param.ParameterName.ToString() + "=" + param.SqlValue.ToString()
                Else
                    parms &= "," & param.ParameterName.ToString() + "=" + param.SqlValue.ToString()
                End If
            Next

            parametros = parms

            value = command.ExecuteScalar()
            command.Parameters.Clear()
            command.Dispose()

            Return value
        Catch ex As Exception
            Throw ex
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Function

    Public Sub RunSQLQuery(ByVal SQL As String)
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlCommand(SQL, conn)
        cmd.CommandTimeout = 900

        cmd.ExecuteNonQuery()
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Public Function RunSQLScalarQuery(ByVal SQL As String) As Long
        Dim valor As Long = 0
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlCommand(SQL, conn)
        valor = cmd.ExecuteScalar
        conn.Close()
        conn.Dispose()
        conn = Nothing
        Return valor
    End Function

    Public Function RunSQLScalarQueryDecimal(ByVal SQL As String) As Decimal
        Dim valor As Decimal = 0
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlCommand(SQL, conn)
        valor = cmd.ExecuteScalar
        conn.Close()
        conn.Dispose()
        conn = Nothing
        Return valor
    End Function

    Public Function RunSQLScalarQueryString(ByVal SQL As String) As String
        Dim valor As String = ""
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlCommand(SQL, conn)
        valor = cmd.ExecuteScalar
        conn.Close()
        conn.Dispose()
        conn = Nothing
        Return valor
    End Function

    Public Function RunSQLScalarQueryString(ByVal _sentence_ As String, ByVal type As Integer, ByVal param As ArrayList) As String
        Dim valor As String = ""
        Dim conn As New SqlConnection(p_conexion)
        Dim command As New SqlCommand
        Try
            conn.Open()
            command.Connection = conn
            If type = 0 Then
                command.CommandType = CommandType.Text
            Else
                command.CommandType = CommandType.StoredProcedure
            End If
            command.CommandText = _sentence_
            For cont As Integer = 0 To param.Count - 1
                command.Parameters.Add(param(cont))
            Next
            valor = command.ExecuteScalar
            command.Parameters.Clear()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
        End Try
        Return valor
    End Function

    Public Sub ExecuteSP(ByVal comando As String, ByVal type As Integer, ByVal param As ArrayList)
        Dim conn As New SqlConnection(p_conexion)
        Dim command As New SqlCommand
        Try
            conn.Open()
            command.Connection = conn
            If type = 0 Then
                command.CommandType = CommandType.Text
            Else
                command.CommandType = CommandType.StoredProcedure
            End If
            command.CommandText = comando
            For cont As Integer = 0 To param.Count - 1
                command.Parameters.Add(param(cont))
            Next
            command.ExecuteNonQuery()
            command.Parameters.Clear()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Public Function SentenceScalarLong(ByVal _sentence_ As String, ByVal type As Integer, ByVal param As ArrayList) As Long
        Dim valor As Long = 0
        Dim conn As New SqlConnection(p_conexion)
        Dim command As New SqlCommand
        Try
            conn.Open()
            command.Connection = conn
            If type = 0 Then
                command.CommandType = CommandType.Text
            Else
                command.CommandType = CommandType.StoredProcedure
            End If
            command.CommandText = _sentence_
            For cont As Integer = 0 To param.Count - 1
                command.Parameters.Add(param(cont))
            Next
            valor = command.ExecuteScalar
            command.Parameters.Clear()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
        End Try
        Return valor
    End Function

    Public Function SentenceScalarLong(ByVal _sentence_ As String, ByVal type As Integer, ByVal param As ArrayList, ByVal con As String) As Long
        Dim valor As Long = 0
        Dim conn As New SqlConnection(con)

        Dim command As New SqlCommand
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            command.Connection = conn
            If type = 0 Then
                command.CommandType = CommandType.Text
            Else
                command.CommandType = CommandType.StoredProcedure
            End If
            command.CommandText = _sentence_
            For cont As Integer = 0 To param.Count - 1
                command.Parameters.Add(param(cont))
            Next
            valor = command.ExecuteScalar
            command.Parameters.Clear()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
        End Try
        Return valor
    End Function

    Public Function FillDataSetCNN(ByVal SQL As String, ByVal con As String) As DataSet
        Dim conn2 As New SqlConnection(con)
        Dim ds As DataSet = New DataSet
        Try
            conn2.Open()
            Dim cmd As New SqlDataAdapter(SQL, conn2)
            cmd.Fill(ds)
            conn2.Close()
            conn2.Dispose()
            conn2 = Nothing
        Catch ex As Exception
            conn2.Close()
            conn2.Dispose()
            conn2 = Nothing
        End Try

        Return ds

    End Function

    Public Sub RunSQLQuery(ByVal SQL As String, ByVal con As String)
        Dim conn As New SqlConnection(con)
        conn.Open()
        Dim cmd As New SqlCommand(SQL, conn)
        cmd.CommandTimeout = 900

        cmd.ExecuteNonQuery()
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Public Function RunSQLQueryLong(ByVal SQL As String, ByVal con As String) As Long

        If con = "" Then
            con = p_conexion
        End If

        Dim valor As Long = 0
        Dim conn As New SqlConnection(con)
        conn.Open()
        Dim cmd As New SqlCommand(SQL, conn)

        valor = cmd.ExecuteScalar

        conn.Close()
        conn.Dispose()
        conn = Nothing

        Return valor

    End Function

    Public Sub ExecuteNonQueryWithParams(ByVal spName As String, ByVal params As ArrayList)
        Dim conn As New SqlConnection(p_conexion)
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parmsC = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                parms += param.ParameterName.ToString() + "=" + param.SqlValue.ToString() + ","
            Next

            parmsC = parms

            command.ExecuteNonQuery()
            command.Parameters.Clear()
            command.Dispose()

        Catch ex As Exception
            Throw New Exception(" - ExecuteNonQueryWithParams: " + ex.Message.ToString())
        Finally
            If conn.State <> ConnectionState.Closed Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Sub

    Public Sub ExecuteNonQueryWithParamsCNN(ByVal spName As String, ByVal params As ArrayList, ByVal con As String)
        Dim conn As New SqlConnection(con)
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 60
            command.Connection = conn

            parms = String.Empty
            parmsC = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                parms += param.ParameterName.ToString() + "=" + param.SqlValue.ToString() + ","
            Next

            parmsC = parms

            command.ExecuteNonQuery()
            command.Parameters.Clear()
            command.Dispose()

        Catch ex As Exception
            Throw New Exception(" - ExecuteNonQueryWithParams: " + ex.Message.ToString())
        Finally
            If conn.State <> ConnectionState.Closed Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Sub

    Public Function ExecuteNonQueryTransactionCNN(ByVal spName As String, ByVal params As ArrayList, ByVal con As String) As Boolean
        Dim conn As New SqlConnection(con)

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim transaction As SqlTransaction = conn.BeginTransaction("SampleTransaction")

        Try

            Dim command As New SqlCommand
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = spName
            command.CommandTimeout = 180
            command.Connection = conn
            command.Transaction = transaction

            parms = String.Empty
            parmsC = String.Empty

            For Each param As SqlParameter In params
                command.Parameters.Add(param)
                parms += param.ParameterName.ToString() + "=" + param.SqlValue.ToString() + ","
            Next

            parmsC = parms

            command.ExecuteNonQuery()
            command.Parameters.Clear()
            command.Dispose()

            transaction.Commit()
            Return True

        Catch ex As Exception
            Throw New Exception(" - ExecuteNonQueryTransactionCNN: " + ex.Message.ToString())
            transaction.Rollback()
            Return False
        Finally
            If conn.State <> ConnectionState.Closed Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

End Class
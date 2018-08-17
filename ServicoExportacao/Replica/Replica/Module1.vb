Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Replicar()
    End Sub

    Public Sub Replicar()
        Try
            'Variaveis:
            Dim strConexaoAntiga As String
            Dim strConexaoNova As String

            Dim ConnAntiga As SqlConnection
            Dim ConnNova As SqlConnection

            Dim SQLAntigo As String
            Dim SQLNovo As String

            Dim CommandAntigo As New SqlCommand
            Dim CommandNovo As New SqlCommand

            Dim Adapter As New SqlDataAdapter

            Dim DDD As New DataTable
            Dim USER As New DataTable
            Dim Cont As Long = 0
            Dim ContTudo As Long = 0

            'Configurações:
            strConexaoAntiga = "Data Source=hiruke.ddns.net,14333;Initial Catalog=SD;Persist Security Info=True;Connect Timeout=120;User ID=sa;Password=123*abc"
            strConexaoNova = "Data Source=unclephill.database.windows.net,1433;Initial Catalog=BD_MeuDDD;Persist Security Info=True;Connect Timeout=120;User ID=Administrador;Password=M1n3Rv@7"

            ConnAntiga = New SqlConnection(strConexaoAntiga)
            ConnNova = New SqlConnection(strConexaoNova)

            ConnAntiga.Open()
            ConnNova.Open()

            'Limpado os usuarios:
            SQLNovo = "Truncate Table USERs"
            CommandNovo = New SqlCommand(SQLNovo, ConnNova)
            CommandNovo.CommandTimeout = 0
            CommandNovo.ExecuteNonQuery()

            'Resgatando USERs:
            SQLAntigo = "Select USERs.id, USERs.nome, USERs.sobrenome, USERs.email, USERs.senha, USERs.data_inclusao From USERs"

            Adapter = New SqlDataAdapter(SQLAntigo, ConnAntiga)
            Adapter.Fill(USER)

            For Each Row As DataRow In USER.Rows
                SQLNovo = "Insert Into USERs (nome, sobrenome, email, senha, data_inclusao) Values('" & R(Row("nome")) & "',Null,'" & R(Row("email")) & "','" & R(Row("senha")) & "','" & Format(Row("data_inclusao"), "yyyy-MM-dd hh:mm:ss") & "')"
                CommandNovo = New SqlCommand(SQLNovo, ConnNova)
                CommandNovo.CommandTimeout = 0
                CommandNovo.ExecuteNonQuery()
            Next

            'Resgatando DDDs:
            SQLAntigo = "Select DDDs.DDD, DDDs.ESTADO, DDDs.CIDADE, DDDs.OPERADORA From DDDs"

            Adapter = New SqlDataAdapter(SQLAntigo, ConnAntiga)
            Adapter.Fill(DDD)

            For Each Row As DataRow In DDD.Rows
                If Cont <= 34315 Then
                    Cont += 1
                    ContTudo += 1
                    Console.WriteLine("Registro: " & ContTudo)
                    Continue For
                End If
                SQLNovo = "Insert Into DDDs (DDD, ESTADO, CIDADE, OPERADORA) Values('" & R(Row("DDD")) & "','" & R(Row("ESTADO")) & "','" & R(Row("CIDADE")) & "','" & R(Row("OPERADORA")) & "')"
                CommandNovo = New SqlCommand(SQLNovo, ConnNova)
                CommandNovo.CommandTimeout = 0
                CommandNovo.ExecuteNonQuery()
                ContTudo += 1
                Console.WriteLine("Registro: " & ContTudo)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function R(Valor As String)
        Return Replace(Valor, "'", "''")
    End Function

End Module

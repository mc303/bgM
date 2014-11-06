
Namespace Cll
    ''' <summary>
    ''' Summary description for keyValue.
    ''' </summary>
    Public Class keyValue
        Private _strName As String = ""
        Private _strValue As String = ""
        Private _nType As Integer = 0

        Public Sub New()
        End Sub

        Public Sub setKeyValue(strName As String, strValue As String, nType As Integer)
            _strName = strName
            _strValue = strValue
            _nType = nType
        End Sub

        Public Function getName() As String
            Return _strName
        End Function

        Public Function getValue() As String
            Return _strValue
        End Function

        Public Overloads Function [getType]() As Integer
            Return _nType
        End Function
    End Class
End Namespace
Class DragInfo
    Public Property InitialMouseCoords As Point
    Public Property InitialLocation As Point

    Public Sub New(ByVal MouseCoords As Point, ByVal Location As Point)
        InitialMouseCoords = MouseCoords
        InitialLocation = Location
    End Sub

    Public Function NewLocation(ByVal MouseCoords As Point) As Point
        Dim loc As New Point(InitialLocation.X + (MouseCoords.X - InitialMouseCoords.X), InitialLocation.Y + (MouseCoords.Y - InitialMouseCoords.Y))
        Return Loc
    End Function
End Class

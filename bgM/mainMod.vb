Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation

Module mainMod

    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
    Public _networkinfo As New Dictionary(Of String, String)

    Public Sub loadPictureToMsToPicturebox()
        Dim _bgBox As PictureBox = frmMain.pbMainBackground
        Dim _bg As String = _reg.getSourceWallpaper
        Dim _imgToMemoryStream As New MemoryStream()
        Dim _bmImage As Bitmap = New System.Drawing.Bitmap(_bg)

        _bmImage.Save(_imgToMemoryStream, System.Drawing.Imaging.ImageFormat.Png)
        _bmImage.Dispose()

        _bgBox.Image = Nothing
        _bgBox.BackgroundImage = Image.FromStream(_imgToMemoryStream)
        _bgBox.BackgroundImageLayout = ImageLayout.Stretch
        _imgToMemoryStream.Dispose()

        _imgToMemoryStream = Nothing
        _bgBox = Nothing
        _bg = Nothing

    End Sub


    Public Function GetHostEntryIPv4() As IPHostEntry
        Dim ipHostInfo As IPHostEntry = Dns.GetHostEntry(System.Net.Dns.GetHostName)

        If Not IsNothing(ipHostInfo) Then
            ipHostInfo.AddressList = Array.FindAll(ipHostInfo.AddressList, Function(n As IPAddress) n.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork)
        End If

        GetHostEntryIPv4 = ipHostInfo
    End Function

    Sub getWallpaper()

    End Sub

    Public Function getItemPosition(ByVal i As Integer) As Point
        Dim _location As Point = New Point(0, 0)
        Dim _cmode As String = _reg.getCoordinatesMode

        If _cmode = "" Then _cmode = "Location"

        Select Case _cmode
            Case "Location"
                'Return frmMain.PointToScreen(_Convert.ToPointFromString(_reg.getItemLocation(i.ToString("D2"))))
                Return _Convert.ToPointFromString(_reg.getItemLocation(i.ToString("D2")))
            Case "Percent"
                Dim _percoor As String() = _reg.getItemLocationPercent(i.ToString("D2")).Split(",")
                Dim _x As Integer = (screenWidth / 100) * _percoor(0)
                Dim _y As Integer = (screenHeight / 100) * _percoor(1)
                Return frmMain.PointToScreen(New Point(_x, _y))

            Case "Invert"
                Dim _percoor As String() = _reg.getItemLocationPercent(i.ToString("D2")).Split(",")
                Dim _x As Integer = screenWidth - _percoor(0)
                Dim _y As Integer = screenHeight - _percoor(1)
                Return frmMain.PointToScreen(New Point(_x, _y))

        End Select

    End Function


    Public Sub createNetworkInformation()
        Dim computerProperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        Dim nics As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        Dim _i As Integer = 0
        Dim _i_item As Integer = 0

        If nics Is Nothing OrElse nics.Length < 1 Then
            Debug.Print("  No network interfaces found.")
            Exit Sub
        End If
        For Each adapter As NetworkInterface In nics
            Dim properties As IPInterfaceProperties = adapter.GetIPProperties()
            Dim nicmac As PhysicalAddress = adapter.GetPhysicalAddress
            Dim ipinfo As IPv4InterfaceProperties = adapter.GetIPProperties.GetIPv4Properties
            Dim gateways As GatewayIPAddressInformationCollection = properties.GatewayAddresses
            If adapter.NetworkInterfaceType = NetworkInterfaceType.Ethernet Then
                _networkinfo.Add("#NIC" + _i.ToString + "-NAME#", adapter.Name)
                _networkinfo.Add("#NIC" + _i.ToString + "-ID#", adapter.Id)
                _networkinfo.Add("#NIC" + _i.ToString + "-DEVICENAME#", adapter.Description)
                _networkinfo.Add("#NIC" + _i.ToString + "-MAC#", nicmac.ToString())
                If ipinfo.IsDhcpEnabled Then _networkinfo.Add("#NIC" + _i.ToString + "-DHCPSERVER#", properties.DhcpServerAddresses.First.ToString)
                _networkinfo.Add("#NIC" + _i.ToString + "-GATEWAY#", gateways.First.Address.ToString)
                If Not properties.DnsSuffix.ToString = "" Then _networkinfo.Add("#NIC" + _i.ToString + "-DNSSUFFIX#", properties.DnsSuffix.ToString)
                _networkinfo.Add("#NIC" + _i.ToString + "-SPEED#", adapter.Speed.ToString)

                _i_item = 0
                For Each ip As System.Net.IPAddress In properties.DnsAddresses
                    _networkinfo.Add("#NIC" + _i.ToString + "-DNS" + _i_item.ToString + "#", ip.ToString)
                    _i_item = _i_item + 1
                Next

                _i_item = 0
                For Each ip As UnicastIPAddressInformation In properties.UnicastAddresses
                    If ip.Address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        _networkinfo.Add("#NIC" + _i.ToString + "-IP" + _i_item.ToString + "#", ip.Address.ToString)
                        _networkinfo.Add("#NIC" + _i.ToString + "-NETMASK" + _i_item.ToString + "#", ip.IPv4Mask.ToString)
                        ' _networkinfo.Add("#NIC" + _i.ToString + "-SUFFIX" + _i_item.ToString + "#", ip.SuffixOrigin.ToString)
                        _i_item = _i_item + 1
                        'ElseIf ip.Address.AddressFamily = Sockets.AddressFamily.InterNetworkV6 Then
                        '    _networkinfo.Add("#NIC" + _i.ToString + "-IP6" + _i_item.ToString + "#", ip.Address.ToString)
                        '    _i_item = _i_item + 1
                    End If
                Next
                _i_item = 0
                _i = _i + 1
            End If
        Next
        Console.WriteLine()
    End Sub
    'Public Sub saveLocation(_lbl As Control)

    '    Dim _pX As Integer = _lbl.Left / (screenWidth / 100)
    '    Dim _pY As Integer = (_lbl.Top + 25) / (screenHeight / 100)
    '    ' frmMain.TextBox3.Text = frmMain.MousePosition.X & ", " & frmMain.MousePosition.Y & " - " & _pX & ", " & _pY

    '    Call _reg.setRegX(_pX)
    '    Call _reg.setRegY(_pY)
    'End Sub



End Module

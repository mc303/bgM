Imports Microsoft.Win32
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Threading
Imports System.Net
Imports System.Net.NetworkInformation

Module modMain
    Const _offsetX = 8
    Const _offsetY = 13

    Public screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
    Public _networkinfo As New Dictionary(Of String, String)

    Sub Main()
        Dim _sleep As Integer = _reg.getWait * 1000
        Thread.Sleep(_sleep)
        Call createNetworkInformation()
        Call createPreviewFromBackground()
        Call Wallpaper.Apply(_reg.getWallpaper)
        Application.Exit()
    End Sub

    Sub createPreviewFromBackground()
        Dim _txt As String
        Dim _screenPos As Point
        Dim _imgToMemoryStream As New MemoryStream()
        Dim _color As Brush
        Dim _envText As String
        Dim stringFormat As New StringFormat()
        Dim _inputFields As Integer = _reg.getInputFields()
        Dim _wallpaper As String = _reg.getSourceWallpaper
        'Load the Image to be written on.
        Dim bitMapImage As Bitmap = Bitmap.FromFile(_wallpaper)
        'Dim bitMapImage As Bitmap = Image.FromFile(_bg)
        Dim graphicImage As Graphics = Graphics.FromImage(bitMapImage)

        'Smooth graphics is nice.
        'graphicImage.SmoothingMode = SmoothingMode.AntiAlias
        graphicImage.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        graphicImage.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        graphicImage.CompositingQuality = Drawing2D.CompositingQuality.HighQuality


        If Not _inputFields = 0 Then
            For i As Integer = 0 To _inputFields - 1
                _txt = _reg.getItemText(i.ToString("D2"))
                '_screenPos = _txt.PointToScreen(New Point(-21, -10))
                _screenPos = _Convert.ToPointFromString(_reg.getItemLocation(i.ToString("D2")))
                _screenPos = New Point((_screenPos.X + _offsetX), (_screenPos.Y + _offsetY))
                'ScreenPos = _txt.Location
                _color = New SolidBrush(ColorTranslator.FromWin32(_reg.getItemColor(i.ToString("D2"))))
                'Write your text.
                Select Case _reg.getItemAlign(i.ToString("D2"))
                    Case 0 'Left
                        stringFormat.Alignment = StringAlignment.Near
                        ' stringFormat.LineAlignment = StringAlignment.Near
                    Case 1 'Right
                        stringFormat.Alignment = StringAlignment.Far
                        ' stringFormat.LineAlignment = StringAlignment.Far
                        _screenPos = New Point(_screenPos.X + _reg.getItemWidth(i.ToString("D2")), _screenPos.Y)
                    Case 2 'Center
                        stringFormat.Alignment = StringAlignment.Center
                        'stringFormat.LineAlignment = StringAlignment.Center
                        _screenPos = New Point(_screenPos.X + (_reg.getItemWidth(i.ToString("D2")) / 2), _screenPos.Y)
                End Select

                _envText = ConvertItems.itemToEnviromentVar(_txt)

                'TextRenderer.DrawText()
                graphicImage.DrawString(_envText, _reg.getItemFont(i.ToString("D2")), _color, _screenPos, stringFormat)
            Next

            'Save the new image to the response output stream.
            bitMapImage.Save(_reg.getWallpaper, System.Drawing.Imaging.ImageFormat.Png)

            'Clean house.
            graphicImage.Dispose()
            bitMapImage.Dispose()
            'Me.pbBackground.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub


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
End Module

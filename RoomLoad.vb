 Public Shared Sub Load()
        Dim newlist As New Dictionary(Of Integer, Room)
        Dim reader As XmlTextReader

        Try
            reader = New XmlTextReader(New FileStream(Path.Combine(Config.PdaPath, "room.xml"), FileMode.Open))
        Catch ex As FileNotFoundException
            Throw New Exception("There is no room file.")
        Catch
            Throw New Exception("Unable to load room file.")
        End Try

        Try
            reader.Read()
            reader.MoveToContent()
            If reader.LocalName <> "RoomList" Then
                Throw New Exception("Room file is corrupt.")
            End If

            reader.Read()
            reader.MoveToContent()
            Do Until reader.EOF
                If reader.LocalName = "Room" Then
                    Dim code As Integer
                    Dim name As String
                    Dim number As String
                    Dim bumon As Integer = 0

                    code = Integer.Parse(reader.GetAttribute("Code"))
                    number = reader.GetAttribute("Number")
                    name = reader.GetAttribute("ShortName")

        Dim deptStr As String = reader.GetAttribute("dept")
        If deptStr IsNot Nothing AndAlso deptStr.Length > 0 Then
          dept = Integer.Parse(deptStr)
                    Else
          'If Application is not supported, set to 0 fixed
          dept = 0
                    End If

        newlist.Add(code, New Room(code, number, name, dept))
                End If
                reader.Read()
                reader.MoveToContent()
            Loop

            RoomList = newlist
        Finally
            reader.Close()
        End Try

    End Sub

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace Models
    Public Class Company
        Public Shared ReadOnly Property OfficeRooms() As IList(Of Room)
            Get
                If HttpContext.Current.Session("OfficeRooms") Is Nothing Then
                    HttpContext.Current.Session("OfficeRooms") = GenerateOfficeRooms()
                End If
                Return DirectCast(HttpContext.Current.Session("OfficeRooms"), IList(Of Room))
            End Get
        End Property

        Public Shared Function GetRoomByID(ByVal id As Integer) As Room
            Return ( _
                From room In OfficeRooms _
                Where room.ID = id _
                Select room).SingleOrDefault()
        End Function
        Public Shared Function GenerateRoomID() As Integer
            Return If(OfficeRooms.Count > 0, OfficeRooms.Last().ID + 1, 0)
        End Function

        Public Shared Sub InsertRoom(ByVal room As Room)
            If room IsNot Nothing Then
                room.ID = GenerateRoomID()
                OfficeRooms.Add(room)
            End If
        End Sub
        Public Shared Sub UpdateRoom(ByVal room As Room)
            Dim editableRoom As Room = GetRoomByID(room.ID)
            If editableRoom IsNot Nothing Then
                editableRoom.ID = room.ID
                editableRoom.Number = room.Number
                editableRoom.Floor = room.Floor
                editableRoom.IsReserved = room.IsReserved
            End If
        End Sub
        Public Shared Sub RemoveRoomByID(ByVal id As Integer)
            Dim editableRoom As Room = GetRoomByID(id)
            If editableRoom IsNot Nothing Then
                OfficeRooms.Remove(editableRoom)
            End If
        End Sub

        Private Shared Function GenerateOfficeRooms() As IList(Of Room)
            Dim rooms As New List(Of Room)()
            rooms.Add(New Room() With {.ID = 0, .Number = 101, .Floor = 5, .IsReserved = False})
            rooms.Add(New Room() With {.ID = 1, .Number = 206, .Floor = 2, .IsReserved = True})
            rooms.Add(New Room() With {.ID = 2, .Number = 159, .Floor = 5, .IsReserved = False})
            rooms.Add(New Room() With {.ID = 3, .Number = 395, .Floor = 3, .IsReserved = False})
            rooms.Add(New Room() With {.ID = 4, .Number = 42, .Floor = 4, .IsReserved = False})
            rooms.Add(New Room() With {.ID = 5, .Number = 322, .Floor = 7, .IsReserved = True})
            rooms.Add(New Room() With {.ID = 6, .Number = 532, .Floor = 2, .IsReserved = True})
            Return rooms
        End Function
    End Class

    Public Class Room
        Public Property ID() As Integer
        Public Property Number() As Integer
        Public Property Floor() As Integer
        Public Property IsReserved() As Boolean
    End Class
End Namespace
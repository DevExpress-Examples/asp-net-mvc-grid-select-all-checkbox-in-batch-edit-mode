Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports Models
Imports DevExpress.Web.Mvc

Namespace T401286.Controllers
    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            ' DXCOMMENT: Pass a data model for GridView

            Return View()
        End Function

        Public Function GridViewPartialView() As ActionResult
            ' DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            Return PartialView("GridViewPartialView", Company.OfficeRooms)
        End Function

        <HttpPost, ValidateInput(False)> _
        Public Function BatchUpdateRoomsPartial(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of Room, Integer)) As ActionResult
            For Each item In batchValues.Insert
                If batchValues.IsValid(item) Then
                    Company.InsertRoom(item)
                Else
                    batchValues.SetErrorText(item, "Correct validation errors")
                End If
            Next item
            For Each item In batchValues.Update
                If batchValues.IsValid(item) Then
                    Company.UpdateRoom(item)
                Else
                    batchValues.SetErrorText(item, "Correct validation errors")
                End If
            Next item
            For Each itemKey In batchValues.DeleteKeys
                Company.RemoveRoomByID(itemKey)
            Next itemKey
            Return PartialView("GridViewPartialView", Company.OfficeRooms)
        End Function

    End Class
End Namespace